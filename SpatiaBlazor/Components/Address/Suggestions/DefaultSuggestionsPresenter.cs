using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Components.Address.Suggestions;

internal sealed class DefaultSuggestionsPresenter(IGeocodeClient suggestionsClient) : ISuggestionsPresenter
{
    private ISuggestionsView? _view;

    public SuggestionsViewModel ViewModel { get; set; } = new();

    public Task InitializeAsync(ISuggestionsView view, CancellationToken cancellationToken = default)
    {
        _view = view;
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<IGeocodeResultsViewModel>> AutocompleteSuggestions(string query, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return [];
        }

        if (string.IsNullOrEmpty(query))
        {
            return [];
        }

        var request = new PhotonAutocompleteRequest
        {
            Query = query
        };
        var suggestions = await suggestionsClient.FromAddress(request, token);
        return suggestions
            .Select(x => new DefaultGeocodeResultsViewModel(x))
            .ToList();
    }

    public void Dispose()
    {
        _view = null;
    }
}
