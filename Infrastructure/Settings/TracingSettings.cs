namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Settings;

public sealed class TracingSettings
{
    public const string Key = "BrowserContext";
    public bool? Screenshots { get; init; } = false;
    public bool? Snapshots { get; init; } = false;
    public bool? Sources { get; init; } = false;
}