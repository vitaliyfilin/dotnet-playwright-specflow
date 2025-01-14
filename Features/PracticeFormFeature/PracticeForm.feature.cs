﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:4.0.0.0
//      SpecFlow Generator Version:4.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace PlaywrightSpecFlowDemo.WebUI.Tests.Features.PracticeFormFeature
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "4.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "Browser:chrome")]
    [Xunit.TraitAttribute("Category", "Smoke")]
    [Xunit.TraitAttribute("Category", "Regression")]
    public partial class PracticeFormTestsFeature : object, Xunit.IClassFixture<PracticeFormTestsFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = new string[] {
                "Browser:chrome",
                "Smoke",
                "Regression"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "PracticeForm.feature"
#line hidden
        
        public PracticeFormTestsFeature(PracticeFormTestsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunnerForAssembly(null, TechTalk.SpecFlow.xUnit.SpecFlowPlugin.XUnitParallelWorkerTracker.Instance.GetWorkerId());
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/PracticeFormFeature", "Practice form tests", null, ProgrammingLanguage.CSharp, featureTags);
            await testRunner.OnFeatureStartAsync(featureInfo);
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
            string testWorkerId = testRunner.TestWorkerId;
            await testRunner.OnFeatureEndAsync();
            testRunner = null;
            TechTalk.SpecFlow.xUnit.SpecFlowPlugin.XUnitParallelWorkerTracker.Instance.ReleaseWorker(testWorkerId);
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        public virtual async System.Threading.Tasks.Task FeatureBackgroundAsync()
        {
#line 6
    #line hidden
#line 7
        await testRunner.GivenAsync("I navigate to practice form page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Successful form fill")]
        [Xunit.TraitAttribute("FeatureTitle", "Practice form tests")]
        [Xunit.TraitAttribute("Description", "Successful form fill")]
        public async System.Threading.Tasks.Task SuccessfulFormFill()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Successful form fill", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 9
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 6
    await this.FeatureBackgroundAsync();
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Label",
                            "Value"});
                table1.AddRow(new string[] {
                            "First Name",
                            "John"});
                table1.AddRow(new string[] {
                            "Last Name",
                            "Doe"});
                table1.AddRow(new string[] {
                            "Email",
                            "john@doe.com"});
                table1.AddRow(new string[] {
                            "Gender",
                            "Male"});
                table1.AddRow(new string[] {
                            "Mobile Number",
                            "1234567891"});
                table1.AddRow(new string[] {
                            "Date of Birth",
                            "11 august 2005"});
                table1.AddRow(new string[] {
                            "Subjects",
                            "English, Economics"});
                table1.AddRow(new string[] {
                            "Hobbies",
                            "Music"});
                table1.AddRow(new string[] {
                            "Current Address",
                            "Unknown, Unknown street"});
#line 10
        await testRunner.WhenAsync("I fill practice form fields with the following data", ((string)(null)), table1, "When ");
#line hidden
#line 21
        await testRunner.WhenAsync("I submit data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 22
        await testRunner.ThenAsync("thanks message should be visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Label",
                            "Value"});
                table2.AddRow(new string[] {
                            "Student Name",
                            "John Doe"});
                table2.AddRow(new string[] {
                            "Student Email",
                            "john@doe.com"});
                table2.AddRow(new string[] {
                            "Gender",
                            "Male"});
                table2.AddRow(new string[] {
                            "Mobile",
                            "1234567891"});
                table2.AddRow(new string[] {
                            "Date of Birth",
                            "11 August,2005"});
                table2.AddRow(new string[] {
                            "Subjects",
                            "English, Economics"});
                table2.AddRow(new string[] {
                            "Hobbies",
                            "Music"});
                table2.AddRow(new string[] {
                            "Address",
                            "Unknown, Unknown street"});
#line 23
        await testRunner.ThenAsync("I compare the table values with the filled values:", ((string)(null)), table2, "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "4.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await PracticeFormTestsFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await PracticeFormTestsFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
