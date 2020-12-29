cmake_minimum_required(VERSION 3.16)

# project definition
project(Cryptonite.Yaal VERSION 0.0.0 LANGUAGES C CXX CSharp)

# project output directories
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

# 3rd-party library directories
set(LIB_BZIP2_PATH ../../lib/bzip2-1.0.8)

# .NET target framework
#set(CMAKE_DOTNET_TARGET_FRAMEWORK "netcoreapp3.1")  # required for .NET Core/Standard
set(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION "v4.6.1")  # required for .NET Framework

# library subdirectories
add_subdirectory(Cryptonite.Yaal.Common)
add_subdirectory(Cryptonite.Yaal.BZip2)
add_subdirectory(Cryptonite.Yaal.BZip2.Native)
add_subdirectory(Cryptonite.Yaal.BZip2.Managed)
add_subdirectory(Cryptonite.Yaal.GZip)
add_subdirectory(Cryptonite.Yaal.Tests.Common)
add_subdirectory(Cryptonite.Yaal.Tests.BZip2)
add_subdirectory(Cryptonite.Yaal.Cmd)