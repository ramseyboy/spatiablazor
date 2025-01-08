using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Geocode;

public interface IAddressFormView : IViewMixin
{
    public bool IsFormValid();
    public Task TriggerFormValidation();
}
