using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using SpatiaBlazor.Geocode.Abstractions.Descriptor;
using SpatiaBlazor.Geocode.Google;

namespace SpatiaBlazor.Geocode.Abstractions;

public static class GeocodeExtensions
{
    public const string HttpClientTag = "SpatiaBlazorHttpClient";
    public const string WGS85GeometryFactoryTag = "SpatiaBlazorWGS84GeometryFactory";

    public static IServiceCollection AddGeocodeAbstractions(this IServiceCollection services)
    {
        //todo use poly and http resilience
        services.AddHttpClient(HttpClientTag);

        services.AddSingleton<IDescriptorFactory, AttributeOrderDescriptorFactory>();

        services.AddKeyedSingleton<GeometryFactory, GeometryFactory>(WGS85GeometryFactoryTag, (_, _) =>
        {
            var epsg4326 = GeographicCoordinateSystem.WGS84;
            return new GeometryFactory().WithSRID((int) epsg4326.AuthorityCode);
        });

        return services;
    }

    internal static string OptionsPrefixPath(this string? configSectionPath)
    {
        var pathPrefix = string.Empty;
        if (configSectionPath is not null)
        {
            pathPrefix = !configSectionPath.EndsWith(':') ? $"{configSectionPath}:" : configSectionPath;
        }

        return pathPrefix;
    }
}
