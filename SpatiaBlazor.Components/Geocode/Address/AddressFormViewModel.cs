using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Geocode.Address;

public class AddressFormViewModel: IViewModelMixin
{
    public object? Id { get; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string City { get; set; }
    public string StateOrProvince { get; set; }
    public string Country { get; set; }
    public string Zip { get; set; }
    public string OtherAddressDetails { get; set; }
}
