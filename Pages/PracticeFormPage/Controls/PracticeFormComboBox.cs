using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

public sealed class PracticeFormComboBox(ILocator locator) : PracticeFormControlBase(locator: locator)
{
    private const string SingleOption = "#react-select-2-option-0";

    protected override async Task SetValueInternalAsync(string value)
    {
        await Locator.ClickAsync(new() { Force = true });

        var values = value.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var optionLocator = Locator.Page.Locator(SingleOption);

        foreach (var v in values)
        {
            await Locator.FillAsync(v);

            await optionLocator.WaitForAsync(new() { Timeout = DefaultTimeout });
            await optionLocator.ClickAsync();
        }
    }
}