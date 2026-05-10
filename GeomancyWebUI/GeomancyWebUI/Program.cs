using GeomancyWebUI.Components;
using GeomancyWebUI.Client.Services;
using GeomancyWebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Register HttpClient for API calls (server-side)
// Configure AFTER AddServiceDefaults to ensure our BaseAddress is used
builder.Services.AddHttpClient<IGeomancyService, GeomancyApiService>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["GeomancyApi:BaseUrl"] ?? "http://localhost:5000/api/geomancy";
    // Ensure the BaseAddress is set correctly - trim trailing slash
    var uri = new Uri(baseUrl.TrimEnd('/'));
    client.BaseAddress = uri;
});

// Register ThemeService as Singleton for session-based state
builder.Services.AddSingleton<ThemeService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GeomancyWebUI.Client._Imports).Assembly);

app.Run();
