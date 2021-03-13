@echo off
:waitforpid
tasklist /fi "pid eq %1" 2>nul | find "%1" >nul
if %ERRORLEVEL%==0 (
	timeout /t 2 /nobreak >nul
	goto :waitforpid
)
echo Process exited,Waiting more and then build!
timeout /t 3 /nobreak >nul

rmdir "D:\YasharRemote\Projects\Remote-Control" /s /q 
git clone "https://github.com/yasharvc/Remote-Control.git"
cd "D:\YasharRemote\Projects\Remote-Control"
rmdir "D:\YasharRemote\Remote\app" /s /q
dotnet publish -c Release --self-contained --runtime win-x86 -o "D:\YasharRemote\Remote\app"

ECHO "This script will now self-destruct. Please ignore the next error message"
DEL "%~f0"