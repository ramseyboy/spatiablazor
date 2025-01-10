using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Address;

internal interface IAddressFormView : IViewMixin
{
    public bool IsFormValid();
    public Task TriggerFormValidation();
}
