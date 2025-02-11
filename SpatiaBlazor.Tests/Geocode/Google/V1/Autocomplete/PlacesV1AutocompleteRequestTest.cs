using SpatiaBlazor.Geocode.Google;
using SpatiaBlazor.Geocode.Google.V1.Autocomplete;

namespace SpatiaBlazor.Tests.Geocode.Google.V1.Autocomplete;

public class PlacesV1AutocompleteRequestTest
{
    [Fact(DisplayName = "Given an autocomplete request input with a url fragment in it (#), When request is turned into request path, Then the resulting query string will have an encoded hash")]
    public void TestRequestPathWithFragmentAsQuery()
    {

        var options = new GoogleGeocodeConfigurationOptions();
        var request = new PlacesV1AutocompleteRequest(options)
        {
            Query = "10135 Smith Rd #105, NotAPlace, TX"
        };

        var path = request.ToRequestPath();
        Assert.NotEmpty(path);
        Assert.DoesNotContain("#", path);
        Assert.Equal("?input=10135+Smith+Rd+%23105%2c+NotAPlace%2c+TX&strictbounds=false&fields=geocode", path);
    }
}
