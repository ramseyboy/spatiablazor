using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Google.V1;

namespace SpatiaBlazor.Geocode.Google;

public sealed class GoogleGeocodeClient(
    IHttpClientFactory httpClientFactory,
    ILogger<GoogleGeocodeClient> logger,
    IOptions<GoogleGeocodeConfigurationOptions> options,
    IGeocodeRecordFactory<PlacesV1GeocodeDetail, GoogleGeocodeRecord> recordFactory)
    : IGeocodeClient
{
    public async Task<IEnumerable<IAutocompleteRecord>> Autocomplete(IAutocompleteRequest request, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(GeocodeExtensions.HttpClientTag);
        httpClient.BaseAddress = new Uri(options.Value.AutocompleteApiUrl);

        var googleRequest = new PlacesV1AutocompleteRequest(request);
        //todo: validation of google specific parameter values

        var response = await httpClient.GetAsync(
            $"{googleRequest.ToRequestPath()}&key={options.Value.ApiKey}",
            cancellationToken: token);
        var responseBody = await response.Content.ReadAsStreamAsync(token);
        var rawRecord = await JsonSerializer.DeserializeAsync<PlacesV1AutocompleteRecord>(
            responseBody,
            cancellationToken: token);

        if (rawRecord is null)
        {
            logger.LogWarning("Could not deserialize google places v1 autocomplete json response or response was null or empty");
            return [];
        }

        return rawRecord.Predictions
            .Select(x => new GoogleAutocompleteRecord(x))
            .ToList();
    }

    public async Task<IEnumerable<IGeocodeRecord>> Geocode(IAutocompleteRecord result, IGeocodeRequest request, CancellationToken token = default)
    {
        var googleRequest = new PlacesV1GeocodeRequest(result.Descriptor)
        {
            BoundingBox = request.BoundingBox,
            Language = request.Language,
            Region = request.Region,
            TypeFilters = request.TypeFilters
        };

        return await Geocode(googleRequest, token);
    }

    public async Task<IEnumerable<IGeocodeRecord>> Geocode(IGeocodeRequest request, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(GeocodeExtensions.HttpClientTag);
        httpClient.BaseAddress = new Uri(options.Value.GeocodeApiUrl);

        var googleRequest = new PlacesV1GeocodeRequest(request);
        //todo: validation of google specific parameter values

        var response = await httpClient.GetAsync(
            $"{googleRequest.ToRequestPath()}&key={options.Value.ApiKey}",
            cancellationToken: token);
        var responseBody = await response.Content.ReadAsStreamAsync(token);
        var rawRecord = await JsonSerializer.DeserializeAsync<PlacesGeocodeRecord>(
            responseBody,
            cancellationToken: token);

        if (rawRecord is null)
        {
            logger.LogWarning("Could not deserialize google places v1 geocode json response or response was null or empty");
            return [];
        }

        return rawRecord.Results
            .Select(recordFactory.Create)
            .ToList();
    }
}
