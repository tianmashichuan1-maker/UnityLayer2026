@echo off
setlocal
cd /d "%~dp0"

echo ========================================
echo Unity Submit Tool
echo ========================================
echo.

where powershell.exe >nul 2>&1
if errorlevel 1 (
    echo [ERROR] PowerShell could not be found.
    echo.
    pause
    exit /b 1
)

powershell.exe -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0submit.ps1"

set "ERR=%ERRORLEVEL%"

if not "%ERR%"=="0" (
    echo.
    echo [ERROR] Submit tool failed. Error code: %ERR%
    echo Please check submit_log.txt if it was created.
    echo.
    pause
    exit /b %ERR%
)

echo.
echo Completed.
pause
exit /b 0
