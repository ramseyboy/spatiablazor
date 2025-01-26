using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using SpatiaBlazor.Geocode.Abstractions;
using SpatiaBlazor.Geocode.Google.V1;

namespace SpatiaBlazor.Geocode.Google;

public sealed record GoogleAutocompleteRecord: IAutocompleteRecord
{
    public required string Id { get; set; }
    public required string Descriptor { get; set; }
    public ISet<string> Types { get; init; } = new HashSet<string>();

    [SetsRequiredMembers]
    public GoogleAutocompleteRecord(PlacesV1AutocompletePrediction record)
    {
        Id = record.PlaceId;
        Descriptor = record.Description;
        Types = record.Types.ToHashSet();
    }
}
