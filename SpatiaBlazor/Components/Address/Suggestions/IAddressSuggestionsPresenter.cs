using SpatiaBlazor.Components.Mixins;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface IAddressSuggestionsPresenter: IPresenterMixin<ISuggestionsView>
{
    /// <summary>
    /// Queries the configured geocode client backend for address suggestions.
    /// Relies on the configured view for the <see cref="AddressSuggestionsParametersViewModel"/> as parameters to feed to geocode client request.
    /// </summary>
    /// <param name="token"></param>
    /// <exception cref="SuggestionsException">thrown when an error occurs when fetching suggestions</exception>
    /// <exception cref="InvalidSuggestionsParameterException">thrown when a parameter is set on the <see cref="AddressSuggestionsParametersViewModel"/> that is invalid</exception>
    /// <returns>An IEnumerable of <see cref="IAutocompleteRecord"/></returns>
    public Task<IEnumerable<IAutocompleteRecord>> Suggest(CancellationToken token);

    public Task<IGeocodeRecord> SuggestionClicked(IAutocompleteRecord record, CancellationToken token);
}
