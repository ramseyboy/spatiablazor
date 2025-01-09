using SpatiaBlazor.Components.Address.Suggestions.Label;
using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Tests.Components.Address.Label;

public class AddressLabelFactoryTest
{
    [Fact(DisplayName = "Given an address with 0 non-empty properties and 0 fallbacks, When label is created, Then it should throw")]
    public void TestZeroProps()
    {
        var factory = new DefaultAddressLabelFactory();

        var record = new TestAddressRecord();

        var label = factory.Create(record);

        Assert.Equal(string.Empty, label);
    }

    [Fact(DisplayName = "Given an address with only 1 non-fallback, non-empty property, When label is created, Then label should have only the property")]
    public void TestOneProp()
    {
        var factory = new DefaultAddressLabelFactory();

        var record = new TestAddressRecord
        {
            Name = "Q2 Stadium"
        };

        var label = factory.Create(record);

        Assert.Equal($"{record.Name}", label);
    }

    [Fact(DisplayName = "Given an address with 2 non-fallback, non-empty properties, When label is created, Then label should have the properties with specified delimiter")]
    public void TestTwoProp()
    {
        var factory = new DefaultAddressLabelFactory();

        var record = new TestAddressRecord
        {
            Name = "Q2 Stadium",
            City = "Austin"
        };

        var label = factory.Create(record);

        Assert.Equal($"{record.Name}, {record.City}", label);
    }

    [Fact(DisplayName = "Given an address with 1 non-fallback, non-empty properties and 1 fallback label, When label is created, Then label should have the property and the fallback with specified delimiter")]
    public void TestOnePropOneFallback()
    {
        var factory = new DefaultAddressLabelFactory();

        var record = new TestAddressRecordWithFallback
        {
            Name = "Q2 Stadium"
        };

        var label = factory.Create(record);

        Assert.Equal($"{record.Name}, N/A", label);
    }

    [Fact(DisplayName = "Given an address with multiple non-empty properties, When label is created, Then label should have the properties without a delimiter at the end")]
    public void TestDelimiterAtEnd()
    {
        var factory = new DefaultAddressLabelFactory();

        var record = new TestAddressRecord
        {
            Name = "Q2 Stadium",
            HouseNumber = "123",
            Street = "Verde Way",
            StateOrProvince = "Texas"
        };

        var label = factory.Create(record);

        Assert.Equal($"{record.Name}, {record.HouseNumber} {record.Street}, {record.StateOrProvince}", label);
    }
}

public class TestAddressRecord: IAddressRecord
{
    [AddressLabelOrder(Order = 1, Delimiter = ",")]
    public string? Name { get; set; }
    [AddressLabelOrder(Order = 2, Delimiter = " ")]
    public string? HouseNumber { get; set; }
    [AddressLabelOrder(Order = 3, Delimiter = ",")]
    public string? Street { get; set; }
    [AddressLabelOrder(Order = 4, Delimiter = ",")]
    public string? City { get; set; }
    [AddressLabelOrder(Order = 5, Delimiter = ",")]
    public string? ZipOrPostCode { get; set; }
    [AddressLabelOrder(Order = 6, Delimiter = ",")]
    public string? StateOrProvince { get; set; }
    [AddressLabelOrder(Order = 7)]
    public string? CountryCode { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? Country { get; set; }
}

public class TestAddressRecordWithFallback: IAddressRecord
{
    [AddressLabelOrder(Order = 1, Delimiter = ",")]
    public string? Name { get; set; }
    [AddressLabelOrder(Order = 2)]
    public string? HouseNumber { get; set; }
    [AddressLabelOrder(Order = 3, Delimiter = ",")]
    public string? Street { get; set; }
    [AddressLabelOrder(Order = 4, Delimiter = ",")]
    public string? City { get; set; }
    [AddressLabelOrder(Order = 5, Delimiter = ",")]
    public string? ZipOrPostCode { get; set; }
    [AddressLabelOrder(Order = 6, Delimiter = ",", FallbackLabel = "N/A")]
    public string? StateOrProvince { get; set; }
    [AddressLabelOrder(Order = 7)]
    public string? CountryCode { get; set; }
    public string? Locality { get; set; }
    public string? CountyOrRegion { get; set; }
    public string? Country { get; set; }
}
