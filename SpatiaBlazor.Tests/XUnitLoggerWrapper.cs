using Microsoft.Extensions.Logging;

namespace SpatiaBlazor.Tests;

public class XUnitLoggerWrapper<T>: ILogger<T>
{
    private readonly ILogger<T> _delegate;

    public XUnitLoggerWrapper(XUnitLoggerFactory factory)
    {
        _delegate = factory.CreateLogger<T>();
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return _delegate.BeginScope(state);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _delegate.IsEnabled(logLevel);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _delegate.Log(logLevel, eventId, state, exception, formatter);
    }
}
