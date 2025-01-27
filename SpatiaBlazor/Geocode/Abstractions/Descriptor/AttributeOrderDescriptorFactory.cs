using System.Reflection;
using System.Text;

namespace SpatiaBlazor.Geocode.Abstractions.Descriptor;

internal sealed class AttributeOrderDescriptorFactory: IDescriptorFactory
{
    public string Create<TRecord>(TRecord record)
    {
        var orderedProperties = typeof(TRecord).GetProperties()
            .Where(pi => pi.GetCustomAttribute<DescriptorOrderAttribute>() is not null)
            .Where(pi => pi.GetValue(record) is not null || pi.GetCustomAttribute<DescriptorOrderAttribute>()!.FallbackLabel is not null)
            .OrderBy(pi => pi.GetCustomAttribute<DescriptorOrderAttribute>()!.Order)
            .ToList();

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < orderedProperties.Count; i++)
        {
            var orderedProperty = orderedProperties[i];

            var attr = orderedProperty.GetCustomAttribute<DescriptorOrderAttribute>();

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
