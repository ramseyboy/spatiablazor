namespace SpatiaBlazor.Components.Address.Suggestions;

public class SuggestionsException : ApplicationException
{
    public SuggestionsException(string? message) : base(message)
    {
    }

    public SuggestionsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

public class InvalidSuggestionsParameterException : SuggestionsException
{
    public InvalidSuggestionsParameterException(string? message, IDictionary<string, string> invalidParameters) : base(message)
    {
    }

    public InvalidSuggestionsParameterException(string? message, Exception? innerException, IDictionary<string, string> invalidParameters) : base(message, innerException)
    {
    }
}
