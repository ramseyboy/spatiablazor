using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Mixins;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface IGeocodeResultsViewModel: IAddressRecord
{
    public Point Geom { get; }
}
