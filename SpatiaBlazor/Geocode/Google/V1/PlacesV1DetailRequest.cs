using System.Globalization;
using System.Text;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google.V1;

public sealed record PlacesV1DetailRequest: IRequest
{
    public required string PlaceId { get; set; }
    public string? Language { get; set; }
    public ISet<string> TypeFilters { get; set; }
    public string? Region { get; set; }

    public string ToRequestPath()
    {
        var builder = new StringBuilder();
        builder.Append('?');
        builder.Append(CultureInfo.InvariantCulture, $"place_id={PlaceId}");

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

        if (TypeFilters.Count > 0)
        {
            var csv = string.Join(',', TypeFilters);
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"fields={csv}");
        }

        return builder.ToString();
    }
}
