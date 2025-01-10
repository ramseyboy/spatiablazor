namespace SpatiaBlazor.Components.Attributes.Label;

internal class LabelOrderAttribute: Attribute
{
    public required int Order { get; set; }
    public string? Delimiter { get; set; }
    public string? FallbackLabel { get; set; }
}
