using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Validation;

namespace SpatiaBlazor.Components.Address.Suggestions;

internal sealed class DefaultSuggestionsPresenter(IGeocodeClient suggestionsClient) : ISuggestionsPresenter
{
    private ISuggestionsView? _view;

    /// <inheritdoc />>
    public Task InitializeAsync(ISuggestionsView view, CancellationToken cancellationToken = default)
    {
        _view = view;
        return Task.CompletedTask;
    }

    /// <inheritdoc />>
    public async Task<IEnumerable<IGeocodeResultsViewModel>> AutocompleteSuggestions(CancellationToken token)
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

        var suggestions = await suggestionsClient.FromAddress(_view.SuggestionsParameters, token);
        return suggestions
            .Select(x => new DefaultGeocodeResultsViewModel(x))
            .ToList();
    }

    public void Dispose()
    {
        _view = null;
    }
}
