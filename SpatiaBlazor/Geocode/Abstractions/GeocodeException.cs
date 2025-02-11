namespace SpatiaBlazor.Geocode.Abstractions;

public class GeocodeException : ApplicationException
{
    public GeocodeException(string? message) : base(message)
    {
    }

    public GeocodeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
