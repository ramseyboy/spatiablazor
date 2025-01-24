using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google;

public sealed class GooglePlacesGeocodeClient(
    IHttpClientFactory httpClientFactory,
    ILogger<GooglePlacesGeocodeClient> logger,
    IOptions<GooglePlacesGeocodeConfigurationOptions> options)
    : IGeocodeClient
{
    public const string HttpClientTag = "GooglePlacesGeocodeService";

    public Task<IEnumerable<IAutocompleteRecord>> Autocomplete(IAutocompleteRequest request, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IGeocodeRecord>> Geocode(IAutocompleteRecord result, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
