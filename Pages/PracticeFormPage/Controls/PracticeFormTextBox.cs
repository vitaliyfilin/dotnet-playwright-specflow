using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

public sealed class PracticeFormTextBox(ILocator locator) : PracticeFormControlBase(locator: locator)
{
    protected override async Task SetValueInternalAsync(string value) => await Locator.FillAsync(value);
}