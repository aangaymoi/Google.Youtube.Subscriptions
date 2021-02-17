@echo off
echo Warning!! This file can delete wanted/needed files! Use with caution!
echo Hit enter to continue using this file, or close it if you do not want to run it.
REM pause

for /f "tokens=1 delims=" %%a in ('dir /b /s *.ncb') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s /A:H *.suo') do (
attrib -H "%%a"
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.tlh') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.tli') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.sdf') do (
del /Q "%%a"
echo %%a deleted.
)


for /f "tokens=1 delims=" %%a in ('dir /b /s *.user') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s BuildLog.htm') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.ilk') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.pdb') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.idb') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.obj') do (
del /Q "%%a"
echo %%a deleted.
)


for /f "tokens=1 delims=" %%a in ('dir /b /s *.pch') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.ipch') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.tlog') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.vshost.exe') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.vshost.exe.config') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.vshost.exe.manifest') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.old') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *stdafx.obj') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.exp') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.Build.CppClean.log') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.lastbuildstate') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.intermediate.manifest') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.embed.manifest') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.embed.manifest.res') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *mt.dep') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.Cache') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *Properties.Resources.resources') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *Form1.resources') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *csproj.FileList.txt') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.csproj.FileListAbsolute.txt') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.sbr') do (
del /Q "%%a"
echo %%a deleted.
)

for /f "tokens=1 delims=" %%a in ('dir /b /s *.bsc') do (
del /Q "%%a"
echo %%a deleted.
)

REM pause