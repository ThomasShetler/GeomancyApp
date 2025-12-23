using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GeomancyWebUI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register HttpClient for API calls
builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["GeomancyApi:BaseUrl"] ?? "http://localhost:5000/api/geomancy";
    return new HttpClient { BaseAddress = new Uri(baseUrl) };
});

// Register Geomancy Service
builder.Services.AddScoped<IGeomancyService, GeomancyApiService>();

await builder.Build().RunAsync();
