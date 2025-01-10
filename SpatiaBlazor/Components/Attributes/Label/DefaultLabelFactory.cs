using System.Reflection;
using System.Text;

namespace SpatiaBlazor.Components.Attributes.Label;

internal sealed class DefaultLabelFactory: ILabelFactory
{
    public string Create(object record)
    {
        var orderedProperties = record.GetType().GetProperties()
            .Where(pi => pi.GetCustomAttribute<LabelOrderAttribute>() is not null)
            .Where(pi => pi.GetValue(record) is not null || pi.GetCustomAttribute<LabelOrderAttribute>()!.FallbackLabel is not null)
            .OrderBy(pi => pi.GetCustomAttribute<LabelOrderAttribute>()!.Order)
            .ToList();

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < orderedProperties.Count; i++)
        {
            var orderedProperty = orderedProperties[i];

            var attr = orderedProperty.GetCustomAttribute<LabelOrderAttribute>();

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
