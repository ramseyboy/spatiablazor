using Meziantou.Extensions.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace SpatiaBlazor.Tests;

public sealed class XUnitLoggerFactory(ITestOutputHelper testOutputHelper) : ILoggerFactory
{
    public void Dispose()
    {
        testOutputHelper = null!;
    }

    public void AddProvider(ILoggerProvider provider)
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitLogger(testOutputHelper, new LoggerExternalScopeProvider(), categoryName);
    }

    public ILogger<T> CreateLogger<T>()
    {
        return XUnitLogger.CreateLogger<T>(testOutputHelper);
    }
}
