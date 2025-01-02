using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SpatiaBlazor.Components.Validation;

public class RequiredEnumerableAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return null;
        }

        if (value is ICollection collection)
        {
            return collection.Count <= 0 ? new ValidationResult(ErrorMessage) : null;
        }

        if (value is IEnumerable enumerable)
        {
            return !enumerable.GetEnumerator().MoveNext() ? new ValidationResult(ErrorMessage) : null;
        }

        return null;
    }
}
