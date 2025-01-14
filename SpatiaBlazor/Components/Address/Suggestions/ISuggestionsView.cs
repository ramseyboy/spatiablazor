using SpatiaBlazor.Components.Mixins;

namespace SpatiaBlazor.Components.Address.Suggestions;

public interface ISuggestionsView : IViewMixin
{
    public AddressSuggestionsParametersViewModel SuggestionsParameters { get; set; }
}
