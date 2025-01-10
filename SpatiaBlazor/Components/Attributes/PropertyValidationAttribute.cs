using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SpatiaBlazor.Components.Attributes;

internal class PropertyValidationAttribute<TModelType> : ValidationAttribute
{
    private readonly List<ValidationAttribute> _validationAttributes;

    public PropertyValidationAttribute(PropertyInfo pi)
    {
        _validationAttributes = pi
            .GetCustomAttributes(true)
            .Where(x => x.GetType().IsAssignableTo(typeof(ValidationAttribute)))
            .Select(x => x as ValidationAttribute ?? throw new InvalidOperationException())
            .ToList();
    }


    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var invalid = _validationAttributes
            .Where(x => !x.IsValid(value))
            .ToList();
        if (invalid.Any())
        {
            var msgs = invalid
                .Select(x => x.ErrorMessage)
                .ToList();
            var result = new ValidationResult(string.Join(",", msgs));
            return result;
        }

        return null;
    }
}
