using Microsoft.Playwright;
using PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Settings;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.PlaywrightInfrastructure;

/// <summary>
/// Factory class responsible for creating and managing Playwright instances and browser contexts.
/// It uses the <see cref="PlaywrightProvider"/> to manage the lifecycle of Playwright and provides methods to create browsers, pages, and browser contexts.
/// </summary>
public sealed class PlaywrightFactory
{
    private readonly PlaywrightSettings _playwrightSettings;
    private readonly PlaywrightProvider _playwrightProvider;

    public PlaywrightFactory(PlaywrightSettings playwrightSettings)
    {
        _playwrightSettings = playwrightSettings;
        _playwrightProvider = new PlaywrightProvider();
    }

    private async Task<IPlaywright> CreateAsync() => await _playwrightProvider.GetPlaywrightAsync();

    public async Task<IBrowser> CreateBrowserAsync(BrowserSettings browserSettings)
    {
        var browserType = await GetBrowserType(browserSettings.Browser ?? _playwrightSettings.Browser);

        return await browserType.LaunchAsync(new()
            {
                Headless = browserSettings.HeadLess.GetValueOrDefault(_playwrightSettings.HeadLess),
                SlowMo = _playwrightSettings.SlowMo,
                TracesDir = _playwrightSettings.TraceDir ?? Path.GetTempPath(),
                
            })
            .ConfigureAwait(false);
    }

    private async Task<IBrowserType> GetBrowserType(string browser)
    {
        var playwright = await CreateAsync();
        browser = browser.ToLower();

        return browser switch
        {
            BrowserConstants.Firefox => playwright.Firefox,
            BrowserConstants.Safari => playwright.Webkit,
            _ => playwright.Chromium,
        };
    }

    public async Task<IPage> CreatePageAsync(IBrowserContext browserContext) => await browserContext.NewPageAsync();

    public async Task<IBrowserContext> CreateBrowserContextAsync(
        IBrowser browser,
        string baseAddress,
        string? locale = null,
        bool? bypassCsp = null,
        bool? ignoreHttpsErrors = null)
    {
        var browserContext = await browser.NewContextAsync(new()
            {
                BaseURL = baseAddress,
                BypassCSP = bypassCsp ?? true,
                IgnoreHTTPSErrors = ignoreHttpsErrors ?? true,
                Locale = locale ?? "en-US",
            })
            .ConfigureAwait(false);

        if (_playwrightSettings.NavigationTimeout.HasValue)
            browserContext.SetDefaultNavigationTimeout(_playwrightSettings.NavigationTimeout.Value * 1000);

        if (_playwrightSettings.LocatorTimeout.HasValue)
            browserContext.SetDefaultTimeout(_playwrightSettings.LocatorTimeout.Value * 1000);

        return browserContext;
    }

    public async Task DisposeAsync() => await _playwrightProvider.DisposeAsync();
}