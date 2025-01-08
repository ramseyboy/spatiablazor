using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SpatiaBlazor.Components.Address.Suggestions;
using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Address;

public record AddressFormViewModel: IViewModelMixin
{
    public AddressFormViewModel()
    {

    }

    public AddressFormViewModel(IGeocodeResultsViewModel geocode)
    {
        Id = geocode.Id;
        if (geocode.Name is not null)
        {
            Address1 = $"{geocode.Name}, {geocode.Street}";
        }
        else
        {
            Address1 = $"{geocode.HouseNumber} {geocode.Street}";
        }
        City = geocode.City;
        StateOrProvince = geocode.StateOrProvince;
        Country = geocode.CountryCode;
        Zip = geocode.ZipOrPostCode;
    }

    public object? Id { get; init; }
    [Required, DisplayName("Address Line 1")]
    public virtual string Address1 { get; set; }
    [DisplayName("Address Line 2")]
    public virtual string? Address2 { get; set; }
    [DisplayName("Address Line 3")]
    public virtual string? Address3 { get; set; }
    [Required]
    [DisplayName("City")]
    public virtual string City { get; set; }
    [Required]
    [DisplayName("State or Province")]
    public virtual string StateOrProvince { get; set; }
    [Required]
    [DisplayName("Country")]
    public virtual string Country { get; set; }
    [Required, MaxLength(9)]
    [DisplayName("Zip or Postal Code")]
    public virtual string Zip { get; set; }
    [DisplayName("Other Address Details")]
    public virtual string? OtherAddressDetails { get; set; }
}
