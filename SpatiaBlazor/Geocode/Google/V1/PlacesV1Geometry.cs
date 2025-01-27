using System.Text.Json.Serialization;

namespace SpatiaBlazor.Geocode.Google.V1;

public sealed record PlacesV1Geometry
{
    [JsonPropertyName("bounds")]
    public PlacesV1Bounds? Bounds { get; set; }
    [JsonPropertyName("location")]
    public PlacesV1Location Location { get; set; } = new();
    [JsonPropertyName("location_type")]
    public string? LocationType { get; set; }
    [JsonPropertyName("viewport")]
    public PlacesV1Bounds? Viewport { get; set; }

    [JsonPropertyName("navigation_points")]
    public IEnumerable<PlacesV1Location> NavigationPoints { get; set; } = [];
}

public sealed record PlacesV1Location
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}

public sealed record PlacesV1Bounds
{
    [JsonPropertyName("northeast")]
    public PlacesV1Location? NorthEast { get; set; }
    [JsonPropertyName("southwest")]
    public PlacesV1Location? SouthWest { get; set; }
}
