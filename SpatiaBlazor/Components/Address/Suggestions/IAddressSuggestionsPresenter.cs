using SpatiaBlazor.Components.Mixins;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface IAddressSuggestionsPresenter: IPresenterMixin<ISuggestionsView>
{

    public IAutocompleteRecord? SelectedAutocompleteValue { get; }

    /// <summary>
    /// Queries the configured geocode client backend for address suggestions.
    /// Relies on the configured view for the <see cref="AddressSuggestionsParametersViewModel"/> as parameters to feed to geocode client request.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="token"></param>
    /// <exception cref="OperationCanceledException">thrown when operation has been cancelled</exception>
    /// <exception cref="InvalidOperationException"> thrown when presenter is not initialized or when an invalid state occurs</exception>
    /// <exception cref="SuggestionsException">thrown when an error occurs when fetching suggestions</exception>
    /// <exception cref="InvalidSuggestionsParameterException">thrown when a parameter is set on the <see cref="AddressSuggestionsParametersViewModel"/> that is invalid</exception>
    /// <returns>A Task of IEnumerable of <see cref="IAutocompleteRecord"/></returns>
    public Task<IEnumerable<IAutocompleteRecord>> Suggest(string query, CancellationToken token);

    /// <summary>
    /// Queries the configured geocode client backend for geocode record based on the selected autocomplete record.
    /// Relies on the configured view for the <see cref="AddressSuggestionsParametersViewModel"/> as parameters to feed to geocode client request.
    /// </summary>
    /// <param name="record"></param>
    /// <param name="token"></param>
    /// <exception cref="OperationCanceledException">thrown when operation has been cancelled</exception>
    /// <exception cref="InvalidOperationException"> thrown when presenter is not initialized or when an invalid state occurs</exception>
    /// <exception cref="SuggestionsException">thrown when an error occurs when fetching suggestions</exception>
    /// <exception cref="InvalidSuggestionsParameterException">thrown when a parameter is set on the <see cref="AddressSuggestionsParametersViewModel"/> that is invalid</exception>
    /// <returns>A Task of <see cref="IGeocodeRecord"/></returns>
    public Task<IGeocodeRecord> SuggestionClicked(IAutocompleteRecord record, CancellationToken token);
}
