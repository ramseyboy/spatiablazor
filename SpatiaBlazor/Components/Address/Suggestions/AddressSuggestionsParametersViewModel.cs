using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

public sealed record AddressSuggestionsParametersViewModel : IAutocompleteRequest
{
    [Display(Name = "Search for address")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Query is empty, please input a query in order to find address suggestions")]
    public string Query { get; set; } = string.Empty;

    public Point? BiasLocation { get; set; }
    public Envelope? BoundingBox { get; set; }
    public string? Language { get; set; }
    public ISet<string> TypeFilters { get; set; } = ImmutableHashSet<string>.Empty;
    public int? Limit { get; set; }
    public double? Zoom { get; set; }
    public int? Scale { get; set; }
    public bool IgnoreErrors { get; set; }

    public int DebounceInterval { get; set; } = 300;

    public int MinQueryLength { get; set; } = 6;
}
