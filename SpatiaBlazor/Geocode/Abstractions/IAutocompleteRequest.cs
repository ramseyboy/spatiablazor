using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Abstractions;

public interface IAutocompleteRequest
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
    public Envelope? BoundingBox { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// OSM layers types or google autocomplete types
    /// </summary>
    public ISet<string> TypeFilters { get; set; }

    /// <summary>
    /// limit results
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    ///
    /// </summary>
    public double? Zoom { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int? Scale { get; set; }

    /// <summary>
    /// Ignore features that cause errors during parsing of response, defaults to true.
    /// If false, request will throw ArgumentException on invalid features.
    /// If true, parsing will log error and continue.
    /// </summary>
    public bool IgnoreErrors { get; set; }
}
