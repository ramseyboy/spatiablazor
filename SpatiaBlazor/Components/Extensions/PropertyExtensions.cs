using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using SpatiaBlazor.Components.Attributes;

namespace SpatiaBlazor.Components.Extensions;

internal static class PropertyExtensions
{
    internal static bool IsHidden<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        var pi = propertyExpression.PropertyInfo();
        if (pi is not MemberInfo member)
        {
            return false;
        }

        var attr =  member.GetCustomAttribute<DisplayAttribute>();
        return (attr?.GetAutoGenerateField() ?? true) == false;
    }

    internal static bool IsEditable<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        var pi = propertyExpression.PropertyInfo();
        if (pi is not MemberInfo member)
        {
            return false;
        }

        var attr =  member.GetCustomAttribute<EditableAttribute>();
        return attr?.AllowEdit ?? false;

    }

    internal static bool IsRequired<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        var pi = propertyExpression.PropertyInfo();
        if (pi is not MemberInfo member)
        {
            return false;
        }

        return !Attribute.IsDefined(member, typeof(RequiredAttribute));

    }

    internal static string PropertyDisplay<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        var pi = propertyExpression.PropertyInfo();

        if (!Attribute.IsDefined(pi, typeof(DisplayAttribute)))
        {
            return pi.Name;
        }

        var display = pi.GetCustomAttributes(true)
            .ToDictionary(a => a.GetType().Name, a => a)
            [nameof(DisplayAttribute)] as DisplayAttribute;
        return display?.Name ?? pi.Name;
    }

    internal static ValidationAttribute PropertyValidation<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        var pi = propertyExpression.PropertyInfo();
        return new PropertyValidationAttribute<TProperty>(pi);
    }

    internal static string PropertyName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        var pi = propertyExpression.PropertyInfo();
        return pi.Name;
    }

    internal static PropertyInfo PropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyExpression)
    {
        if (propertyExpression.Body is not MemberExpression member)
        {
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a method, not a property.");
        }

        if (member.Member is not PropertyInfo propInfo)
        {
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a field, not a property.");
        }

        var type = typeof(TSource);
        if (propInfo.ReflectedType != null && type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
        {
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a property that is not from type {type}.");
        }

        return propInfo;
    }
}
