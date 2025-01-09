using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Abstractions;

public interface IAddressSuggestionsRequest : IGeocodeRequest
{
    /// <summary>
    ///
    /// </summary>
    public string Query { get; set; }

    /// <summary>
    /// The longitude and latitude of a point to bias/filter results
    /// </summary>
    public Point? BiasLocation { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Envelope? BBox { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Ignore features that cause errors during parsing of response, defaults to true.
    /// If false, request will throw ArgumentException on invalid features.
    /// If true, parsing will log error and continue.
    /// </summary>
    public bool IgnoreErrors { get; set; }
}
