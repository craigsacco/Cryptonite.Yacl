#!powershell

& ${Env:Programfiles}\CMake\bin\cmake.exe -S . -B build
& ${Env:Programfiles}\CMake\bin\cmake.exe --build build