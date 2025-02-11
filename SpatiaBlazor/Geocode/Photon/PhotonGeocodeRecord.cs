using System.Collections.Immutable;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Abstractions.Descriptor;

namespace SpatiaBlazor.Geocode.Photon;

public sealed record PhotonGeocodeRecord: IGeocodeRecord
{
    public required string Id { get; set; }
    public string Descriptor { get; set; } = string.Empty;
    public required Point Geom { get; set; }
    public required Envelope BoundingBox { get; set; }
    [DescriptorOrder(Order = 1, Delimiter = ",")]
    public string? Name { get; set; }
    [DescriptorOrder(Order = 2, Delimiter = " ")]
    public string? HouseNumber { get; set; }
    [DescriptorOrder(Order = 3, Delimiter = ",")]
    public string? Street { get; set; }
    public string? SubPremise { get; set; }
    [DescriptorOrder(Order = 4, Delimiter = ",")]
    public string? City { get; set; }
    [DescriptorOrder(Order = 5, Delimiter = ",")]
    public string? StateOrProvince { get; set; }
    [DescriptorOrder(Order = 6, Delimiter = ",")]
    public string? ZipOrPostCode { get; set; }
    [DescriptorOrder(Order = 7)]
    public string? CountryCode { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? Country { get; set; }
    public ISet<string> Types { get; init; } = new HashSet<string>();

    public bool IsValid { get; set; } = true;
}
