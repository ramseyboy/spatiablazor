using NetTopologySuite.Geometries;
using SpatiaBlazor.Geocode.Photon;

namespace SpatiaBlazor.Components.Geocode.Suggestions.Photon;

public sealed class PhotonGeocodeResultsViewModel : IGeocodeResultsViewModel
{
    public PhotonGeocodeResultsViewModel(PhotonGeocodeResponse response)
    {
        Id = response.Id;
        Location = response.Geom as Point; //todo handle non point
        Label = CreateLabel(response);
    }

    public object Id { get; }
    public Point Location { get;}
    public string Label { get; set; }

    private string CreateLabel(PhotonGeocodeResponse response)
    {
        return response.StateOrProvince ?? string.Empty;
    }
}
