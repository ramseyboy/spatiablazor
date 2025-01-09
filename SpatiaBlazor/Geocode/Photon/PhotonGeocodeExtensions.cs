using Microsoft.Extensions.DependencyInjection;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public static class PhotonGeocodeExtensions
{
    public static IServiceCollection AddPhotonGeocodeClient(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddOptions<PhotonGeocodeConfigurationOptions>()
            .BindConfiguration(PhotonGeocodeConfigurationOptions.OptionsPath)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        //todo use poly and http resilience
        services.AddHttpClient(PhotonGeocodeClient.HttpClientTag);
        services.Add(new ServiceDescriptor(typeof(IGeocodeClient), typeof(PhotonGeocodeClient), serviceLifetime));
        return services;
    }
}
