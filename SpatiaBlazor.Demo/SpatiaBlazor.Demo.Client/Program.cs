using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SpatiaBlazor.Components.Address.Suggestions;
using SpatiaBlazor.Geocode.Google;
using SpatiaBlazor.Geocode.Photon;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddMudServices();

builder.Services.AddPhotonGeocodeClient();
builder.Services.AddGooglePlacesGeocodeClient();
builder.Services.AddAddressSuggestions();

await builder.Build().RunAsync();
