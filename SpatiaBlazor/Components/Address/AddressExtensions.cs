using System.Formats.Tar;
using System.Text;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Address.Suggestions;
using SpatiaBlazor.Components.Attributes.Label;

namespace SpatiaBlazor.Components.Address;

public static class AddressExtensions
{
    public static string Label(this AddressViewModel viewModel, ILabelFactory? labelFactory = null)
    {
        labelFactory ??= new DefaultLabelFactory();
        return labelFactory.Create(viewModel);
    }

    public static string Label(this IGeocodeResultsViewModel viewModel, ILabelFactory? labelFactory = null)
    {
        labelFactory ??= new DefaultLabelFactory();
        return labelFactory.Create(viewModel);
    }

    internal static void UpdateFromGeocode(this AddressViewModel viewModel, IGeocodeResultsViewModel geocode)
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(geocode.Name))
        {
            builder.Append(geocode.Name);
            if (!string.IsNullOrWhiteSpace(geocode.HouseNumber) || !string.IsNullOrWhiteSpace(geocode.Street))
            {
                builder.Append(',');
                builder.Append(' ');
            }
        }

        if (!string.IsNullOrWhiteSpace(geocode.HouseNumber))
        {
            builder.Append(geocode.HouseNumber);
            if (!string.IsNullOrWhiteSpace(geocode.Street))
            {
                builder.Append(' ');
            }
        }

        if (!string.IsNullOrWhiteSpace(geocode.Street))
        {
            builder.Append(geocode.Street);
        }

        viewModel.Address1 = builder.ToString();
        viewModel.City = geocode.City ?? string.Empty;
        viewModel.StateOrProvince = geocode.StateOrProvince ?? string.Empty;
        viewModel.Country = geocode.CountryCode ?? string.Empty;
        viewModel.ZipOrPostCode = geocode.ZipOrPostCode ?? string.Empty;
        viewModel.Geom = geocode.Geom;
    }

    internal static void Clear(this AddressViewModel viewModel)
    {
        viewModel.Address1 = string.Empty;
        viewModel.Address2 = null;
        viewModel.Address3 = null;
        viewModel.City = string.Empty;
        viewModel.StateOrProvince = string.Empty;
        viewModel.Country = string.Empty;
        viewModel.ZipOrPostCode = string.Empty;
        viewModel.OtherAddressDetails = null;
        viewModel.Geom = null;
    }
}
