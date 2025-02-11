using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Validation;

namespace SpatiaBlazor.Components.Address.Suggestions;

internal sealed class DefaultAddressSuggestionsPresenter(IGeocodeClient suggestionsClient) : IAddressSuggestionsPresenter
{
    private ISuggestionsView? _view;

    public IAutocompleteRecord? SelectedAutocompleteValue { get; private set; }

    /// <inheritdoc />>
    public Task InitializeAsync(ISuggestionsView view, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        _view = view ?? throw new InvalidOperationException("View is null, must call 'InitializeAsync(View)' with a non-null view");
        return Task.CompletedTask;
    }

    /// <inheritdoc />>
    public Task HandleParametersAsync(CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        if (_view is null)
        {
            throw new InvalidOperationException("Presenter is uninitialized, must call 'InitializeAsync(View)' first");
        }

        SelectedAutocompleteValue = _view.Value;
        return Task.CompletedTask;
    }

    /// <inheritdoc />>
    public async Task<IEnumerable<IAutocompleteRecord>> Suggest(string query, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        if (_view is null)
        {
            throw new InvalidOperationException("Presenter is uninitialized, must call 'InitializeAsync(View)' first");
        }

        var viewModel = _view.SuggestionsParameters;

        viewModel.Query = query;

        var validator = new ComponentModelValidator();
        var validationResults = validator.Validate(viewModel);

        if (validationResults.Count != 0)
        {
            throw new InvalidSuggestionsParameterException($"Invalid parameters {validationResults.ToParameterList()}, cannot request suggestions",
                validationResults.ToDictionary());
        }

        try
        {
            _view.StartLoading();
            var suggestions = await suggestionsClient.Autocomplete(viewModel, token);
            return suggestions;
        }
        catch (GeocodeException e)
        {
            throw new SuggestionsException("An error occurred when requesting address suggestions, please try again", e);
        }
        finally
        {
            _view.StopLoading();
        }
    }

    /// <inheritdoc />>
    public async Task<IGeocodeRecord> SuggestionClicked(IAutocompleteRecord record, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        if (_view is null)
        {
            throw new InvalidOperationException("Presenter is uninitialized, must call 'InitializeAsync(View)' first");
        }

        var viewModel = _view.SuggestionsParameters;
        try
        {
            _view.StartLoading();
            var geocodeRecords = await suggestionsClient.Geocode(record, viewModel, token);
            //todo: show dialog to accept record if multiple or show error if none
            var first = geocodeRecords?.FirstOrDefault();
            if (first is null)
            {
                throw new SuggestionsException("An error occurred when requesting address suggestions, no matching address found for selection");
            }

            return first;
        }
        catch (GeocodeException e)
        {
            throw new SuggestionsException("An error occurred when requesting address suggestions, please try again", e);
        }
        finally
        {
            _view.StopLoading();
        }
    }

    /// <inheritdoc />>
    public void Dispose()
    {
        _view = null;
    }
}
