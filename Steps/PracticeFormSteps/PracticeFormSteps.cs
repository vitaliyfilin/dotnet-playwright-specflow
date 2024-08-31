using FluentAssertions;
using PlaywrightSpecFlowDemo.WebUI.Tests.Models;
using PlaywrightSpecFlowDemo.WebUI.Tests.Pages.PracticeFormPage;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Steps.PracticeFormSteps;

[Binding]
public sealed class PracticeFormSteps : StepsBase
{
    private readonly PracticeFormPage _practiceFormPage;

    public PracticeFormSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(
        featureContext: featureContext, scenarioContext: scenarioContext)
    {
        _practiceFormPage = new PracticeFormPage(page: Page);
    }

    [Given("I navigate to practice form page")]
    public async Task GivenINavigateToPracticeFormPage()
    {
        await _practiceFormPage.Navigate();
    }

    [When(@"I fill practice form fields with the following data")]
    public async Task WhenIFillPracticeFormFieldsWithTheFollowingData(Table table)
    {
        var data = table.CreateSet<FieldValue>();

        foreach (var value in data)
        {
            if (value is not null)
                await _practiceFormPage.FillAsync(label: value.Label, value: value.Value);
        }
    }

    [When("I submit data")]
    public async Task WhenISubmitData()
    {
        await _practiceFormPage.Submit();
    }

    [Then("thanks message should be visible")]
    public async Task ThenThanksMessageShouldBeVisible()
    {
        var isThanksVisible = await _practiceFormPage.IsThanksVisible();

        isThanksVisible.Should().Be(expected: true,
            because: "because the thanks message should be visible to the user after form submission");
    }

    [Then(@"I click on a button (.*)")]
    public async Task ThenIClickOnAButton(string buttonName)
    {
        var button = Page.GetByText(buttonName).First;
        await button.ClickAsync();
    }

    [Then("I compare the table values with the filled values:")]
    public async Task ThenICompareTheTableValuesWithTheFilledValues(Table table)
    {
        var data = table.CreateSet<FieldValue>();
        await AreResultsMatchingAsync(expectedValues: data);
    }
    
    private async Task AreResultsMatchingAsync(IEnumerable<FieldValue> expectedValues)
    {
        var tableResults = await _practiceFormPage.GetTableResultsAsync();
        
        foreach (var expected in expectedValues.AsParallel())
        {
            var actualValue = expected.Label switch
            {
                "Student Name" => tableResults.StudentName,
                "Student Email" => tableResults.StudentEmail,
                "Gender" => tableResults.Gender,
                "Mobile" => tableResults.Mobile,
                "Date of Birth" => tableResults.DateOfBirth,
                "Subjects" => tableResults.Subjects,
                "Hobbies" => tableResults.Hobbies,
                "Picture" => tableResults.Picture,
                "Address" => tableResults.Address,
                "State and City" => tableResults.StateAndCity,
                _ => throw new InvalidOperationException($"Unexpected label '{expected.Label}'")
            };

            actualValue.Should().BeEquivalentTo(expected.Value,
                $"Value for '{expected.Label}' should match the expected value");
        }
    }
}