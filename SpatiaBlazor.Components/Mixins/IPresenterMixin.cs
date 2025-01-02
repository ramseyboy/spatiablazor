namespace SpatiaBlazor.Components.Mixins;

public interface IPresenterMixin<in TVIew> : IDisposable where TVIew : IViewMixin
{
    Task InitializeAsync(TVIew view, CancellationToken cancellationToken = default);
}
