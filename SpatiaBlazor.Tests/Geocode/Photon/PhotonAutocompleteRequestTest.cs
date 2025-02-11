using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Tests.Geocode.Photon;

public class PhotonAutocompleteRequestTest
{
    [Fact(DisplayName = "Given an autocomplete request query with a url fragment in it (#), When request is turned into request path, Then the resulting query string will have an encoded hash")]
    public void TestRequestPathWithFragmentAsQuery()
    {

        var request = new PhotonAutocompleteRequest()
        {
            Query = "10135 Smith Rd #105, NotAPlace, TX"
        };

        var path = request.ToRequestPath();
        Assert.NotEmpty(path);
        Assert.DoesNotContain("#", path);
        Assert.Equal("api?q=10135+Smith+Rd+%23105%2c+NotAPlace%2c+TX&limit=15", path);
    }
}
