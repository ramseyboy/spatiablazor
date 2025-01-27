namespace SpatiaBlazor.Geocode.Abstractions.Descriptor;

public interface IDescriptorFactory
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    /// <returns>The structured label or empty if no valid properties or fallbacks</returns>
    string Create<TRecord>(TRecord record);
}
