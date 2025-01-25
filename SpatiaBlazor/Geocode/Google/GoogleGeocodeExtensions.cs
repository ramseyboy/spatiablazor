using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Google.V1;

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
        services.Add(new ServiceDescriptor(
            typeof(IGeocodeRecordFactory<PlacesV1GeocodeDetail, GoogleGeocodeRecord>),
            typeof(GoogleGeocodeRecordFactory),
            serviceLifetime));
        return services;
    }
}
