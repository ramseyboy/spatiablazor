using System.ComponentModel.DataAnnotations;

namespace SpatiaBlazor.Validation;

internal class ComponentModelValidator
{
    internal ICollection<ValidationResult> Validate<T>(T input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input), "Cannot validate a null input");
        }
        var (isValid, results) = ValidateComponentModel(input);
        if (isValid)
        {
            return new List<ValidationResult>();
        }

        return results;
    }

    private (bool isValid, ValidationResult[] errors) ValidateComponentModel(object input, ICollection<ValidationResult>? results = null)
    {
        results ??= new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(input, new ValidationContext(input), results, true);

        var properties = input.GetType().GetProperties()
            .Where(prop => prop is {CanRead: true, PropertyType.IsClass: true});

        foreach (var prop in properties)
        {
            var value = prop.GetValue(input);
            if (value != null)
            {
                if (value is IEnumerable<object> list)
                {
                    isValid = list
                        .Select(item => Validator.TryValidateObject(item, new ValidationContext(item), results, true))
                        .Aggregate(isValid, (current, nestedIsValid) => current & nestedIsValid);
                }
                else
                {
                    var nestedIsValid = Validator.TryValidateObject(value, new ValidationContext(value), results, true);
                    isValid &= nestedIsValid;
                }
            }
        }

        return (isValid, results.ToArray());
    }
}
