using Microsoft.Playwright;

namespace PlaywrightSpecFlowDemo.WebUI.Tests.Infrastructure.PlaywrightInfrastructure;

/// <summary>
/// Manages the lifecycle of Playwright, including its creation and disposal.
/// This class is responsible solely for initializing Playwright asynchronously and ensuring its proper disposal.
/// Lazy is used to ensure that the IPlaywright instance is created only once and only when needed. 
/// It provides lazy initialization, similar to singleton behavior but does not manage global access.
/// </summary>
public sealed class PlaywrightProvider : IAsyncDisposable
{
    private readonly Lazy<Task<IPlaywright>> _playwrightTask;
    private volatile bool _disposed;

    public PlaywrightProvider()
    {
        _playwrightTask = new Lazy<Task<IPlaywright>>(async () => await CreatePlaywrightAsync());
    }

    public async Task<IPlaywright> GetPlaywrightAsync()
        => await _playwrightTask.Value;

    private async Task<IPlaywright> CreatePlaywrightAsync()
        => await Playwright.CreateAsync();

    public async ValueTask DisposeAsync()
        => await DisposeAsync(disposing: true);

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (_disposed) return;

        if (disposing && _playwrightTask.IsValueCreated)
        {
            var playwright = await _playwrightTask.Value;
            playwright.Dispose();
        }

        _disposed = true;
    }
}