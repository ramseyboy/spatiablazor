using System.ComponentModel.DataAnnotations;

namespace SpatiaBlazor.Validation;

internal static class ValidationExtensions
{
    internal static IDictionary<string, string> ToDictionary(this ICollection<ValidationResult> results)
    {
        return results
            .ToDictionary(x => x.ErrorMessage ?? "Unknown error message", x => string.Join(',', x.MemberNames));
    }

    internal static string ToParameterList(this ICollection<ValidationResult> results)
    {
        var names = results
            .SelectMany(x => x.MemberNames)
            .Distinct();
        return string.Join(",", names);
    }
}
