namespace SpatiaBlazor.Components.Mixins;

public interface IPresenterMixin<in TVIew> : IDisposable where TVIew : IViewMixin
{
    /// <summary>
    /// Initialize Presenter with view
    /// </summary>
    /// <param name="view"></param>
    /// <param name="token"></param>
    /// <exception cref="OperationCanceledException">thrown when operation has been cancelled</exception>
    /// <exception cref="InvalidOperationException"> thrown when presenter is not initialized or when an invalid state occurs</exception>
    /// <returns>task representing the initialization result</returns>
    Task InitializeAsync(TVIew view, CancellationToken token = default);

    /// <summary>
    /// Handle parameters from view, this will be called whenever view parameters change:
    /// 1. On initialize
    /// 2. on page refresh
    /// 3. on navigation
    /// 4. Any time a rendered component [Parameter] is updated
    /// </summary>
    /// <param name="token"></param>
    /// <exception cref="OperationCanceledException">thrown when operation has been cancelled</exception>
    /// <exception cref="InvalidOperationException"> thrown when presenter is not initialized or when an invalid state occurs</exception>
    /// <returns>task representing the handle result</returns>
    Task HandleParametersAsync(CancellationToken token = default);
}
