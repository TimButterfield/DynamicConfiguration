.\src\.paket\paket.bootstrapper.exe
if errorlevel 1 (
    exit /b %errorlevel%
)

.\src\.paket\paket.exe restore
if errorlevel 1 (
    exit /b %errorlevel%
)

.\src\packages\FAKE\tools\FAKE.exe build.fsx %*