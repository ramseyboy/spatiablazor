using Microsoft.Extensions.DependencyInjection;

namespace SpatiaBlazor.Components.Address.Suggestions;

public static class AddressSuggestionsExtensions
{
    public static IServiceCollection AddAddressSuggestions(
        this IServiceCollection services,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.Add(new ServiceDescriptor(typeof(IAddressSuggestionsPresenter), typeof(DefaultAddressSuggestionsPresenter), serviceLifetime));
        return services;
    }
}
