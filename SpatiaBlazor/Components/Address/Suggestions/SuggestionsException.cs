namespace SpatiaBlazor.Components.Address.Suggestions;

public class SuggestionsException(string? message) : ApplicationException(message);

public class InvalidSuggestionsParameterException(string? message, IDictionary<string, string> invalidParameters) : SuggestionsException(message);
