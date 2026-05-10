using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GeomancyWebUI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// In WASM, always talk to the same origin that served the app. The embedded
// ASP.NET Core controller in GeomancyWebUI handles /api/geomancy on that origin
// (production), and in dev/F4.8-fallback mode the same controller still proxies
// the route through InProcessGeomancyService.
builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(nav.BaseUri.TrimEnd('/') + "/api/geomancy/")
    };
});

builder.Services.AddScoped<IGeomancyService, GeomancyApiService>();

await builder.Build().RunAsync();
