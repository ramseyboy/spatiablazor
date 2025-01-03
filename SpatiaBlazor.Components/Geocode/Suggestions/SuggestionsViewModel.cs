using NetTopologySuite.Geometries;

namespace SpatiaBlazor.Components.Geocode.Suggestions;

public class SuggestionsViewModel
{
    public Point? BiasLocation { get; set; }

    public Envelope? BBox { get; set; }

    public string? Language { get; set; }
}
