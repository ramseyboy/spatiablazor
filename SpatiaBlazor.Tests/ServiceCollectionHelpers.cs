using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace SpatiaBlazor.Tests;

public class ServiceCollectionHelpers
{
    public static readonly IConfiguration BaseConfiguration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.UnitTest.json", optional: true, reloadOnChange: false)
        .Build();

    public static IServiceCollection CreateServiceCollection(ITestOutputHelper testOutputHelper, IConfiguration? configuration = null, Action<IServiceCollection>? configureServicesFunc = null)
    {
        configuration ??= BaseConfiguration;

        var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        serviceCollection.AddSingleton(configuration);

        var loggerFactory = new XUnitLoggerFactory(testOutputHelper);
        serviceCollection.TryAdd(ServiceDescriptor.Singleton(loggerFactory));
        serviceCollection.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory>(loggerFactory));
        serviceCollection.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(XUnitLoggerWrapper<>)));

        configureServicesFunc?.Invoke(serviceCollection);

        return serviceCollection;
    }
}
