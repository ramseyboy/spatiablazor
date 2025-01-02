using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Humanizer;
using SpatiaBlazor.Components.Validation;

namespace SpatiaBlazor.Components.Extensions;

public static class PropertyExtensions
{
    public static string GetPropertyDisplay<T>(this T _, string propertyName)
    {
        return typeof(T).GetProperty(propertyName).GetPropertyDisplayName().Humanize();
    }

    public static ValidationAttribute GetPropertyValidation<T>(this T type, string propertyName)
    {
        return new PropertyValidationAttribute<T>(propertyName);
    }
    
    public static string GetPropertyDisplayName(this PropertyInfo propertyInfo)
    {
        if (!Attribute.IsDefined(propertyInfo, typeof(DisplayAttribute)))
        {
            return propertyInfo.Name;
        }

        var display = propertyInfo.GetCustomAttributes(true)
            .ToDictionary(a => a.GetType().Name, a => a)
            [nameof(DisplayAttribute)] as DisplayAttribute;
        return display?.Name ?? propertyInfo.Name;
    }
}
