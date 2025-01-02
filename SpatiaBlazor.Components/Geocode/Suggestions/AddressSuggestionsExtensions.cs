using Microsoft.Extensions.DependencyInjection;
using SpatiaBlazor.Components.Geocode.Suggestions.Photon;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Components.Geocode.Suggestions;

public static class AddressSuggestionsExtensions
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
