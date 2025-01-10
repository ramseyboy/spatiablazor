using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Address.Suggestions;

namespace SpatiaBlazor.Components.Address;

internal static class AddressExtensions
{
    public static AddressViewModel UpdateFromGeocode(this AddressViewModel viewModel, IGeocodeResultsViewModel geocode)
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
        viewModel.ZipOrPostCode = geocode.ZipOrPostCode;
        viewModel.InnerViewModel = geocode;
        return viewModel;
    }

    public static AddressViewModel Clear(this AddressViewModel viewModel)
    {
        viewModel.Address1 = string.Empty;
        viewModel.Address2 = null;
        viewModel.Address3 = null;
        viewModel.City = string.Empty;
        viewModel.StateOrProvince = string.Empty;
        viewModel.Country = string.Empty;
        viewModel.ZipOrPostCode = string.Empty;
        viewModel.OtherAddressDetails = null;
        viewModel.InnerViewModel = null;
        return viewModel;
    }
}
