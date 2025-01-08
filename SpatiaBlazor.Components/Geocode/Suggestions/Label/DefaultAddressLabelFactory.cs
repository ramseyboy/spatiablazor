using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using BlazorGeospatial.Geocode.Client;

namespace SpatiaBlazor.Components.Geocode.Suggestions.Label;

internal sealed class DefaultAddressLabelFactory: IAddressLabelFactory
{
    public string Create(IAddressRecord record)
    {
        var orderedProperties = record.GetType().GetProperties()
            .Where(pi => pi.GetCustomAttribute<AddressLabelOrderAttribute>() is not null)
            .Where(pi => pi.GetValue(record) is not null || pi.GetCustomAttribute<AddressLabelOrderAttribute>()!.FallbackLabel is not null)
            .OrderBy(pi => pi.GetCustomAttribute<AddressLabelOrderAttribute>()!.Order)
            .ToList();

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < orderedProperties.Count; i++)
        {
            var orderedProperty = orderedProperties[i];

            var attr = orderedProperty.GetCustomAttribute<AddressLabelOrderAttribute>();

            var delimiter = attr!.Delimiter;
            var val = orderedProperty.GetValue(record)?.ToString();
            var fallbackLabel = attr.FallbackLabel;

            if (!string.IsNullOrWhiteSpace(val) || fallbackLabel is not null)
            {
                stringBuilder.Append(val ?? fallbackLabel);
                if (i != orderedProperties.Count - 1 && delimiter is not null)
                {
                    stringBuilder.Append(delimiter);
                    if (delimiter is not " ")
                    {
                        stringBuilder.Append(' ');
                    }
                }
            }
        }

        return stringBuilder.ToString();
    }
}
