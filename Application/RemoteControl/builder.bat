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
cd "D:\YasharRemote\Projects\"
git clone "https://github.com/yasharvc/Remote-Control.git"
rmdir "D:\YasharRemote\Remote\app" /s /q
cd "D:\YasharRemote\Projects\Remote-Control"
dotnet publish -c Release --self-contained --runtime win-x86 -o "D:\YasharRemote\Remote\app"

D:\YasharRemote\Remote\app\RemoteControl.exe
DEL "%~f0"