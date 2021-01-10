#!powershell

$ErrorActionPreference = "Stop"

& cmake.exe -S . -B build
& cmake.exe --build build