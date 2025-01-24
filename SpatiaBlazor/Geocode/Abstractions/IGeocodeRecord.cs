using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Abstractions;

public interface IGeocodeRecord: IAutocompleteRecord, IAddressRecord
{
    public Point Geom { get; set; }
    public Envelope BoundingBox { get; set; }
}
