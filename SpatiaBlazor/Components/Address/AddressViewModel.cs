using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Address.Suggestions;
using SpatiaBlazor.Components.Attributes.Label;

namespace SpatiaBlazor.Components.Address;

public record AddressViewModel
{
    public Point Geom { get; set; } = Point.Empty;

    [StringLength(256, ErrorMessage = "Address Line 1 must not exceed 256 characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
    [Display(Name = "Address Line 1")]
    [Editable(false)]
    [LabelOrder(Order = 1, Delimiter = ",")]
    public virtual string Address1 { get; set; } = string.Empty;

    [StringLength(256, ErrorMessage = "Address Line 2 must not exceed 256 characters.")]
    [Display(Name = "Address Line 2", AutoGenerateField = true)]
    [Editable(true)]
    [LabelOrder(Order = 2, Delimiter = ",")]
    public virtual string? Address2 { get; set; }

    [StringLength(256, ErrorMessage = "Address Line 3 must not exceed 256 characters. ")]
    [Display(Name = "Address Line 3", AutoGenerateField = true)]
    [Editable(true)]
    [LabelOrder(Order = 3, Delimiter = ",")]
    public virtual string? Address3 { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "City is required")]
    [Display(Name = "City")]
    [Editable(false)]
    [StringLength(128, ErrorMessage = "City or Municipality must not exceed 128 characters. ")]
    [LabelOrder(Order = 4, Delimiter = ",")]
    public virtual string City { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "State or province is required")]
    [Display(Name = "State or Province")]
    [Editable(false)]
    [StringLength(128, ErrorMessage = "State or Province must not exceed 128 characters. ")]
    [LabelOrder(Order = 5, Delimiter = ",")]
    public virtual string StateOrProvince { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required")]
    [Display(Name = "Country")]
    [Editable(false)]
    [StringLength(128, ErrorMessage = "Country must not exceed 128 characters. ")]
    [LabelOrder(Order = 6, Delimiter = ",")]
    public virtual string Country { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Zip or postal code is required")]
    [StringLength(9, ErrorMessage = "Zip or Postal Code must not exceed 9 characters. ")]
    [Display(Name = "Zip or Postal Code")]
    [Editable(false)]
    [LabelOrder(Order = 7, Delimiter = ",")]
    public virtual string ZipOrPostCode { get; set; } = string.Empty;

    [StringLength(1024, ErrorMessage = "Details must not exceed 1024 characters. ")]
    [Display(Name = "Other Address Details")]
    [Editable(true)]
    public virtual string? OtherAddressDetails { get; set; }
}
