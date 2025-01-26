using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public sealed record PhotonReverseGeocodeRequest: IReverseGeocodeRequest, IRequest
{
    public PhotonReverseGeocodeRequest()
    {
    }

    public PhotonReverseGeocodeRequest(IReverseGeocodeRequest request)
    {
        Location = request.Location;
    }

    /// <summary>
    ///
    /// </summary>
    [Required]
    public Point Location { get; set; } = new(0,0);

    /// <summary>
    ///
    /// </summary>
    public double? Radius { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int Limit { get; set; }

    /// <inheritdoc />>
    public string? Language { get; set; }

    /// <summary>
    ///
    /// </summary>
    public ISet<string> TypeFilters { get; set; } = new HashSet<string>();

    /// <summary>
    ///
    /// </summary>
    public IDictionary<string, string> OsmTagFilters { get; set; } = ImmutableDictionary<string, string>.Empty;

    public string ToRequestPath()
    {
        return $"reverse?lon={Location.X}&lat={Location.Y}";
    }
}
