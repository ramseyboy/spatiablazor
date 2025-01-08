using BlazorGeospatial.Geocode.Client;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Geocode.Suggestions.Label;
using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Geocode.Suggestions;

public interface IGeocodeResultsViewModel: IViewModelMixin, IAddressRecord
{
    public Point Location { get; }
    public string Label { get; }
}
