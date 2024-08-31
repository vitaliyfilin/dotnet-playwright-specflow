using Microsoft.Extensions.Configuration;
using PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Settings;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.Configuration;

public sealed class TestConfiguration
{
    private const string ConfigDir = ".config";
    private readonly string _baseDirectory;
    public PlaywrightSettings PlaywrightSettings { get; }
    public BrowserContextSettings BrowserContextSettings { get; }
    public TracingSettings TracingSettings { get; }

    public TestConfiguration(string? baseDirectory)
    {
        _baseDirectory = baseDirectory ?? throw new ArgumentNullException(nameof(baseDirectory));
        PlaywrightSettings = new PlaywrightSettings();
        BrowserContextSettings = new BrowserContextSettings();
        TracingSettings = new TracingSettings();
        LoadTestConfiguration();
    }

    private void LoadTestConfiguration()
    {
        var environment = GetCurrentEnvironment();

        string GetCurrentEnvironment()
            //local settings json is expected
            => Environment.GetEnvironmentVariable("EnvName") ?? "local";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(_baseDirectory)
            .AddJsonFile(Path.Combine(ConfigDir, "settings.json"))
            .AddJsonFile(Path.Combine(ConfigDir, $"settings.{environment}.json"))
#if DEBUG
            .AddJsonFile(Path.Combine(ConfigDir, "settings.local.json"), optional: true)
#endif
            .AddEnvironmentVariables()
            .Build();

        configuration
            .GetSection(key: PlaywrightSettings.Key)
            .Bind(PlaywrightSettings);

        configuration
            .GetSection(key: BrowserContextSettings.Key)
            .Bind(BrowserContextSettings);

        configuration
            .GetSection(key: TracingSettings.Key)
            .Bind(TracingSettings);
    }
}