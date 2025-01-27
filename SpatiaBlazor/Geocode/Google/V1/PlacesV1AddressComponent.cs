using System.Text.Json.Serialization;

namespace SpatiaBlazor.Geocode.Google.V1;

public sealed record PlacesV1AddressComponent
{
    public const string StateAttribute = "administrative_area_level_1";
    public const string CityAttribute = "locality";
    public const string CountryAttribute = "country";
    public const string HouseNumberAttribute = "street_number";
    public const string PostCodeAttribute = "postal_code";
    public const string StreetAttribute = "route";
    public const string LocalityAttribute = "neighborhood";
    public const string CountyAttribute = "administrative_area_level_2";

    [JsonPropertyName("long_name")]
    public string LongName { get; set; } = string.Empty;
    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = string.Empty;
    [JsonPropertyName("types")]
    public IEnumerable<string> Types { get; set; } = [];
}
