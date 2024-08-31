using BoDi;
using Microsoft.Playwright;
using PlaywrightSpecFlowDemo.WebUI.Tests.Models;
using TechTalk.SpecFlow;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Steps;

public class StepsBase
{
    protected IPage Page { get; private set; }
    private readonly FeatureContext _featureContext;
    protected IBrowserContext BrowserContext { get; }
    private ScenarioContext ScenarioContext { get; }
    protected IObjectContainer ScenarioContainer => ScenarioContext.ScenarioContainer;

    protected StepsBase(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        _featureContext = featureContext;
        ScenarioContext = scenarioContext;
        Page = ScenarioContainer.Resolve<IPage>();
        BrowserContext = ScenarioContainer.Resolve<IBrowserContext>();

        BrowserContext.Page += (_, page) =>
        {
            Page = page;
        };
    }

    protected void AddContextData<T>(T contextData, string name)
    {
        var primitiveContextData = new ContextData<T>(contextData);
        ScenarioContainer.RegisterInstanceAs(primitiveContextData, name);
    }

    protected T GetContextData<T>(string name)
    {
        var primitiveContextData = ScenarioContainer.Resolve<ContextData<T>>(name);
        if (primitiveContextData == null)
            throw new InvalidOperationException($"Context data not found in registry for {name}");
        return primitiveContextData.Value;
    }

    protected IReadOnlyList<IDownload> GetDownloads() => ScenarioContainer.Resolve<List<IDownload>>("downloads");
}