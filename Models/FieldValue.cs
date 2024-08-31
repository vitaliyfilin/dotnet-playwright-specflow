namespace PlaywrightSpecFlowDemo.WebUI.Tests.Models;

// ReSharper disable ClassNeverInstantiated.Global
internal sealed record FieldValue
    // ReSharper restore ClassNeverInstantiated.Global
{
    public required string Label { get; init; }
    public string? Value { get; init; }
}
