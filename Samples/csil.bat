@echo off
%VS_CSC% %1.cs
%VS_ILDASM% %1.exe /NOBAR /OUT:%1.il