using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SpatiaBlazor.Components.Geocode.Suggestions;
using SpatiaBlazor.Components.Geocode.Suggestions.Photon;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddMudServices();

builder.Services.AddPhotonAddressSuggestionsComponent();

await builder.Build().RunAsync();
