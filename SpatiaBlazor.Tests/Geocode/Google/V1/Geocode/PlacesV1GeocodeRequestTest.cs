using SpatiaBlazor.Geocode.Google.V1.Geocode;

namespace SpatiaBlazor.Tests.Geocode.Google.V1.Geocode;

public class PlacesV1GeocodeRequestTest
{
    [Fact(DisplayName = "Given an geocode request input with a url fragment in it (#), When request is turned into request path, Then the resulting query string will have an encoded hash")]
    public void TestRequestPathWithFragmentAsQuery()
    {

        var request = new PlacesV1GeocodeRequest()
        {
            Query = "10135 Smith Rd #105, NotAPlace, TX"
        };

        var path = request.ToRequestPath();
        Assert.NotEmpty(path);
        Assert.DoesNotContain("#", path);
        Assert.Equal("?address=10135+Smith+Rd+%23105%2c+NotAPlace%2c+TX", path);
    }
}
