using Microsoft.Playwright;
using PlaywrightSpecFlowDemo.WebUI.Tests.Models;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage.Components;

public sealed class PracticeFormPageResultsTable
{
    private readonly PracticeFormPage _page;
    private IPage Page => _page.Page;
    private readonly ILocator _resultsTable;

    public PracticeFormPageResultsTable(PracticeFormPage page)
    {
        _page = page;
        _resultsTable = Page.GetByRole(AriaRole.Table);
    }

    public async Task<TableResults> GetResultsAsync()
    {
        var rows = await _resultsTable.Page.QuerySelectorAllAsync("tr");
        var results = new Dictionary<string, string>(rows.Count);

        await Parallel.ForEachAsync(rows, async (row, _) =>
        {
            var cells = await row.QuerySelectorAllAsync("td");
            if (cells.Count != 2) return;

            var label = await cells[0].InnerTextAsync();
            var value = await cells[1].InnerTextAsync();
            results[label] = value;
        });

        return new TableResults
        {
            StudentName = results.GetValueOrDefault("Student Name")!,
            StudentEmail = results.GetValueOrDefault("Student Email")!,
            Gender = results.GetValueOrDefault("Gender")!,
            Mobile = results.GetValueOrDefault("Mobile")!,
            DateOfBirth = results.GetValueOrDefault("Date of Birth")!,
            Subjects = results.GetValueOrDefault("Subjects")!,
            Hobbies = results.GetValueOrDefault("Hobbies")!,
            Picture = results.GetValueOrDefault("Picture"),
            Address = results.GetValueOrDefault("Address")!,
            StateAndCity = results.GetValueOrDefault("State and City")
        };
    }
}