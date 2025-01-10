using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Address.Suggestions;
using SpatiaBlazor.Components.Attributes.Label;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Tests.Components.Address.Photon;

public class DefaultGeocodeResultsViewModelTest
{
    [Fact(DisplayName = "Given a valid non empty structured address, When view model is created, Then label should match the address data")]
    public void TestLabelWithAllNull()
    {
        var response = new PhotonGeocodeRecord()
        {
            Id = "1",
            Name = "Q2 Stadium",
            HouseNumber = "123",
            Street = "Verde Way",
            City = "Austin",
            ZipOrPostCode = "78758",
            StateOrProvince = "Texas",
            CountryCode = "US",
            Geom = new Point(0,0)
        };
        var viewModel = new DefaultGeocodeResultsViewModel(response, new DefaultLabelFactory());

        Assert.NotNull(viewModel.Label);
        Assert.NotEmpty(viewModel.Label);
        Assert.Equal($"{viewModel.Name}, {viewModel.HouseNumber} {viewModel.Street}, {viewModel.City}, {viewModel.StateOrProvince}, {viewModel.ZipOrPostCode}, {viewModel.CountryCode}", viewModel.Label);
    }
}
