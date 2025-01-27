using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Abstractions;

public interface IGeocodeRequest
{
    /// <summary>
    ///
    /// </summary>
    public string Query { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Envelope? BoundingBox { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Only used by google, a top level ccTLD region code ie. uk, us
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// OSM layer types or google autocomplete fields
    /// </summary>
    public ISet<string> TypeFilters { get; set; }
}
