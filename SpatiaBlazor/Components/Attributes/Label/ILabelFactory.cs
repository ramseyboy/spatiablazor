namespace SpatiaBlazor.Components.Attributes.Label;

public interface ILabelFactory
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="record"></param>
    /// <returns>The structured label or empty if no valid properties or fallbacks</returns>
    string Create(object record);
}
