namespace BlazorGeospatial.Geocode.Client;

public interface IGeocodeClient<TGeocodeResponse> where TGeocodeResponse: IGeocodeRecord
{
    public Task<IEnumerable<TGeocodeResponse>> FromAddress(
        IAddressSuggestionsRequest request,
        CancellationToken token = default);

    public Task<IEnumerable<TGeocodeResponse>> FromPoint(
        IReverseGeocodeRequest request,
        CancellationToken token = default);
}
