using System.Text.Json.Serialization;

namespace SpatiaBlazor.Geocode.Google.V1;

public sealed record PlacesV1DetailRecord
{
    [JsonPropertyName("result")]
    public PlacesV1PlaceDetail? Result { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

public sealed record PlacesV1PlaceDetail
{
    [JsonPropertyName("address_components")]
    public IEnumerable<PlacesV1AddressComponent> AddressComponents { get; set; } = [];
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("geometry")]
    public PlacesV1Geometry Geometry { get; set; } = new();
}
