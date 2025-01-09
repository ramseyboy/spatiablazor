using SpatiaBlazor.Geocode.Abstractions;

namespace SpatiaBlazor.Components.Address.Suggestions.Label;

public interface IAddressLabelFactory
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    /// <returns>The structured label or empty if no valid properties or fallbacks</returns>
    string Create(IAddressRecord record);
}
