using GeomancyWebUI.Components;
using GeomancyWebUI.Client.Services;
using GeomancyWebUI.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Railway (and most PaaS) terminates TLS at the edge and forwards plain HTTP to
// the container. Without this, ASP.NET Core sees Request.Scheme="http" and
// Blazor's /_blazor/negotiate response advertises ws:// URLs that the browser
// blocks as mixed content, breaking the SignalR circuit and showing the
// "An unhandled error has occurred" toast.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedHost;
    // Railway's edge IPs aren't known in advance; trust whatever proxy hops
    // the request came through. Safe because the container is only reachable
    // via Railway's edge in the first place.
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

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

// Per Blazor Server circuit — theme must not be Singleton (shared across all users/tabs).
builder.Services.AddScoped<ThemeService>();

var app = builder.Build();

// MUST run before any middleware that reads Request.Scheme/Host (HTTPS redirect,
// auth, antiforgery, Blazor circuit URL building, etc.). See the
// ForwardedHeadersOptions configuration above for why this matters on Railway.
app.UseForwardedHeaders();

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
