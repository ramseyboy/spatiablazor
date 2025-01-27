using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Abstractions.Descriptor;

namespace SpatiaBlazor.Geocode.Photon;

public class PhotonGeocodeRecordFactory(
    [FromKeyedServices(GeocodeExtensions.WGS85GeometryFactoryTag)]
    GeometryFactory geometryFactory,
    IDescriptorFactory descriptorFactory)
{
    private const string OsmIdAttribute = "osm_id";
    //unused for now
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

    public PhotonGeocodeRecord Empty { get; } = new()
    {
        Id = string.Empty,
        Descriptor = string.Empty,
        Geom = Point.Empty,
        BoundingBox = new Envelope(),
        IsValid = false
    };

    /// <summary>
    /// Creates a geocode record instance given the input value
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="ArgumentException">throws if the input value is invalid or missing required data</exception>
    /// <returns>A valid geocode record</returns>
    public PhotonGeocodeRecord Create(IFeature feature)
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

        ISet<string> typeSet = new HashSet<string>();
        if (!string.IsNullOrEmpty(locationType))
        {
            typeSet.Add(locationType);
        }

        Point geom;
        if (feature.Geometry is Point point)
        {
            geom = point;
        } else
        {
            // create point from centroid of geom, better than just grabbing the first/random vertex I reckon
            geom = feature.Geometry.Centroid;
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

        var record = new PhotonGeocodeRecord
        {
            Id = id,
            Geom = geom,
            BoundingBox = boundingBox,
            Name = attr.GetOptionalValue(NameAttribute)?.ToString(),
            Street = attr.GetOptionalValue(StreetAttribute)?.ToString(),
            HouseNumber = attr.GetOptionalValue(HouseNumberAttribute)?.ToString(),
            City = attr.GetOptionalValue(CityAttribute)?.ToString(),
            StateOrProvince = attr.GetOptionalValue(StateAttribute)?.ToString(),
            Country = attr.GetOptionalValue(CountryAttribute)?.ToString(),
            CountryCode = attr.GetOptionalValue(CountryCodeAttribute)?.ToString(),
            ZipOrPostCode = attr.GetOptionalValue(PostCodeAttribute)?.ToString(),
            Locality = attr.GetOptionalValue(LocalityAttribute)?.ToString(),
            CountyOrRegion = attr.GetOptionalValue(CountyAttribute)?.ToString(),
            Types = typeSet,
            IsValid = true
        };

        record.Descriptor = descriptorFactory.Create(record);
        return record;
    }
}
