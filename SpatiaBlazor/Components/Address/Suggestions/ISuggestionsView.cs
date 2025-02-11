using SpatiaBlazor.Components.Mixins;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface ISuggestionsView : IViewMixin
{
    public AddressSuggestionsParametersViewModel SuggestionsParameters { get; set; }

    public IGeocodeRecord? Value { get; }

    void StartLoading();

    void StopLoading();
}
