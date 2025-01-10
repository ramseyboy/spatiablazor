using NetTopologySuite.Geometries;
using SpatiaBlazor.Components.Attributes.Label;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions;

internal sealed record DefaultGeocodeResultsViewModel : IGeocodeResultsViewModel
{
    public DefaultGeocodeResultsViewModel(IGeocodeRecord record, ILabelFactory? labelFactory = null)
    {
        labelFactory ??= new DefaultLabelFactory();

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
        Geom = record.Geom as Point ?? throw new ApplicationException(); //todo handle non point

        Label = labelFactory.Create(this);
    }

    public object Id { get; }
    public Point Geom { get;}
    public Envelope BoundingBox { get; set; }
    public string Label { get; set; }

    [LabelOrder(Order = 1, Delimiter = ",")]
    public string? Name { get; set; }
    [LabelOrder(Order = 2, Delimiter = " ")]
    public string? HouseNumber { get; set; }
    [LabelOrder(Order = 3, Delimiter = ",")]
    public string? Street { get; set; }
    [LabelOrder(Order = 4, Delimiter = ",")]
    public string? City { get; set; }
    [LabelOrder(Order = 5, Delimiter = ",")]
    public string? StateOrProvince { get; set; }
    [LabelOrder(Order = 6, Delimiter = ",")]
    public string? ZipOrPostCode { get; set; }
    [LabelOrder(Order = 7)]
    public string? CountryCode { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? Country { get; set; }
}
