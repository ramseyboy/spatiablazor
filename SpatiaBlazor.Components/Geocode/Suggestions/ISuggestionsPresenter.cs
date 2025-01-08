using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Geocode.Suggestions;

public interface ISuggestionsPresenter: IPresenterMixin<ISuggestionsView>
{
    public SuggestionsViewModel ViewModel { get; set; }

    public Task<IEnumerable<IGeocodeResultsViewModel>> AutocompleteSuggestions(string query, CancellationToken token);
}
