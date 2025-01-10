using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpatiaBlazor.Geocode.Photon;

public class PhotonGeocodeConfigurationOptions
{
    [Required]
    public string ApiUrl { get; set; }
}
