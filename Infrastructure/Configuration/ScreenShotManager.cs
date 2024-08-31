using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Configuration;

public sealed class ScreenShotManager
{
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
    private readonly ScreenShotConfiguration _screenShotConfiguration;

    public ScreenShotManager(ScreenShotConfiguration screenShotConfiguration,
        ISpecFlowOutputHelper specFlowOutputHelper)
    {
        _screenShotConfiguration = screenShotConfiguration;
        _specFlowOutputHelper = specFlowOutputHelper;
    }

    public async Task TakeScreenShotAsync(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var scenarioName = scenarioContext.ScenarioInfo.Title;
        var featureName = featureContext.FeatureInfo.Title;
        var browserContext = scenarioContext.ScenarioContainer.Resolve<IBrowserContext>();

        var pages = browserContext.Pages;
        var topmostActivePage = pages[^1];

        await TakePageScreenShotAsync(topmostActivePage, featureName, scenarioName);
    }

    private async Task TakePageScreenShotAsync(IPage page, string featureName, string scenarioName)
    {
        var screenShotFileName = _screenShotConfiguration.GenerateScreenShotFileName(featureName, scenarioName);
        var screenShotPath = _screenShotConfiguration.GetScreenShotPath(screenShotFileName);
        var attachmentUrl = _screenShotConfiguration.GetAttachmentUrl(screenShotFileName);

        await page.ScreenshotAsync(new()
        {
            Path = screenShotPath,
            Type = ScreenshotType.Jpeg,
            FullPage = true,
            Quality = 60,
        });

        Console.WriteLine($@"SCREENSHOT[ {attachmentUrl} ]SCREENSHOT");
        _specFlowOutputHelper.AddAttachment(attachmentUrl);
    }
}