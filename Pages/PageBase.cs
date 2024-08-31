using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages;

public class PageBase
{
    public IPage Page { get; }

    protected PageBase(IPage page)
    {
        Page = page;
    }

    protected async Task Navigate(string url, string? referer = null)
    {
        await Page.GotoAsync(url, new()
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Referer = referer
        });
    }
}