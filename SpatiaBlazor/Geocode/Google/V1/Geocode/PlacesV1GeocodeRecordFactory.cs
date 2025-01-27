using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;
using static SpatiaBlazor.Geocode.Google.V1.PlacesV1AddressComponent;

namespace SpatiaBlazor.Geocode.Google.V1.Geocode;

public class PlacesV1GeocodeRecordFactory(
    [FromKeyedServices(GeocodeExtensions.WGS85GeometryFactoryTag)]
    GeometryFactory geometryFactory)
{
    public GoogleGeocodeRecord Empty { get; } = new()
    {
        Id = string.Empty,
        Descriptor = string.Empty,
        Geom = Point.Empty,
        BoundingBox = new Envelope()
    };

    /// <summary>
    /// Creates a geocode record instance given the input value
    /// </summary>
    /// <param name="detail"></param>
    /// <exception cref="ArgumentException">throws if the input value is invalid or missing required data</exception>
    /// <returns>A valid geocode record</returns>
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
