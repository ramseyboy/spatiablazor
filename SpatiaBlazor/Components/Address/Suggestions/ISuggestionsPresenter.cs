using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface ISuggestionsPresenter: IPresenterMixin<ISuggestionsView>
{
    /// <summary>
    /// Queries the configured geocode client backend for address suggestions.
    /// Relies on the configured view for the <see cref="AddressSuggestionsParametersViewModel"/> as parameters to feed to geocode client request.
    /// </summary>
    /// <param name="token"></param>
    /// <exception cref="SuggestionsException">thrown when an error occurs when fetching suggestions</exception>
    /// <exception cref="InvalidSuggestionsParameterException">thrown when a parameter is set on the <see cref="AddressSuggestionsParametersViewModel"/> that is invalid</exception>
    /// <returns>An IEnumerable of <see cref="IGeocodeResultsViewModel"/></returns>
    public Task<IEnumerable<IGeocodeResultsViewModel>> AutocompleteSuggestions(CancellationToken token);
}
