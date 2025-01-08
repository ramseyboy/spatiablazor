using BlazorGeospatial.Geocode.Client;
using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Geocode.Suggestions.Label;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Components.Geocode.Suggestions.Photon;

public sealed record PhotonGeocodeResultsViewModel : IGeocodeResultsViewModel, IAddressRecord
{
    public PhotonGeocodeResultsViewModel(PhotonGeocodeRecord record, IAddressLabelFactory? addressLabelFactory = null)
    {
        addressLabelFactory ??= new DefaultAddressLabelFactory();

        Id = record.Id ?? throw new ApplicationException(); //todo
        Name = record.Name;
        Street = record.Street;
        HouseNumber = record.HouseNumber;
        City = record.City;
        Locality = record.Locality;
        CountyOrRegion = record.CountyOrRegion;
        StateOrProvince = record.StateOrProvince;
        Country = record.Country;
        CountryCode = record.CountryCode;
        ZipOrPostCode = record.ZipOrPostCode;

        BoundingBox = record.BoundingBox;
        Location = record.Geom as Point ?? throw new ApplicationException(); //todo handle non point

        Label = addressLabelFactory.Create(this);
    }

    public object Id { get; }
    public Point Location { get;}
    public Envelope BoundingBox { get; set; }
    public string Label { get; set; }

    [AddressLabelOrder(Order = 1, Delimiter = ",")]
    public string? Name { get; set; }
    [AddressLabelOrder(Order = 2, Delimiter = " ")]
    public string? HouseNumber { get; set; }
    [AddressLabelOrder(Order = 3, Delimiter = ",")]
    public string? Street { get; set; }
    [AddressLabelOrder(Order = 4, Delimiter = ",")]
    public string? City { get; set; }
    [AddressLabelOrder(Order = 5, Delimiter = ",")]
    public string? StateOrProvince { get; set; }
    [AddressLabelOrder(Order = 6, Delimiter = ",")]
    public string? ZipOrPostCode { get; set; }
    [AddressLabelOrder(Order = 7)]
    public string? CountryCode { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? Country { get; set; }
}
