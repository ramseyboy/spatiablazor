namespace SpatiaBlazor.Geocode.Photon;

public interface IPhotonRequestMixin
{
    /// <summary>
    ///
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    ///
    /// </summary>
    public ISet<string> LayerFilters { get; set; }

    /// <summary>
    ///
    /// </summary>
    public IDictionary<string, string> OsmTagFilters { get; set; }
}
