using System.Text.Json.Serialization;

namespace SpatiaBlazor.Geocode.Google.V1;

public record PlacesV1AutocompleteRecord
{
    [JsonPropertyName("predictions")]
    public IEnumerable<PlacesV1AutocompletePrediction> Predictions { get; set; } = [];
}

public record PlacesV1AutocompletePrediction
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("place_id")]
    public string PlaceId { get; set; } = string.Empty;
    [JsonPropertyName("types")]
    public IEnumerable<string> Types { get; set; } = [];
}
