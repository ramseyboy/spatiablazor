using System.Text.Json.Serialization;

namespace SpatiaBlazor.Geocode.Google.V1.Geocode;

public sealed record PlacesV1GeocodeRecord
{
    [JsonPropertyName("results")]
    public IEnumerable<PlacesV1GeocodeDetail> Results { get; set; } = [];
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

}

public sealed record PlacesV1GeocodeDetail
{
    [JsonPropertyName("address_components")]
    public IEnumerable<PlacesV1AddressComponent> AddressComponents { get; set; } = [];
    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; } = string.Empty;
    [JsonPropertyName("geometry")]
    public PlacesV1Geometry Geometry { get; set; } = new();
    [JsonPropertyName("place_id")]
    public string PlaceId { get; set; } = string.Empty;
    [JsonPropertyName("types")]
    public IEnumerable<string> Types { get; set; } = [];
}
