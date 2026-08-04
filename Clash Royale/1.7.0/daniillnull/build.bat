@echo off
set PROJECT_DIR=../daniillnull
set BIN_DIR=%PROJECT_DIR%\bin

echo Building Java project...

if not exist "%BIN_DIR%" (
    mkdir "%BIN_DIR%"
)

cd /d "%PROJECT_DIR%"

dir /s /b *.java > sources.txt

javac -d "%BIN_DIR%" @sources.txt

del sources.txt

if %ERRORLEVEL% NEQ 0 (
    echo Build failed.
    pause
    exit /b %ERRORLEVEL%
)

echo Build successful!
pause