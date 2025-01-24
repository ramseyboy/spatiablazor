using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions.Descriptor;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Tests.Geocode.Photon;

public class PhotonGeocodeRecordTest
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

        var descriptorFactory = new AttributeOrderDescriptorFactory();
        response.Descriptor = descriptorFactory.Create(response);

        Assert.NotNull(response.Descriptor);
        Assert.NotEmpty(response.Descriptor);
        Assert.Equal($"{response.Name}, {response.HouseNumber} {response.Street}, {response.City}, {response.StateOrProvince}, {response.ZipOrPostCode}, {response.CountryCode}", response.Descriptor);
    }
}
