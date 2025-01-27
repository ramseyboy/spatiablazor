namespace SpatiaBlazor.Geocode.Abstractions.Descriptor;

internal class DescriptorOrderAttribute: Attribute
{
    public required int Order { get; set; }
    public string? Delimiter { get; set; }
    public string? FallbackLabel { get; set; }
}
