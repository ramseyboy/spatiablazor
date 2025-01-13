namespace SpatiaBlazor.Geocode.Abstractions;

internal class GeocodeQueryParameterAttribute(string name)
{
    public string Name { get; private set; } = name;
}
