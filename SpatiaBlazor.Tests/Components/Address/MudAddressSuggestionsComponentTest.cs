using Bunit;
using FakeItEasy;
using MudBlazor.Services;
using SpatiaBlazor.Components.Address.Suggestions;
using Xunit.Abstractions;

namespace SpatiaBlazor.Tests.Components.Address;

public class MudAddressSuggestionsComponentTest(ITestOutputHelper testOutputHelper) : TestContext, IAsyncLifetime
{
    /// <inheritdoc />
    public Task InitializeAsync()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        var serviceCollection = ServiceCollectionHelpers.CreateServiceCollection(testOutputHelper);
        foreach (var serviceDescriptor in serviceCollection)
        {
            Services.Add(serviceDescriptor);
        }

        Services.AddMudServices();
        Services.AddAddressSuggestions();
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact(DisplayName = "Given a valid service collection with dependencies, when schedule component rendered, the view should be in a valid state")]
    public void TestRenderingOfComponent()
    {
        var mockPresenter = A.Fake<ISuggestionsPresenter>();
        Services.SwapTransient(mockPresenter);

        var cut = RenderComponent<MudAddressSuggestionsComponent>();

        var view = cut.Instance;
        Assert.NotNull(view);
    }
}
