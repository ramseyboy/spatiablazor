using BlazorGeospatial.Geocode.Client;
using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Google;

public class GooglePlacesGeocodeRecord: IGeocodeRecord
{
    public string Id { get; set; }
    public Geometry Geom { get; set; }
    public Envelope BoundingBox { get; set; }
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? City { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? StateOrProvince { get; set; }
    public string? Country { get; set; }
    public string? CountryCode { get; set; }
    public string? ZipOrPostCode { get; set; }
    public ISet<string> Types { get; }
}
