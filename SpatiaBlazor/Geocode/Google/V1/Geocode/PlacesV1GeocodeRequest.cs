using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Web;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google.V1.Geocode;

[method: SetsRequiredMembers]
public sealed record PlacesV1GeocodeRequest() : IGeocodeRequest, IRequest
{
    public required string Query { get; set; }
    public Envelope? BoundingBox { get; set; }
    public string? Language { get; set; }
    public string? Region { get; set; }
    public ISet<string> TypeFilters { get; set; } = new HashSet<string>();

    public string ToRequestPath()
    {
        var builder = new StringBuilder();
        builder.Append('?');

        var encodedQuery = HttpUtility.UrlEncode(Query);
        builder.Append(CultureInfo.InvariantCulture, $"address={encodedQuery}");

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
