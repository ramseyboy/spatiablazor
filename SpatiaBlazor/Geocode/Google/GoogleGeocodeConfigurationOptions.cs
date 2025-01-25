using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpatiaBlazor.Geocode.Google;

public class GoogleGeocodeConfigurationOptions
{
    private const string GoogleMapsApiBaseUri = "https://maps.googleapis.com/maps/api";

    [Required]
    public string ApiKey { get; set; }

    /// <summary>
    /// Places v2 API is not implemented yet
    /// </summary>
    public bool UseV2PlacesApi { get; set; } = false;

    public string AutocompleteApiUrl { get; } = $"{GoogleMapsApiBaseUri}/place/autocomplete/json";

    public string PlaceDetailApiUrl { get; } = $"{GoogleMapsApiBaseUri}/place/details/json";

    public string GeocodeApiUrl { get; } = $"{GoogleMapsApiBaseUri}/geocode/json";
}
