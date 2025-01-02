using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpatiaBlazor.Geocode.Photon;

public class PhotonGeocodeConfigurationOptions
{
    public const string OptionsPath = "SpatiaBlazor:Geocode:Photon";
    [Required]
    public string ApiUrl { get; set; }
}
