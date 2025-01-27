using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Google.V1;
using SpatiaBlazor.Geocode.Google.V1.Geocode;
using SpatiaBlazor.Geocode.Google.V1.Places;

namespace SpatiaBlazor.Geocode.Google;

public static class GoogleGeocodeExtensions
{
    public static IServiceCollection AddGooglePlacesGeocodeClient(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        string? configSectionPath = null)
    {
        var path = $"{configSectionPath.OptionsPrefixPath()}SpatiaBlazor:Geocode:GooglePlaces";

        services.AddOptions<GoogleGeocodeConfigurationOptions>()
            .BindConfiguration(path)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddGeocodeAbstractions();

        services.Add(new ServiceDescriptor(typeof(IGeocodeClient), typeof(GoogleGeocodeClient), serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(PlacesV1GeocodeRecordFactory), typeof(PlacesV1GeocodeRecordFactory), serviceLifetime));
        services.Add(new ServiceDescriptor(typeof(PlacesV1PlaceDetailRecordFactory), typeof(PlacesV1PlaceDetailRecordFactory), serviceLifetime));
        return services;
    }
}
