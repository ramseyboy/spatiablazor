using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Google.V1;

namespace SpatiaBlazor.Geocode.Google;

public class GoogleGeocodeRecordFactory(
    [FromKeyedServices(GeocodeExtensions.WGS85GeometryFactoryTag)]
    GeometryFactory geometryFactory) :
    IGeocodeRecordFactory<PlacesV1GeocodeDetail, GoogleGeocodeRecord>
{
    private const string StateAttribute = "administrative_area_level_1";
    private const string CityAttribute = "locality";
    private const string CountryAttribute = "country";
    private const string HouseNumberAttribute = "street_number";
    private const string PostCodeAttribute = "postal_code";
    private const string StreetAttribute = "route";
    private const string LocalityAttribute = "neighborhood";
    private const string CountyAttribute = "administrative_area_level_2";

    public GoogleGeocodeRecord Empty { get; } = new()
    {
        Id = string.Empty,
        Descriptor = string.Empty,
        Geom = Point.Empty,
        BoundingBox = new Envelope()
    };

    public GoogleGeocodeRecord Create(PlacesV1GeocodeDetail detail)
    {
        var addressMap = detail.AddressComponents
            .Where(x => x.Types.Any())
            .ToDictionary(
                x => x.Types.First(),
                x => x);

        var location = detail.Geometry.Location;
        var point = geometryFactory.CreatePoint(new Coordinate(location.Lng, location.Lat));

        var viewport = detail.Geometry.Bounds;
        Envelope boundingBox;
        if (viewport is {SouthWest: not null, NorthEast: not null})
        {
            var coordinateSequence = geometryFactory.CoordinateSequenceFactory.Create(
            [
                new Coordinate(viewport.SouthWest.Lng, viewport.SouthWest.Lat),
                new Coordinate(viewport.NorthEast.Lng, viewport.NorthEast.Lat)
            ]);
            boundingBox = new Envelope(coordinateSequence);
        }
        else
        {
            //default to a envelope surrounding the point
            boundingBox = new Envelope(point.Coordinates);
        }

        return new GoogleGeocodeRecord
        {
            Id = detail.PlaceId,
            Descriptor = detail.FormattedAddress,
            Geom = point,
            BoundingBox = boundingBox,
            HouseNumber = addressMap.GetValueOrDefault(HouseNumberAttribute)?.ShortName,
            Street = addressMap.GetValueOrDefault(StreetAttribute)?.ShortName,
            Locality = addressMap.GetValueOrDefault(LocalityAttribute)?.ShortName,
            City = addressMap.GetValueOrDefault(CityAttribute)?.ShortName,
            CountyOrRegion = addressMap.GetValueOrDefault(CountyAttribute)?.ShortName,
            StateOrProvince = addressMap.GetValueOrDefault(StateAttribute)?.ShortName,
            CountryCode = addressMap.GetValueOrDefault(CountryAttribute)?.ShortName,
            Country = addressMap.GetValueOrDefault(CountryAttribute)?.LongName,
            ZipOrPostCode = addressMap.GetValueOrDefault(PostCodeAttribute)?.ShortName,
            Types = detail.Types.ToHashSet()
        };
    }
}
