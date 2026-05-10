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
COPY HouseAndCourtDirectory/ HouseAndCourtDirectory/
COPY WayOfPointsDirectory/ WayOfPointsDirectory/
COPY GeomancyWebUI/ GeomancyWebUI/

RUN dotnet publish GeomancyWebUI/GeomancyWebUI/GeomancyWebUI.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Railway sets $PORT; default to 8080 for local docker run.
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE 8080

ENTRYPOINT ["dotnet", "GeomancyWebUI.dll"]
