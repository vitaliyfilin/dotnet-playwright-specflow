using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

public sealed class PracticeFormRadioButton(ILocator locator) : PracticeFormControlBase(locator: locator)
{
    protected override async Task SetValueInternalAsync(string value) =>
        await Locator.ClickAsync(new()
        {
            Force = true,
            NoWaitAfter = true
        });
}