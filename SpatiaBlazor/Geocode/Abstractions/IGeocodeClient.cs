namespace SpatiaBlazor.Geocode.Abstractions;

public interface IGeocodeClient
{
    public Task<IEnumerable<IGeocodeRecord>> FromAddress(
        IAutocompleteRequest request,
        CancellationToken token = default);

    public Task<IEnumerable<IGeocodeRecord>> FromPoint(
        IReverseGeocodeRequest request,
        CancellationToken token = default);
}
