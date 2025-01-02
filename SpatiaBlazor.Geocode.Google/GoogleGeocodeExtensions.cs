using Microsoft.Extensions.DependencyInjection;

namespace SpatiaBlazor.Geocode.Google;

public static class GoogleGeocodeExtensions
{
    public static IServiceCollection AddGooglePlacesGeocodeClient(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddOptions<GooglePlacesGeocodeConfigurationOptions>()
            .BindConfiguration(GooglePlacesGeocodeConfigurationOptions.OptionsPath)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        //todo use poly and http resilience
        services.AddHttpClient(GooglePlacesGeocodeClient.HttpClientTag);
        return services;
    }
}
