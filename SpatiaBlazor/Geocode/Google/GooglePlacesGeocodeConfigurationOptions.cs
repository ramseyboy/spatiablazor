using System.ComponentModel.DataAnnotations;

namespace SpatiaBlazor.Geocode.Google;

public class GooglePlacesGeocodeConfigurationOptions
{
    public const string OptionsPath = "SpatiaBlazor:Geocode:GooglePlaces";

    [Required]
    public string ApiKey { get; set; }
    [Required]
    public string ApiUrl { get; set; }
}
