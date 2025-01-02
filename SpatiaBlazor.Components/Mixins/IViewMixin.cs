namespace SpatiaBlazor.Components.Mixins;

public interface IViewMixin
{
    void TriggerRender();

    void StartLoading();

    void StopLoading();
}
