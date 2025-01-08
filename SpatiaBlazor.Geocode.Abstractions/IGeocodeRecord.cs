using NetTopologySuite.Geometries;

namespace BlazorGeospatial.Geocode.Client;

public interface IGeocodeRecord: IAddressRecord
{
    public string Id { get; set; }
    public Geometry Geom { get; set; }
    public Envelope BoundingBox { get; set; }
    public ISet<string> Types { get; }
}
