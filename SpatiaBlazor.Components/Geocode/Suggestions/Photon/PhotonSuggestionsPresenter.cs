using BlazorGeospatial.Geocode.Client;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Components.Geocode.Suggestions.Photon;

public class PhotonSuggestionsPresenter(IGeocodeClient<PhotonGeocodeRecord> suggestionsClient) : ISuggestionsPresenter
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
        //todo put request args into UI form and map for reverse lookup
        //todo default extent as rect
        //todo default extent as polygon
        //todo allow drawing/picking points to use as bias (or center circle extent)
        //todo allow drawing rect extent
        //todo arcgis implementation along with google, photon and custom
        //todo Map view for syep eligibilities/jobs that show distance, transit stops, roads, interest heatmap, schedule heatmap
        if (token.IsCancellationRequested)
        {
            return [];
        }

        if (string.IsNullOrEmpty(query))
        {
            return [];
        }

        var request = new DefaultAddressSuggestionsGeocodeRequest
        {
            Query = query
        };
        var suggestions = await suggestionsClient.FromAddress(request, token);
        return suggestions
            .Select(x => new PhotonGeocodeResultsViewModel(x))
            .ToList();
    }

    public void Dispose()
    {
        _view = null;
    }
}
