namespace SpatiaBlazor.Geocode.Abstractions;

public interface IGeocodeClient
{
    /// <summary>
    /// Search for autocomplete addresses given a request string and additional options
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <exception cref="GeocodeException"></exception>
    /// <returns>A Task of auto complete records</returns>
    public Task<IEnumerable<IAutocompleteRecord>> Autocomplete(
        IAutocompleteRequest request,
        CancellationToken token = default);

    /// <summary>
    /// Geocode an autocomplete record to get additional geometry and metadata given a descriptor and additional options
    /// </summary>
    /// <param name="result"></param>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <exception cref="GeocodeException"></exception>
    /// <returns>A Task of geocode records</returns>
    public Task<IEnumerable<IGeocodeRecord>> Geocode(
        IAutocompleteRecord result,
        IGeocodeRequest request,
        CancellationToken token = default);
}
