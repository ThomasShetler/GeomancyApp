# syntax=docker/dockerfile:1
# ---------------------------------------------------------------------------
# Multi-stage build for the Geofancy Blazor Web project.
#
# Note: this Dockerfile intentionally builds *only* GeomancyWebUI.csproj, NOT
# the full GeomancyApp.sln. The two .NET Framework 4.8 projects in the
# solution (GeomancyApp WinForms desktop and GeomancyAPI self-host) cannot
# build on Linux. The Blazor project transitively pulls in everything it
# actually needs (Geomancy.Core, Geomancy.Api.Contracts, Geomancy.Api.Handlers,
# GeomancyApp.ServiceDefaults, GeomancyWebUI.Client) - all SDK-style and
# netstandard2.0 / net8.0, so they restore and publish cleanly here.
# ---------------------------------------------------------------------------

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the SDK-style projects + their JSON data dirs that GeomancyWebUI
# transitively depends on. Skipping the F4.8 projects keeps the build fast
# and avoids the Linux-incompatible old-style csproj files.
COPY GeomancyApp.ServiceDefaults/ GeomancyApp.ServiceDefaults/
COPY Geomancy.Core/ Geomancy.Core/
COPY Geomancy.Api.Contracts/ Geomancy.Api.Contracts/
COPY Geomancy.Api.Handlers/ Geomancy.Api.Handlers/
COPY databank/ databank/
COPY GeomancyWebUI/ GeomancyWebUI/

RUN dotnet publish GeomancyWebUI/GeomancyWebUI/GeomancyWebUI.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:PublishReadyToRun=true

# Publish emits a fingerprinted Client scoped-css bundle; styles.css @import and
# App.razor expect a stable GeomancyWebUI.Client.bundle.scp.css filename.
RUN set -e; cd /app/publish/wwwroot; \
    bundle=$(ls GeomancyWebUI.Client.*.bundle.scp.css 2>/dev/null | head -n1); \
    if [ -n "$bundle" ] && [ ! -f GeomancyWebUI.Client.bundle.scp.css ]; then \
      cp "$bundle" GeomancyWebUI.Client.bundle.scp.css; \
    fi

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Railway sets $PORT; default to 8080 for local docker run.
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
# Small PaaS: workstation GC + R2R cuts cold-start / circuit stutter vs Server GC.
ENV DOTNET_EnableDiagnostics=0 \
    DOTNET_ReadyToRun=1 \
    DOTNET_TieredCompilation=1 \
    DOTNET_TC_QuickJit=1 \
    DOTNET_TC_QuickJitForLoops=1 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    DOTNET_gcServer=0 \
    DOTNET_GCConserveMemory=5
EXPOSE 8080

ENTRYPOINT ["dotnet", "GeomancyWebUI.dll"]
