@echo off
set csc="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe"

for /f %%f in ('dir /b .\*.cs') do (
	%csc% /nologo /target:exe %%f
)
