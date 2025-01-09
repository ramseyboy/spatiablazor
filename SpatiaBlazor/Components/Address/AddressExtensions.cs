using SpatiaBlazor.Components.Address.Suggestions;

namespace SpatiaBlazor.Components.Address;

internal static class AddressExtensions
{
    public static AddressFormViewModel UpdateFromGeocode(this AddressFormViewModel viewModel, IGeocodeResultsViewModel geocode)
    {
        if (geocode.Name is not null)
        {
            viewModel.Address1 = $"{geocode.Name}, {geocode.Street}";
        }
        else
        {
            viewModel.Address1 = $"{geocode.HouseNumber} {geocode.Street}";
        }
        viewModel.City = geocode.City;
        viewModel.StateOrProvince = geocode.StateOrProvince;
        viewModel.Country = geocode.CountryCode;
        viewModel.Zip = geocode.ZipOrPostCode;
        return viewModel;
    }
}
