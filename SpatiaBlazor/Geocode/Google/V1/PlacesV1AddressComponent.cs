using System.Text.Json.Serialization;

namespace SpatiaBlazor.Geocode.Google.V1;

public sealed record PlacesV1AddressComponent
{
    [JsonPropertyName("long_name")]
    public string LongName { get; set; } = string.Empty;
    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = string.Empty;
    [JsonPropertyName("types")]
    public IEnumerable<string> Types { get; set; } = [];
}
