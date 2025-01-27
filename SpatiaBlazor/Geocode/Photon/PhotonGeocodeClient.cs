using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Features;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public sealed class PhotonGeocodeClient(
    IHttpClientFactory httpClientFactory,
    ILogger<PhotonGeocodeClient> logger,
    IOptions<PhotonGeocodeConfigurationOptions> options,
    PhotonGeocodeRecordFactory recordFactory)
    : IGeocodeClient
{
    private string BaseUrl => options.Value.ApiUrl;

    public async Task<IEnumerable<IAutocompleteRecord>> Autocomplete(IAutocompleteRequest request, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(GeocodeExtensions.HttpClientTag);
        httpClient.BaseAddress = new Uri(BaseUrl);

        var photonRequest = new PhotonAutocompleteRequest(request);
        //todo: validation of photon specific parameter values

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
                        return recordFactory.Create(feature);
                    }
                    catch (ArgumentException e)
                    {
                        if (!request.IgnoreErrors)
                        {
                            throw;
                        }

                        logger.LogError(e, "Encountered error when parsing feature for query {q}", request.Query);
                        return recordFactory.Empty;

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

    public async Task<IEnumerable<IGeocodeRecord>> Geocode(IAutocompleteRecord result, IGeocodeRequest request, CancellationToken token = default)
    {
        if (result is PhotonGeocodeRecord record)
        {
            return [record];
        }

        var autoCompleteRequest = new PhotonAutocompleteRequest
        {
            Query = result.Descriptor,
            BoundingBox = request.BoundingBox,
            Language = request.Language,
            Region = request.Region,
            TypeFilters = request.TypeFilters
        };

        var results = await Autocomplete(autoCompleteRequest, token);
        return results.Cast<PhotonGeocodeRecord>();
    }
}
