# Deploying Geofancy to Railway

The `Dockerfile` at the repo root packages the .NET 8 Blazor Web project
(`GeomancyWebUI`) as a single Linux container. The legacy .NET Framework 4.8
projects (`GeomancyApp` WinForms desktop, `GeomancyAPI` self-host) are
intentionally **excluded** from the container - they stay Windows-only and are
unaffected by the deploy.

## Local smoke test

```bash
docker build -t geofancy:local .
docker run --rm -p 8080:8080 -e PORT=8080 geofancy:local
```

Then open <http://localhost:8080>. The "Cast a chart!" button should land on
the desktop or mobile workspace and figures should populate (proves the
in-process API is wired up).

## One-time Railway setup

1. Create a Railway account and a new project.
2. **New Project -> Deploy from GitHub repo** -> pick this repository.
3. **Settings -> Source -> Branch** -> set to `web-app` (or whichever branch
   contains the `Dockerfile` + `GeomancyWebUI/`). The `master` branch is the
   pre-refactor WinForms snapshot and will fail the Railpack auto-detect with
   "could not determine how to build the app" because it has no `Dockerfile`.
4. **Settings -> Build -> Builder** -> `Dockerfile` (the committed
   `railway.toml` already pins this, but confirm it stuck). Dockerfile path is
   `Dockerfile` at the repo root.
5. **Settings -> Networking -> Generate Domain**. You get a free
   `*.up.railway.app` URL with HTTPS handled at the edge.
6. No environment variables are required for v1. Railway injects `$PORT` and
   the `Dockerfile` honors it.

## Architecture notes

- The container only includes the SDK-style projects: `Geomancy.Core`,
  `Geomancy.Api.Contracts`, `Geomancy.Api.Handlers`, `GeomancyApp.ServiceDefaults`,
  `GeomancyWebUI.Client`, and `GeomancyWebUI`. All `netstandard2.0` or `net8.0`,
  no Windows-only references.
- `GeomancyApi:UseInProcess` defaults to `true` in `appsettings.json`, so the
  Blazor Server interactive components call `GeomancyAPI.Handlers.GeomancyHandlers`
  directly (no HTTP loopback) in production.
- The WASM client always talks same-origin to `/api/geomancy/`, served by
  `GeomancyWebUI/Controllers/GeomancyController.cs` (an ASP.NET Core port that
  also delegates to the same handlers).
- HTTPS redirect is wrapped in `IsDevelopment()` so Railway's TLS termination
  at the edge doesn't loop.

## Dev environment is unchanged

After the refactor:

- WinForms desktop still builds under Visual Studio against `Geomancy.Core`.
- F4.8 self-host API still runs on `localhost:5000`; its controller is now
  thin shims that delegate to `Geomancy.Api.Handlers`.
- Blazor Web in dev defaults to in-process. Set
  `"GeomancyApi:UseInProcess": false` in `appsettings.Development.json` to
  switch back to the F4.8 self-host for A/B comparisons.
