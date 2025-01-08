using BlazorGeospatial.Geocode.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SpatiaBlazor.Geocode.Google;

public sealed class GooglePlacesGeocodeClient(
    IHttpClientFactory httpClientFactory,
    ILogger<GooglePlacesGeocodeClient> logger,
    IOptions<GooglePlacesGeocodeConfigurationOptions> options)
    : IGeocodeClient<GooglePlacesGeocodeRecord>
{
    public const string HttpClientTag = "GooglePlacesGeocodeService";

    public Task<IEnumerable<GooglePlacesGeocodeRecord>> FromAddress(IAddressSuggestionsRequest request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GooglePlacesGeocodeRecord>> FromPoint(IReverseGeocodeRequest request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
