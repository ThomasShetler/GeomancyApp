using GeomancyWebUI.Components;
using GeomancyWebUI.Client.Services;
using GeomancyWebUI.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Newtonsoft is required so the [JsonProperty("snake_case")] attributes on the
// reference-directory DTOs serialize the way the WASM client expects.
builder.Services.AddControllers()
    .AddNewtonsoftJson(opts =>
    {
        opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Production default: skip HTTP and call handlers directly. Set
// GeomancyApi:UseInProcess=false in dev to fall back to the F4.8 self-host on :5000.
var useInProcess = builder.Configuration.GetValue("GeomancyApi:UseInProcess", true);
if (useInProcess)
{
    builder.Services.AddScoped<IGeomancyService, InProcessGeomancyService>();
}
else
{
    builder.Services.AddHttpClient<IGeomancyService, GeomancyApiService>((sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();
        var baseUrl = config["GeomancyApi:BaseUrl"] ?? "http://localhost:5000/api/geomancy";
        client.BaseAddress = new Uri(baseUrl.TrimEnd('/'));
    });
}

builder.Services.AddSingleton<ThemeService>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Railway terminates TLS at the edge - HTTPS redirect would loop in prod.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GeomancyWebUI.Client._Imports).Assembly);

app.Run();
