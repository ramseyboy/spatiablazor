using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Validation;

namespace SpatiaBlazor.Components.Address.Suggestions;

internal sealed class DefaultAddressSuggestionsPresenter(IGeocodeClient suggestionsClient) : IAddressSuggestionsPresenter
{
    private ISuggestionsView? _view;

    /// <inheritdoc />>
    public Task InitializeAsync(ISuggestionsView view, CancellationToken cancellationToken = default)
    {
        _view = view;
        return Task.CompletedTask;
    }

    /// <inheritdoc />>
    public async Task<IEnumerable<IAutocompleteRecord>> Suggest(CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return [];
        }

        if (_view is null)
        {
            return [];
        }

        var viewModel = _view.SuggestionsParameters;

        var validator = new ComponentModelValidator();
        var validationResults = validator.Validate(viewModel);

        if (validationResults.Count != 0)
        {
            throw new InvalidSuggestionsParameterException($"Invalid parameters {validationResults.ToParameterList()}, cannot fetch suggestions",
                validationResults.ToDictionary());
        }

        var suggestions = await suggestionsClient.Autocomplete(_view.SuggestionsParameters, token);
        return suggestions;
    }

    public async Task<IGeocodeRecord> SuggestionClicked(IAutocompleteRecord record, CancellationToken token)
    {
        if (_view is null)
        {
            //todo
            throw new ApplicationException();
        }

        var viewModel = _view.SuggestionsParameters;
        var geocodeRecords = await suggestionsClient.Geocode(record, viewModel, token);
        //todo: show dialog to accept record if multiple or show error if none
        return geocodeRecords.First();
    }

    public void Dispose()
    {
        _view = null;
    }
}
