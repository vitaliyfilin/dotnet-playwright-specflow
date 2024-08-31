using Microsoft.Playwright;
using PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Configuration;
using PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.PlaywrightInfrastructure;
using PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Settings;
using TechTalk.SpecFlow;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Hooks;

[Binding]
public sealed class SpecFlowHooks
{
    private const int MaxFeaturesRunInParallelDefault = 1;

    private static readonly PlaywrightFactory PlaywrightFactory;
    private static readonly TestConfiguration TestConfiguration;
    private static readonly ScreenShotConfiguration ScreenShotConfiguration;
    private static readonly SemaphoreSlim SemaphoreSlim;
    private static readonly string TraceDir;

    static SpecFlowHooks()
    {
        var workingDirectory = Environment.CurrentDirectory;
        var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        TestConfiguration = new TestConfiguration(projectDirectory);
        ScreenShotConfiguration = new ScreenShotConfiguration();
        PlaywrightFactory = new PlaywrightFactory(TestConfiguration.PlaywrightSettings);
        SemaphoreSlim =
            new SemaphoreSlim(
                TestConfiguration.PlaywrightSettings.MaxFeaturesRunInParallel.GetValueOrDefault(
                    MaxFeaturesRunInParallelDefault));
        TraceDir = TestConfiguration.PlaywrightSettings.TraceDir!;
    }

    private static ScreenShotManager CreateScreenShotManager(ISpecFlowOutputHelper outputHelper) =>
        new(ScreenShotConfiguration, outputHelper);

    [BeforeTestRun]
    public static void BeforeTestRun(ISpecFlowOutputHelper specFlowOutputHelper)
    {
        ClearTraceDir(specFlowOutputHelper);
        ScreenShotConfiguration.ClearScreenShotDir(specFlowOutputHelper);
    }

    /// <summary>
    /// Initializes the browser instance before any feature is executed.
    /// Registers the browser instance in the FeatureContainer to be shared across all scenarios in the feature.
    /// </summary>
    [BeforeFeature]
    public static async Task BeforeFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
    {
        await SemaphoreSlim.WaitAsync();

        outputHelper.WriteLine($"### Start {featureContext.FeatureInfo.Title} ###");

        var browser = await CreateBrowser(featureContext);
        featureContext.FeatureContainer.RegisterInstanceAs(browser);
        featureContext.FeatureContainer.RegisterInstanceAs(CreateScreenShotManager(outputHelper));
    }

