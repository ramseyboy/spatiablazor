using System.Collections.Immutable;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google;

public sealed record GoogleGeocodeRecord : IGeocodeRecord
{
    public required string Id { get; set; }
    public string Descriptor { get; set; } = string.Empty;
    public required Point Geom { get; set; }
    public required Envelope BoundingBox { get; set; }
    public string? Name { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? SubPremise { get; set; }
    public string? City { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? StateOrProvince { get; set; }
    public string? Country { get; set; }
    public string? CountryCode { get; set; }
    public string? ZipOrPostCode { get; set; }
    public ISet<string> Types { get; init; } = new HashSet<string>();
}
