namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Settings;

public sealed class PlaywrightSettings
{
    public const string Key = "Playwright";
    public string Browser { get; init; } = "Chromium";
    public bool HeadLess { get; init; } = false;
    public float? NavigationTimeout { get; init; }
    public float? SlowMo { get; init; }
    public string? TraceDir { get; init; }
    public float? LocatorTimeout { get; init; }
    public int? MaxFeaturesRunInParallel { get; init; }
}