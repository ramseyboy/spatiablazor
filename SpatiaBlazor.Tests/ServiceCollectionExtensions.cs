using Microsoft.Extensions.DependencyInjection;

namespace SpatiaBlazor.Tests;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Removes all registered <see cref="ServiceLifetime.Transient" /> registrations of <see cref="TService" /> and adds in <see cref="TImplementation" />.
    /// </summary>
    /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
    /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService" /> to add into <see cref="services" />.</typeparam>
    /// <param name="services"></param>
    public static void SwapTransient<TService, TImplementation>(this IServiceCollection services)
        where TImplementation : class, TService
    {
        if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient))
        {
            var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient).ToList();
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                services.Remove(serviceDescriptor);
            }
        }

        services.AddTransient(typeof(TService), typeof(TImplementation));
    }

    public static void SwapTransient<TService>(this IServiceCollection services, TService impl)
    {
        if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient))
        {
            var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient).ToList();
            foreach (var serviceDescriptor in serviceDescriptors)
            {
                services.Remove(serviceDescriptor);
            }
        }

        services.AddTransient(typeof(TService), _ => impl!);
    }

    public static void RemoveService<TService>(this IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }
}
