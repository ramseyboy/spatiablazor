using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Address;

public interface IAddressFormView : IViewMixin
{
    public bool IsFormValid();
    public Task TriggerFormValidation();
}
