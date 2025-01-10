using Microsoft.Extensions.DependencyInjection;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public static class PhotonGeocodeExtensions
{
    public static IServiceCollection AddPhotonGeocodeClient(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        string? configSectionPath = null)

    {
        var pathPrefix = string.Empty;
        if (configSectionPath is not null)
        {
            pathPrefix = !configSectionPath.EndsWith(':') ? $"{configSectionPath}:" : configSectionPath;
        }

        var path = $"{pathPrefix}SpatiaBlazor:Geocode:Photon";
        services.AddOptions<PhotonGeocodeConfigurationOptions>()
            .BindConfiguration(path)
            .ValidateDataAnnotations();

        //todo use poly and http resilience
        services.AddHttpClient(PhotonGeocodeClient.HttpClientTag);
        services.Add(new ServiceDescriptor(typeof(IGeocodeClient), typeof(PhotonGeocodeClient), serviceLifetime));
        return services;
    }
}
