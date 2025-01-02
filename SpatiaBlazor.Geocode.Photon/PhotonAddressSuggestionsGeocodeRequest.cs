using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using BlazorGeospatial.Geocode.Client;
using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Geocode.Photon;

public record PhotonAddressSuggestionsGeocodeRequest : IAddressSuggestionsRequest, IPhotonRequestMixin
{
    public PhotonAddressSuggestionsGeocodeRequest()
    {
    }

    public PhotonAddressSuggestionsGeocodeRequest(IAddressSuggestionsRequest request)
    {
        Query = request.Query;
    }

    /// <inheritdoc />>
    [Required(AllowEmptyStrings = false)]
    public string Query { get; set; } = string.Empty;

    /// <inheritdoc />>
    [Range(1, 30)]
    public int Limit { get; set; } = 15;

    /// <summary>
    ///
    /// </summary>
    public double Zoom { get; set; } = 0.2;

    /// <summary>
    ///
    /// </summary>
    public int Scale { get; set; } = 14;

    /// <inheritdoc />>
    public Point? BiasLocation { get; set; }

    /// <inheritdoc />>
    public Envelope? BBox { get; set; }

    /// <inheritdoc />>
    public string? Language { get; set; }

    /// <inheritdoc />>
    public ISet<string> LayerFilters { get; set; } = ImmutableHashSet<string>.Empty;

    /// <inheritdoc />>
    public IDictionary<string, string> OsmTagFilters { get; set; } = ImmutableDictionary<string, string>.Empty;

    /// <inheritdoc />>
    public bool IgnoreErrors { get; set; } = true;

    public string ToRequestPath()
    {
        return $"api?q={Query}";
    }

    //todo implement structured request
    // public string? CountryCode { get; set; }
    //
    // public string? State { get; set; }
    //
    // public string? County { get; set; }
    //
    // public string? City { get; set; }
    //
    // public string? PostCode { get; set; }
    //
    // public string? District { get; set; }
    //
    // public string? Street { get; set; }
    //
    // public string? HouseNumber { get; set; }
}
