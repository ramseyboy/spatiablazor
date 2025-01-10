namespace SpatiaBlazor.Geocode.Abstractions;

public interface IAddressRecord
{
    public string? Name { get; set; }
    public string? HouseNumber { get; set; }
    public string? Street { get; set; }
    public string? Locality { get; set; }
    public string? City { get; set; }
    public string? ZipOrPostCode { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? StateOrProvince { get; set; }
    public string? Country { get; set; }
    public string? CountryCode { get; set; }
}
