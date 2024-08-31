using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

public sealed class PracticeFormControlFactory : IPracticeFormControlFactory
{
    private readonly PracticeFormPage _page;
    private IPage Page => _page.Page;

    public PracticeFormControlFactory(PracticeFormPage page)
    {
        _page = page;
    }

    public PracticeFormControlBase Create(string label, string? value = null) => label.ToLower() switch
    {
        "subjects" => new PracticeFormComboBox(Page.Locator("#subjectsInput")),
        "email" => new PracticeFormTextBox(Page.GetByPlaceholder("name@example.com")),
        "date of birth" => new PracticeFormTextBox(Page.Locator("#dateOfBirthInput")),

        "gender" when !string.IsNullOrWhiteSpace(value) => new PracticeFormRadioButton(Page.GetByRole(AriaRole.Radio,
            new() { Name = value, Exact = true })),
        "gender" => throw new ArgumentException("Value cannot be null or empty for gender", nameof(value)),

        "hobbies" when !string.IsNullOrWhiteSpace(value) => new PracticeFormCheckBox(Page.GetByRole(AriaRole.Checkbox,
            new() { Name = value, Exact = true})),
        "hobbies" => throw new ArgumentException("Value cannot be null or empty for hobbies", nameof(value)),

        _ => new PracticeFormTextBox(Page.GetByPlaceholder(label))
    };
}