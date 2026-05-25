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
3. **Settings -> Source -> Branch** -> set to **`master`** for production deploys.
   The repo's `master` branch contains the `Dockerfile` and `GeomancyWebUI/`.
   Use **`web-app`** as the integration / testing branch; merge tested work into
   `master` via pull request to ship to Railway.
4. **Settings -> Build -> Builder** -> `Dockerfile` (the committed
   `railway.toml` already pins this, but confirm it stuck). Dockerfile path is
   `Dockerfile` at the repo root.
5. **Settings -> Networking -> Generate Domain**. You get a free
   `*.up.railway.app` URL with HTTPS handled at the edge.
6. No environment variables are required for v1. Railway injects `$PORT` and
   the `Dockerfile` honors it.

## Test vs production (two Railway services)

Geofancy uses **two deploy targets** in the same GitHub repo:

| Environment | Git branch | Typical URL |
|-------------|------------|-------------|
| **Production** | `master` | `https://geofancy.up.railway.app` (or your custom domain) |
| **Testing** | `web-app` | Second `*.up.railway.app` domain on a separate Railway **service** |

`railway.toml` only sets the Docker builder; **branch and domain are configured in the Railway dashboard per service**, not in git.

### Checklist when test works but production does not

1. **Confirm you are on the production URL**, not the test service domain. Bookmarks and “Add to Home Screen” icons often still point at the old test hostname.
2. **Production service → Settings → Source → Branch** must be **`master`**. The test service must be **`web-app`**. A redeploy on the wrong service looks successful but does not update prod.
3. **Production → Deployments** → latest deployment should show commit **`Release v1.0.5`** (or newer). If it still shows **v1.0.4**, trigger **Redeploy** on `master` or push an empty commit to `master`.
4. **Custom domain** (if any): **Networking** on the **production** service only. A domain attached to the test service will keep serving the test branch.
5. **Verify version on the server** (not only in the UI after Blazor loads):
   - Open prod home page → **View source** → search for `v1.0.5` or `favicon.png?v=1.0.5`.
   - Or run: `curl -s https://geofancy.up.railway.app/ | findstr 1.0.5` (Windows) / `grep 1.0.5` (macOS/Linux).
6. **Phone / browser cache**: Static JS and CSS were previously cached without a version query string. After **v1.0.5+**, assets use `?v={version}` and `Cache-Control: no-cache` on `.js`/`.css`. On the phone: Safari → clear website data for the prod host, or open prod in a **private** tab. Remove and re-add the home-screen shortcut if you use one.
7. **Stuck Docker layer cache** (rare): Production service → **Settings → Build** → redeploy with cache cleared, or set env `NO_CACHE=1` for one build (Railway UI varies by plan).

### Quick “is prod actually updated?” test

```bash
curl -s https://geofancy.up.railway.app/clipboard.js | findstr geofancyFindFigureDetailScroller
```

If that string is present, the container is on the v1.0.5 mobile scroll fix; any remaining bug is almost certainly **client cache** or **wrong hostname**.

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
