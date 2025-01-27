using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Google.V1;
using SpatiaBlazor.Geocode.Google.V1.Autocomplete;
using SpatiaBlazor.Geocode.Google.V1.Geocode;
using SpatiaBlazor.Geocode.Google.V1.Places;

namespace SpatiaBlazor.Geocode.Google;

public sealed class GoogleGeocodeClient(
    IHttpClientFactory httpClientFactory,
    ILogger<GoogleGeocodeClient> logger,
    IOptions<GoogleGeocodeConfigurationOptions> options,
    PlacesV1GeocodeRecordFactory geocodeRecordFactory,
    PlacesV1PlaceDetailRecordFactory placeDetailRecordFactory)
    : IGeocodeClient
{
    public async Task<IEnumerable<IAutocompleteRecord>> Autocomplete(IAutocompleteRequest request, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(GeocodeExtensions.HttpClientTag);
        httpClient.BaseAddress = new Uri(options.Value.AutocompleteApiUrl);

        var googleRequest = new PlacesV1AutocompleteRequest(request, options.Value);
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

    public async Task<IEnumerable<IGeocodeRecord>> Geocode(IAutocompleteRecord result, IGeocodeRequest geocodeRequest, CancellationToken token = default)
    {
        var httpClient = httpClientFactory.CreateClient(GeocodeExtensions.HttpClientTag);

        //todo extract this method to delegate
        IRequest request;
        if (options.Value.UsePlaceDetailApi)
        {
            httpClient.BaseAddress = new Uri(options.Value.PlaceDetailApiUrl);
            request = new PlacesV1DetailRequest
            {
                PlaceId = result.Id,
                Language = geocodeRequest.Language,
                Region = geocodeRequest.Region,
                TypeFilters = geocodeRequest.TypeFilters
            };
        }
        else
        {
            httpClient.BaseAddress = new Uri(options.Value.GeocodeApiUrl);
            request = new PlacesV1GeocodeRequest
            {
                Query = result.Descriptor,
                BoundingBox = geocodeRequest.BoundingBox,
                Language = geocodeRequest.Language,
                Region = geocodeRequest.Region,
                TypeFilters = geocodeRequest.TypeFilters
            };
        }

        //todo: validation of google specific parameter values

        var response = await httpClient.GetAsync(
            $"{request.ToRequestPath()}&key={options.Value.ApiKey}",
            cancellationToken: token);
        var responseBody = await response.Content.ReadAsStreamAsync(token);

        if (options.Value.UsePlaceDetailApi)
        {
            var rawRecord = await JsonSerializer.DeserializeAsync<PlacesV1DetailRecord>(
                responseBody,
                cancellationToken: token);

            if (rawRecord?.Result is null)
            {
                logger.LogWarning("Could not deserialize google places v1 geocode json response or response was null or empty");
                return [];
            }

            var placeRequest = request as PlacesV1DetailRequest;
            return [placeDetailRecordFactory.Create(rawRecord.Result, placeRequest!.PlaceId)];
        }
        else
        {
            var rawRecord = await JsonSerializer.DeserializeAsync<PlacesV1GeocodeRecord>(
                responseBody,
                cancellationToken: token);

            if (rawRecord is null)
            {
                logger.LogWarning("Could not deserialize google places v1 geocode json response or response was null or empty");
                return [];
            }

            return rawRecord.Results
                .Select(geocodeRecordFactory.Create)
                .ToList();
        }
    }
}
