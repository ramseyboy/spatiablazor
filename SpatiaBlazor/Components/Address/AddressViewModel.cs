using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Address.Suggestions;

namespace SpatiaBlazor.Components.Address;

public record AddressViewModel
{
    [StringLength(256, ErrorMessage = "Address Line 1 must not exceed 256 characters.")]
    [Required(AllowEmptyStrings = false)]
    [Display(Name = "Address Line 1")]
    [Editable(false)]
    public virtual string Address1 { get; set; } = string.Empty;

    [StringLength(256, ErrorMessage = "Address Line 2 must not exceed 256 characters.")]
    [Display(Name = "Address Line 2", AutoGenerateField = true)]
    [Editable(true)]
    public virtual string? Address2 { get; set; }

    [StringLength(256, ErrorMessage = "Address Line 3 must not exceed 256 characters. ")]
    [Display(Name = "Address Line 3", AutoGenerateField = false)]
    [Editable(true)]
    public virtual string? Address3 { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Display(Name = "City")]
    [Editable(false)]
    [StringLength(128, ErrorMessage = "City or Municipality must not exceed 128 characters. ")]
    public virtual string City { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Display(Name = "State or Province")]
    [Editable(false)]
    [StringLength(128, ErrorMessage = "State or Province must not exceed 128 characters. ")]
    public virtual string StateOrProvince { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Display(Name = "Country")]
    [Editable(false)]
    [StringLength(128, ErrorMessage = "Country must not exceed 128 characters. ")]
    public virtual string Country { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [StringLength(9, ErrorMessage = "Zip or Postal Code must not exceed 9 characters. ")]
    [Display(Name = "Zip or Postal Code")]
    [Editable(false)]
    public virtual string ZipOrPostCode { get; set; } = string.Empty;

    [StringLength(1024, ErrorMessage = "Details must not exceed 1024 characters. ")]
    [Display(Name = "Other Address Details")]
    [Editable(true)]
    public virtual string? OtherAddressDetails { get; set; }

    public IGeocodeResultsViewModel? InnerViewModel { get; set; }
}
