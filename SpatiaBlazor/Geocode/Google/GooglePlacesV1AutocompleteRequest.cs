using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google;

public class GooglePlacesV1AutocompleteRequest: IAutocompleteRequest, IGeocodeRequest
{
    public string Query { get; set; }
    public Point? BiasLocation { get; set; }
    public Envelope? BoundingBox { get; set; }
    public string? Language { get; set; }
    public ISet<string> TypeFilters { get; set; }
    public double? Zoom { get; set; }
    public int? Scale { get; set; }
    public int? Limit { get; set; }
    public bool IgnoreErrors { get; set; }

    public string ToRequestPath()
    {
        throw new NotImplementedException();
    }
}
