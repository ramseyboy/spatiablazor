using Microsoft.Extensions.DependencyInjection;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Components.Geocode.Suggestions.Photon;

public static class PhotonSuggestionsExtensions
{
    public static IServiceCollection AddPhotonAddressSuggestionsComponent(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddPhotonGeocodeClient(serviceLifetime);
        services.Add(new ServiceDescriptor(typeof(ISuggestionsPresenter), typeof(PhotonSuggestionsPresenter), serviceLifetime));
        return services;
    }
}
