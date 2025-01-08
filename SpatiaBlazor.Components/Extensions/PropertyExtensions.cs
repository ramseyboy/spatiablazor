using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Humanizer;
using SpatiaBlazor.Components.Mixins;
using SpatiaBlazor.Components.Validation;

namespace SpatiaBlazor.Components.Extensions;

public static class PropertyExtensions
{
    public static string GetPropertyDisplay<TViewModel>(this TViewModel _, string propertyName) where TViewModel: IViewModelMixin
    {
        return typeof(TViewModel).GetProperty(propertyName)?.GetPropertyDisplayName()?.Humanize() ?? string.Empty;
    }

    public static ValidationAttribute GetPropertyValidation<TViewModel>(this TViewModel _, string propertyName) where TViewModel: IViewModelMixin
    {
        return new PropertyValidationAttribute<TViewModel>(propertyName);
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
