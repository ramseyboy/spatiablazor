namespace SpatiaBlazor.Geocode.Abstractions;

public interface IGeocodeRecordFactory<in TInput, out TOutput> where TOutput: IGeocodeRecord
{
    public TOutput Empty { get; }

    /// <summary>
    /// Creates a geocode record instance given the input value
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="ArgumentException">throws if the input value is invalid or missing required data</exception>
    /// <returns>A valid geocode record</returns>
    public TOutput Create(TInput input);
}
