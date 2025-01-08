namespace SpatiaBlazor.Components.Geocode.Suggestions.Label;

internal class AddressLabelOrderAttribute: Attribute
{
    public required int Order { get; set; }
    public string? Delimiter { get; set; }
    public string? FallbackLabel { get; set; }
}
