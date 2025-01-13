using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Photon;

public record PhotonAutocompleteRequest : IAutocompleteRequest, IGeocodeRequest
{
    public PhotonAutocompleteRequest(IAutocompleteRequest request)
    {
        Query = request.Query;
        Limit = request.Limit;
        BiasLocation = request.BiasLocation;
        Zoom = request.Zoom;
        Scale = request.Scale;
        BoundingBox = request.BoundingBox;
        Language = request.Language;
        TypeFilters = request.TypeFilters;
        IgnoreErrors = request.IgnoreErrors;
    }

    /// <inheritdoc />>
    [Required(AllowEmptyStrings = false)]
    public string Query { get; set; } = string.Empty;

    /// <inheritdoc />>
    [Range(1, 30)]
    public int? Limit { get; set; } = 15;

    /// <inheritdoc />>
    public double? Zoom { get; set; } = 0.2;

    /// <inheritdoc />>
    public int? Scale { get; set; } = 14;

    /// <inheritdoc />>
    public Point? BiasLocation { get; set; }

    /// <inheritdoc />>
    public Envelope? BoundingBox { get; set; }

    /// <inheritdoc />>
    public string? Language { get; set; }

    /// <inheritdoc />>
    public ISet<string> TypeFilters { get; set; } = ImmutableHashSet<string>.Empty;

    /// <summary>
    ///
    /// </summary>
    public IDictionary<string, string> OsmTagFilters { get; set; } = ImmutableDictionary<string, string>.Empty;

    /// <inheritdoc />>
    public bool IgnoreErrors { get; set; } = true;

    public string ToRequestPath()
    {
        var builder = new StringBuilder();
        builder.Append("api?");
        builder.Append(CultureInfo.InvariantCulture, $"q={Query}");

        if (Limit is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"limit={Limit}");
        }

        if (BiasLocation is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"lon={BiasLocation.X}");
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"lat={BiasLocation.Y}");

            if (Zoom is not null)
            {
                builder.Append('&');
                builder.Append(CultureInfo.InvariantCulture, $"zoom={Zoom}");
            }

            if (Scale is not null)
            {
                builder.Append('&');
                builder.Append(CultureInfo.InvariantCulture, $"location_bias_scale={Scale}");
            }
        }

        if (BoundingBox is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"bbox={BoundingBox.MinX},{BoundingBox.MinY},{BoundingBox.MaxX},{BoundingBox.MaxY}");
        }

        if (Language is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"lang={Language}");
        }

        foreach (var typeFilter in TypeFilters)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"layer={typeFilter}");
        }

        Console.WriteLine(builder);

        return builder.ToString();
    }
}
