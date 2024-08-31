using Microsoft.Playwright;
using PlaywrightSpecFlowDemo.WebUI.Tests.Models;
using PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Components;
using PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Controls;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage;

public sealed class PracticeFormPage : PageBase
{
    private const string Url = "/automation-practice-form";
    private readonly ILocator _submitButton, _thanksHeader;
    private readonly IPracticeFormControlFactory _practiceFormControlFactory;
    private readonly PracticeFormPageResultsTable _resultsTable;

    public PracticeFormPage(IPage page) : base(page)
    {
        _submitButton = Page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
        _thanksHeader = Page.GetByText("Thanks");
        _practiceFormControlFactory = new PracticeFormControlFactory(this);
        _resultsTable = new PracticeFormPageResultsTable(this);
    }

    public async Task Navigate() => await Navigate(url: Url);
    public async Task Submit() => await _submitButton.ClickAsync();
    public async Task<bool> IsThanksVisible() => await _thanksHeader.IsVisibleAsync();

    /// <summary>
    /// Fills a form field based on the provided label and value.
    /// </summary>
    /// <param name="label">The label of the form field.</param>
    /// <param name="value">The value to set in the form field.</param>
    public async Task FillAsync(string label, string? value)
    {
        var control = _practiceFormControlFactory.Create(label, value);
        await control.SetValueAsync(value);
    }
    
    /// <summary>
    /// Asynchronously returns an object of type <see cref="TableResults"/>.
    /// </summary>
    /// <returns>An instance of <see cref="TableResults"/>.</returns>
    public async Task<TableResults> GetTableResultsAsync()
    {
        return await _resultsTable.GetResultsAsync();
    }

}