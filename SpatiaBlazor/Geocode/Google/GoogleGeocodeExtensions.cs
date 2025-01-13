using Microsoft.Extensions.DependencyInjection;

namespace SpatiaBlazor.Geocode.Google;

public static class GoogleGeocodeExtensions
{
    public static IServiceCollection AddGooglePlacesGeocodeClient(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        string? configSectionPath = null)
    {
        var pathPrefix = string.Empty;
        if (configSectionPath is not null)
        {
            pathPrefix = !configSectionPath.EndsWith(':') ? $"{configSectionPath}:" : configSectionPath;
        }

        var path = $"{pathPrefix}SpatiaBlazor:Geocode:Google";

        services.AddOptions<GooglePlacesGeocodeConfigurationOptions>()
            .BindConfiguration(path)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        //todo use poly and http resilience
        services.AddHttpClient(GooglePlacesGeocodeClient.HttpClientTag);
        return services;
    }
}
