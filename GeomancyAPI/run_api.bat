@echo off
echo Starting Geomancy API with IIS Express...
echo.

REM Try to find IIS Express
set "IISEXPRESS_PATH="
if exist "C:\Program Files (x86)\IIS Express\iisexpress.exe" (
    set "IISEXPRESS_PATH=C:\Program Files (x86)\IIS Express\iisexpress.exe"
) else if exist "C:\Program Files\IIS Express\iisexpress.exe" (
    set "IISEXPRESS_PATH=C:\Program Files\IIS Express\iisexpress.exe"
)

if "%IISEXPRESS_PATH%"=="" (
    echo IIS Express not found. Trying to run as self-hosted application...
    echo.
    echo If you get connection refused errors, try running as Administrator.
    echo.
    pause
    start "" "GeomancyAPI.exe"
) else (
    echo Found IIS Express at: %IISEXPRESS_PATH%
    echo Starting API on http://localhost:8080
    echo Swagger UI will be available at: http://localhost:8080/swagger
    echo.
    echo Press Ctrl+C to stop the server
    echo.
    "%IISEXPRESS_PATH%" /path:"%~dp0" /port:8080
)

pause 