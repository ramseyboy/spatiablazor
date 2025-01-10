using System.Collections.Immutable;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public record PhotonGeocodeRecord: IGeocodeRecord
{
    //unused for now
    private const string OsmIdAttribute = "osm_id";
    private const string OsmKeyAttribute = "osm_key";
    private const string OsmValueAttribute = "osm_value";
    private const string OsmTypeAttribute = "osm_type";

    private const string TypeAttribute = "type";
    private const string ExtentAttribute = "extent";

    private const string NameAttribute = "name";
    private const string StateAttribute = "state";
    private const string CityAttribute = "city";
    private const string CountryAttribute = "country";
    private const string CountryCodeAttribute = "countrycode";
    private const string HouseNumberAttribute = "housenumber";
    private const string PostCodeAttribute = "postcode";
    private const string StreetAttribute = "street";
    private const string LocalityAttribute = "locality";
    private const string CountyAttribute = "county";

    public PhotonGeocodeRecord()
    {

    }

    public PhotonGeocodeRecord(IFeature feature)
    {
        if (feature.Geometry is null || feature.Geometry.IsEmpty || feature.Attributes is null)
        {
            throw new ArgumentException("Feature geometry is missing and/or attributes are missing, cannot parse response");
        }

        var attr = feature.Attributes;

        var id = attr.GetOptionalValue(OsmIdAttribute)?.ToString();
        if (id is null)
        {
            throw new ArgumentException("Feature is malformed, cannot locate unique identifier");
        }

        var locationType = attr.GetOptionalValue(TypeAttribute)?.ToString();

        ISet<string> typeSet = ImmutableHashSet<string>.Empty;
        if (!string.IsNullOrEmpty(locationType))
        {
            typeSet = new HashSet<string>
            {
                locationType
            };
        }

        var boundingBox = new Envelope(feature.Geometry.Coordinates);
        if (feature.BoundingBox is not null && !feature.BoundingBox.IsNull)
        {
            boundingBox = feature.BoundingBox;
        } else if (attr.GetOptionalValue(ExtentAttribute) is object[] envelope)
        {
            var parsedEnvelope = envelope
                .Select(x =>
                {
                    var result = double.TryParse(x?.ToString(), out var dub);
                    if (result)
                    {
                        return dub as double?;
                    }

                    return null;
                })
                .Where(x => x is not null)
                .Cast<double>()
                .ToArray();

            if (parsedEnvelope.Length == 4)
            {
                boundingBox = new Envelope(parsedEnvelope[0], parsedEnvelope[2], parsedEnvelope[1], parsedEnvelope[3]);
            }
        }

        Id = id;
        Geom = feature.Geometry;
        BoundingBox = boundingBox;
        Name = attr.GetOptionalValue(NameAttribute)?.ToString();
        Street = attr.GetOptionalValue(StreetAttribute)?.ToString();
        HouseNumber = attr.GetOptionalValue(HouseNumberAttribute)?.ToString();
        City = attr.GetOptionalValue(CityAttribute)?.ToString();
        StateOrProvince = attr.GetOptionalValue(StateAttribute)?.ToString();
        Country = attr.GetOptionalValue(CountryAttribute)?.ToString();
        CountryCode = attr.GetOptionalValue(CountryCodeAttribute)?.ToString();
        ZipOrPostCode = attr.GetOptionalValue(PostCodeAttribute)?.ToString();
        Locality = attr.GetOptionalValue(LocalityAttribute)?.ToString();
        CountyOrRegion = attr.GetOptionalValue(CountyAttribute)?.ToString();
        Types = typeSet;
    }

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
    public ISet<string> Types { get; private set; } = ImmutableHashSet<string>.Empty;

    public bool IsValid { get; set; } = true;
}
