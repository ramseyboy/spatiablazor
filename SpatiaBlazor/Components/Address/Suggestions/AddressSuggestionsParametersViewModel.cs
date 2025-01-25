using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

/// <summary>
/// Used to set default values for the address suggestions search parameters, i.e. Bounding box, bias location, filters, language.
/// </summary>
public sealed record AddressSuggestionsParametersViewModel : IAutocompleteRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Query is empty, please input a query in order to find address suggestions")]
    public string Query { get; set; } = string.Empty;

    public Point? BiasLocation { get; set; }
    public Envelope? BoundingBox { get; set; }
    public string? Language { get; set; }
    public ISet<string> TypeFilters { get; set; } = ImmutableHashSet<string>.Empty;
    public int? Limit { get; set; }
    public double? Radius { get; set; }
    public double? Scale { get; set; }
    public string? Region { get; set; }
    public bool IgnoreErrors { get; set; }

    public int DebounceInterval { get; set; } = 300;

    public int MinQueryLength { get; set; } = 6;

    public string? HelpText { get; set; }
}
