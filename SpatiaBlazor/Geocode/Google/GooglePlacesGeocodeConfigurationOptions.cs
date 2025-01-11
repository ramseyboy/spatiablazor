using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpatiaBlazor.Geocode.Google;

public class GooglePlacesGeocodeConfigurationOptions
{
    private const string GoogleMapsApiBaseUri = "https://maps.googleapis.com/maps/api";

    [Required]
    public string ApiKey { get; set; }

    public string AutocompleteApiUrl { get; set; } = $"{GoogleMapsApiBaseUri}/place/autocomplete/json";

    public string ReverseGeocodeApiUrl { get; set; } = $"{GoogleMapsApiBaseUri}/geocode/json";
}
