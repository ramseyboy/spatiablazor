using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Web;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Geocode.Google.V1.Autocomplete;

public sealed record PlacesV1AutocompleteRequest(GoogleGeocodeConfigurationOptions options): IAutocompleteRequest, IRequest
{
    private const string GeocodeTypeFilter = "geocode";

    [SetsRequiredMembers]
    public PlacesV1AutocompleteRequest(IAutocompleteRequest request, GoogleGeocodeConfigurationOptions options) : this(options)
    {
        Query = request.Query;
        Limit = request.Limit;
        BiasLocation = request.BiasLocation;
        Radius = request.Radius;
        Scale = request.Scale;
        BoundingBox = request.BoundingBox;
        Language = request.Language;
        TypeFilters = request.TypeFilters;
        Region = request.Region;
        IgnoreErrors = request.IgnoreErrors;
    }

    [Required]
    public required string Query { get; set; }
    public Point? BiasLocation { get; set; }
    public Envelope? BoundingBox { get; set; }
    public string? Language { get; set; }
    public ISet<string> TypeFilters { get; set; } = new HashSet<string>();
    public double? Radius { get; set; }
    // todo: implement this as a switch of either bias location vs restrict location
    public double? Scale { get; set; }
    public int? Limit { get; set; }
    public bool IgnoreErrors { get; set; }
    public string? Region { get; set; }

    public string ToRequestPath()
    {
        var builder = new StringBuilder();
        builder.Append('?');

        var encodedQuery = HttpUtility.UrlEncode(Query);
        builder.Append(CultureInfo.InvariantCulture, $"input={encodedQuery}");

        builder.Append('&');
        builder.Append("strictbounds=false");

        if (Limit is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"limit={Limit}");
        }

        if (BiasLocation is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"location={BiasLocation.Y},{BiasLocation.X}");

            if (Radius is not null)
            {
                builder.Append('&');
                builder.Append(CultureInfo.InvariantCulture, $"radius={Radius}");
            }
        }

        if (BoundingBox is not null)
        {
            builder.Append('&');
            builder.Append(CultureInfo.InvariantCulture, $"locationrestriction=rectangle:{BoundingBox.MinY},{BoundingBox.MinX}|{BoundingBox.MaxY},{BoundingBox.MaxX}");
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

        if (!options.UsePlaceDetailApi)
        {
            if (TypeFilters.Count == 0)
            {
                TypeFilters = new HashSet<string>();
                TypeFilters.Add(GeocodeTypeFilter);
            }
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
