using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public record PhotonReverseGeocodeRequest: IReverseGeocodeRequest, IPhotonRequestMixin
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

    /// <inheritdoc />>
    public int Limit { get; set; }

    /// <inheritdoc />>
    public string? Language { get; set; }

    /// <inheritdoc />>
    public ISet<string> LayerFilters { get; set; } = ImmutableHashSet<string>.Empty;

    /// <inheritdoc />>
    public IDictionary<string, string> OsmTagFilters { get; set; } = ImmutableDictionary<string, string>.Empty;

    public string ToRequestPath()
    {
        return $"reverse?lon={Location.X}&lat={Location.Y}";
    }
}
