using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Geocode.Suggestions;

public interface IGeocodeResultsViewModel: IViewModelMixin
{
    public Point Location { get; }
    public string Label { get; set; }
}
