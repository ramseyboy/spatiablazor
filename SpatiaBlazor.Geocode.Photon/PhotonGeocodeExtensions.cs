using BlazorGeospatial.Geocode.Client;
using Microsoft.Extensions.DependencyInjection;

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
        services.Add(new ServiceDescriptor(typeof(IGeocodeClient<PhotonGeocodeResponse>), typeof(PhotonGeocodeClient), serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(PhotonGeocodeClient), typeof(PhotonGeocodeClient), serviceLifetime));
        return services;
    }
}
