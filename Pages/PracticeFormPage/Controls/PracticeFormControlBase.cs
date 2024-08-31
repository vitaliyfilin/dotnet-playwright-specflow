using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

public abstract class PracticeFormControlBase
{
    protected ILocator Locator { get; }
    protected const int DefaultTimeout = 1_000;

    protected PracticeFormControlBase(ILocator locator)
    {
        Locator = locator;
    }

    // Public method to handle null or whitespace check
    public async Task SetValueAsync(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;
        
        await SetValueInternalAsync(value);
    }

    // Abstract method for specific implementation in derived classes
    protected abstract Task SetValueInternalAsync(string value);
}