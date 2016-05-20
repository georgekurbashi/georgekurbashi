cls
echo off
cls

set scriptName=C# Tree 2.cmd

set m=0
for %%f in ("%CD%\*") do (if "%%~xf" EQU ".sln" set m=1)
if %m%==0 (echo No solution file found in this directory! & pause & exit /B)

set n=0
for %%f in ("%CD%\*") do (if "%%~nxf" EQU "nunit.framework.dll" set n=1)
if %n%==0 (echo Copy the nunit.framework.dll file into script directory! & pause & exit /B)

md Source

for /d %%f in ("%CD%\*") do (xcopy "%%~nxf" "%CD%\Source\%%~nxf" /H /I /E && rmdir "%%f" /s /q)

xcopy "%CD%" "%CD%\Source" /h /k
del "%CD%\Source\%scriptName%" /Q /F

for %%f in ("%CD%\*") do (if "%%~nxf" NEQ "%scriptName%" (del /F "%%~nxf"))
del "%CD%" /Q /F /A:h

md Lib
xcopy "%CD%\Source\nunit.framework.dll" "%CD%\Lib" /k
del "%CD%\Source\nunit.framework.dll" /Q /F

md Build\AnyCPU\Debug
md Build\AnyCPU\Release
echo on

exit /B