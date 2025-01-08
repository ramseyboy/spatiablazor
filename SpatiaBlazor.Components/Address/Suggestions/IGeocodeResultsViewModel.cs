using BlazorGeospatial.Geocode.Client;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface IGeocodeResultsViewModel: IViewModelMixin, IAddressRecord
{
    public Point Location { get; }
    public string Label { get; }
}
