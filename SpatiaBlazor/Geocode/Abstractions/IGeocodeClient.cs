namespace SpatiaBlazor.Geocode.Abstractions;

public interface IGeocodeClient
{
    public Task<IEnumerable<IAutocompleteRecord>> Autocomplete(
        IAutocompleteRequest request,
        CancellationToken token = default);

    public Task<IEnumerable<IGeocodeRecord>> Geocode(
        IAutocompleteRecord result,
        CancellationToken token = default);
}
