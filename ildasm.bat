@ECHO OFF

REM "C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\x64\ildasm.exe" /LINENUM /SOURCE /BYTES /NOBAR %1 /out=%2 

REM "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\x64\ildasm.exe" /LINENUM /SOURCE /BYTES /NOBAR %1 /out=%2 

"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\x64\ildasm.exe" /NOBAR %1 /out=%2 
