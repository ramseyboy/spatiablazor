using System.Formats.Tar;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Address.Suggestions;
using SpatiaBlazor.Components.Attributes.Label;

namespace SpatiaBlazor.Components.Address;

internal static class AddressExtensions
{
    public static string Label(this AddressViewModel viewModel, ILabelFactory? labelFactory = null)
    {
        labelFactory ??= new DefaultLabelFactory();
        return labelFactory.Create(viewModel);
    }

    public static AddressViewModel UpdateFromGeocode(this AddressViewModel viewModel, IGeocodeResultsViewModel geocode, ILabelFactory? labelFactory = null)
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
        viewModel.Geom = geocode.Geom;

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
        viewModel.Geom = Point.Empty;
        return viewModel;
    }
}
