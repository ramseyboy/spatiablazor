using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Features;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public static class PhotonGeocodeExtensions
{
    public static IServiceCollection AddPhotonGeocodeClient(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        string? configSectionPath = null)

    {
        var path = $"{configSectionPath.OptionsPrefixPath()}SpatiaBlazor:Geocode:Photon";
        services.AddOptions<PhotonGeocodeConfigurationOptions>()
            .BindConfiguration(path)
            .ValidateOnStart()
            .ValidateDataAnnotations();

        services.AddGeocodeAbstractions();

        services.Add(new ServiceDescriptor(typeof(IGeocodeClient), typeof(PhotonGeocodeClient), serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(PhotonGeocodeRecordFactory), typeof(PhotonGeocodeRecordFactory), serviceLifetime));
        return services;
    }
}
