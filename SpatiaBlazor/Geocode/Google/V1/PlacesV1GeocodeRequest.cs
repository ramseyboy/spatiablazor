using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google.V1;

[method: SetsRequiredMembers]
public sealed record PlacesV1GeocodeRequest(string Address) : IGeocodeRequest, IRequest
{
    public required string Query { get; set; } = Address;
    public Envelope? BoundingBox { get; set; }
    public string? Language { get; set; }
    public string? Region { get; set; }
    public ISet<string> TypeFilters { get; set; } = new HashSet<string>();

    [SetsRequiredMembers]
    public PlacesV1GeocodeRequest(IGeocodeRequest request) : this(request.Query)
    {
        BoundingBox = request.BoundingBox;
        Language = request.Language;
        Region = request.Region;
        TypeFilters = request.TypeFilters;
    }

    public string ToRequestPath()
    {
        var builder = new StringBuilder();
        builder.Append('?');
        builder.Append(CultureInfo.InvariantCulture, $"address={Address}");

        if (BoundingBox is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"bounds={BoundingBox.MinY},{BoundingBox.MinX}|{BoundingBox.MaxY},{BoundingBox.MaxX}");
        }

        if (Language is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"language={Language}");
        }

        if (Region is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"region={Region}");
        }

        return builder.ToString();
    }
}
