namespace PlaywrightSpecFlowDemo.WebUI.Tests.Models;

public sealed class ContextData<T>
{
    public T Value { get; }

    public ContextData(T value)
    {
        Value = value;
    }
}
