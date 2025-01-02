using NetTopologySuite.Geometries;

namespace BlazorGeospatial.Geocode.Client;

public interface IReverseGeocodeRequest: IGeocodeRequest
{
    /// <summary>
    ///
    /// </summary>
    public Point Location { get; set; }

    /// <summary>
    ///
    /// </summary>
    public double? Radius { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Language { get; set; }
}
