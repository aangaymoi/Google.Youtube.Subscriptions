@echo off
FOR /F "tokens=*" %%a IN ('DIR /B /AD /S obj') DO (
	RMDIR /S /Q "%%a"	
	echo %%a deleted.
)

FOR /F "tokens=*" %%a IN ('DIR /B /AD /S .vs') DO (
	RMDIR /S /Q "%%a"	
	echo %%a deleted.
)

rem pause