    /// <summary>
    /// Initializes the browser context and page before each scenario is executed.
    /// Registers the page and browser context instances in the ScenarioContainer for use within the scenario.
    /// </summary>
    [BeforeScenario]
    public static async Task BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext,
        ISpecFlowOutputHelper outputHelper)
    {
        var screenShotConfiguration = new ScreenShotConfiguration();
        var name = screenShotConfiguration.GenerateScreenShotFileName(featureContext.FeatureInfo.Title,
            scenarioContext.ScenarioInfo.Title);

        outputHelper.WriteLine(name);

        outputHelper.WriteLine($"### Start scenario {scenarioContext.ScenarioInfo.Title} ###");

        var browserContext = await CreateBrowserContext(featureContext);

        await browserContext.Tracing.StartAsync(new()
        {
            Screenshots = TestConfiguration.TracingSettings.Screenshots,
            Snapshots = TestConfiguration.TracingSettings.Snapshots,
            Sources = TestConfiguration.TracingSettings.Sources
        });

        var page = await PlaywrightFactory.CreatePageAsync(browserContext);
        var downloads = new List<IDownload>();

        page.Console += (_, msg) =>
        {
            if ("error".Equals(msg.Type))
                Console.WriteLine($"Console error: {msg.Text}");
        };

        page.Download += (_, file) => { downloads.Add(file); };

        scenarioContext.Add("browserContextSettings", TestConfiguration.BrowserContextSettings);
        scenarioContext.ScenarioContainer.RegisterInstanceAs(downloads, nameof(downloads));
        scenarioContext.ScenarioContainer.RegisterInstanceAs(page);
        scenarioContext.ScenarioContainer.RegisterInstanceAs(browserContext);
    }


    [BeforeStep]
    public async Task BeforeStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        await TakeScreenShot(featureContext, scenarioContext);
    }

    [AfterStep]
    public async Task AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        await TakeScreenShot(featureContext, scenarioContext);
    }

    private async Task TakeScreenShot(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var photographer = featureContext.FeatureContainer.Resolve<ScreenShotManager>();
        await photographer.TakeScreenShotAsync(featureContext, scenarioContext);
    }

    /// <summary>
    /// Closes the browser instance after all scenarios in a feature have been executed.
    /// This ensures that the browser is properly closed and resources are released.
    /// </summary>
    [AfterFeature]
    public static async Task AfterFeature(FeatureContext featureContext, ISpecFlowOutputHelper outputHelper)
    {
        try
        {
            var featureContainer = featureContext.FeatureContainer;
            if (featureContainer.IsRegistered<IBrowser>())
            {
                var browser = featureContainer.Resolve<IBrowser>();
                if (browser is not null)
                    await browser.CloseAsync();
            }
        }
        finally
        {
            SemaphoreSlim.Release();
            outputHelper.WriteLine($"### End {featureContext.FeatureInfo.Title} ###");
        }
    }

    [AfterScenario]
    public static async Task AfterScenario(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
    {
        try
        {
            if (scenarioContext.ScenarioContainer.IsRegistered<IPage>())
            {
                var page = scenarioContext.ScenarioContainer.Resolve<IPage>();
                if (page is not null)
                    await page.CloseAsync();
            }

            if (scenarioContext.ScenarioContainer.IsRegistered<IBrowserContext>())
            {
                var browserContext = scenarioContext.ScenarioContainer.Resolve<IBrowserContext>();
                if (browserContext is not null)
                    await browserContext.CloseAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            outputHelper.WriteLine($"### End scenario {scenarioContext.ScenarioInfo.Title} ###");
        }
    }

    /// <summary>
    /// Disposes of the PlaywrightFactory after all tests have been run.
    /// Ensures that resources used by Playwright are cleaned up.
    /// </summary>
    [AfterTestRun]
    public static async Task AfterTestRun(ISpecFlowOutputHelper outputHelper)
    {
        try
        {
            await PlaywrightFactory.DisposeAsync();
        }
        catch (Exception e)
        {
            outputHelper.WriteLine(e.Message);
        }
    }

    private static async Task<IBrowserContext> CreateBrowserContext(FeatureContext featureContext)
    {
        var browser = featureContext.FeatureContainer.Resolve<IBrowser>();
        var baseAddress = TestConfiguration.BrowserContextSettings.BaseAddress;

        return await PlaywrightFactory.CreateBrowserContextAsync(
            browser: browser,
            baseAddress: baseAddress,
            bypassCsp: TestConfiguration.BrowserContextSettings.ByPassCsp,
            ignoreHttpsErrors: TestConfiguration.BrowserContextSettings.IgnoreHttpsErrors,
            locale: TestConfiguration.BrowserContextSettings.Locale);
    }

    private static async Task<IBrowser> CreateBrowser(FeatureContext featureContext)
    {
        var browserSettings = CreateCustomBrowserSettings(featureContext);
        return await PlaywrightFactory.CreateBrowserAsync(browserSettings);
    }

    private static BrowserSettings CreateCustomBrowserSettings(FeatureContext featureContext)
    {
        var featureTags = ParseFeatureTags(featureContext: featureContext);
        var isNotHeadless = !featureTags.ContainsKey(CustomTags.Headless);

        featureTags.TryGetValue(CustomTags.Browser, out var browserName);

        return new BrowserSettings
        {
            HeadLess = !isNotHeadless,
            Browser = browserName
        };
    }

    private static Dictionary<string, string?> ParseFeatureTags(FeatureContext featureContext)
    {
        var featureTags = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        var tags = featureContext.FeatureInfo.Tags.AsSpan();

        foreach (var tag in tags)
        {
            var tagSpan = tag.AsSpan();
            var delimiterIndex = tagSpan.IndexOf(':');

            if (delimiterIndex == -1)
            {
                featureTags.Add(tagSpan.ToString(), null);
            }
            else
            {
                var keySpan = tagSpan.Slice(0, delimiterIndex).Trim();
                var valueSpan = tagSpan.Slice(delimiterIndex + 1).Trim();
                featureTags.Add(keySpan.ToString(), valueSpan.ToString());
            }
        }

        return featureTags;
    }

    private static void ClearTraceDir(ISpecFlowOutputHelper specFlowOutputHelper)
    {
        var directory = new DirectoryInfo(TraceDir);
        if (!directory.Exists) return;

        specFlowOutputHelper.WriteLine($"Clearing trace directory: {TraceDir}");
        foreach (var file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (var dir in directory.GetDirectories())
        {
            dir.Delete(true);
        }
    }
}