namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Settings;

public sealed class BrowserContextSettings
{
    public const string Key = "WebUi";
    public string BaseAddress { get; init; }
    public bool? ByPassCsp { get; init; } = true;
    public bool? IgnoreHttpsErrors { get; init; } = true;
    public string? Locale { get; init; } = "en-US";
}