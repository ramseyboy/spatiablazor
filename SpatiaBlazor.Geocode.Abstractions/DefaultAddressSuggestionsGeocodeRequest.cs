using NetTopologySuite.Geometries;

namespace BlazorGeospatial.Geocode.Client;

public record DefaultAddressSuggestionsGeocodeRequest : IAddressSuggestionsRequest
{
    /// <inheritdoc />>
    public required string Query { get; set; }

    /// <inheritdoc />>
    public Point? BiasLocation { get; set; }

    /// <inheritdoc />>
    public Envelope? BBox { get; set; }

    /// <inheritdoc />>
    public string? Language { get; set; }

    /// <inheritdoc />>
    public bool IgnoreErrors { get; set; } = true;

    public string ToRequestPath()
    {
        throw new NotImplementedException("This method is overridden in the implementation of the request");
    }
}
