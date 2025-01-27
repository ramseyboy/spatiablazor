using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Abstractions;

public interface IAutocompleteRequest: IGeocodeRequest
{
    /// <summary>
    /// The longitude and latitude of a point to bias/filter results
    /// </summary>
    public Point? BiasLocation { get; set; }

    /// <summary>
    /// limit results
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The desired radius around the bias location point
    /// </summary>
    public double? Radius { get; set; }

    /// <summary>
    /// Only used for photon
    /// </summary>
    public double? Scale { get; set; }

    /// <summary>
    /// Ignore features that cause errors during parsing of response, defaults to true.
    /// If false, request will throw ArgumentException on invalid features.
    /// If true, parsing will log error and continue.
    /// </summary>
    public bool IgnoreErrors { get; set; }
}
