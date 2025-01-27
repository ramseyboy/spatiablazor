namespace SpatiaBlazor.Geocode.Abstractions;

public interface IAutocompleteRecord
{
    public string Id { get; set; }
    public string Descriptor { get; set; }
    public ISet<string> Types { get; init; }
}
