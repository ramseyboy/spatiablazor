using System.Net.Http.Json;
using System.Text.Json;
using BlazorGeospatial.Geocode.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Features;

namespace SpatiaBlazor.Geocode.Photon;

public sealed class PhotonGeocodeClient(
    IHttpClientFactory httpClientFactory,
    ILogger<PhotonGeocodeClient> logger,
    IOptions<PhotonGeocodeConfigurationOptions> options)
    : IGeocodeClient<PhotonGeocodeResponse>
{
    public const string HttpClientTag = "PhotonGeocodeClient";

    private string BaseUrl => options.Value.ApiUrl;

    public async Task<IEnumerable<PhotonGeocodeResponse>> FromAddress(IAddressSuggestionsRequest request, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(HttpClientTag);
        httpClient.BaseAddress = new Uri(BaseUrl);

        var photonRequest = new PhotonAddressSuggestionsGeocodeRequest(request);

        try
        {
            var response = await httpClient.GetAsync(
                photonRequest.ToRequestPath(),
                cancellationToken: token);
            var responseBody = await response.Content.ReadAsStreamAsync(token);
            var collection = await JsonSerializer.DeserializeAsync<FeatureCollection>(
                responseBody,
                new JsonSerializerOptions
                {
                    Converters = {new NetTopologySuite.IO.Converters.GeoJsonConverterFactory()}
                },
                token);

            if (collection is null || collection.Count == 0)
            {
                logger.LogWarning("No results found");
                return [];
            }

            return collection
                .Select(feature =>
                {
                    try
                    {
                        return PhotonGeocodeResponse.Create(feature);
                    }
                    catch (ArgumentException e)
                    {
                        if (request.IgnoreErrors)
                        {
                            logger.LogError(e, "Encountered error when parsing feature for query {q}", request.Query);
                            return new PhotonGeocodeResponse
                            {
                                IsValid = false
                            };
                        }
                        else
                        {
                            throw;
                        }
                    }
                })
                .Where(x => x.IsValid)
                .ToList();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message, e);
        }
        return [];
    }

    public async Task<IEnumerable<PhotonGeocodeResponse>> FromPoint(IReverseGeocodeRequest request, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(HttpClientTag);
        httpClient.BaseAddress = new Uri(BaseUrl);

        var photonRequest = new PhotonReverseGeocodeRequest(request);

        var results = await httpClient.GetFromJsonAsync<IEnumerable<IGeocodeResponse>>(
            photonRequest.ToRequestPath(),
            new JsonSerializerOptions
            {
                Converters = {new NetTopologySuite.IO.Converters.GeoJsonConverterFactory()}
            },
            cancellationToken: token);

        //todo
        return [];
    }
}
