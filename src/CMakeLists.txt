cmake_minimum_required(VERSION 3.16)

# project definition
project(Cryptonite.Yacl VERSION 0.0.0 LANGUAGES C CXX CSharp)

# project output directories
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

# only support Debug and Release configuration types
set(CMAKE_CONFIGURATION_TYPES "Debug;Release") 

# 3rd-party library directories
set(LIB_BZIP2_PATH ../../lib/bzip2-1.0.8)
set(LIB_XZUTILS_PATH ../../lib/xz-5.2.5)

# .NET target framework
set(CMAKE_DOTNET_TARGET_FRAMEWORK "netcoreapp3.1")

# library subdirectories
add_subdirectory(Cryptonite.Yacl.Common)
add_subdirectory(Cryptonite.Yacl.BZip2)
add_subdirectory(Cryptonite.Yacl.BZip2.Native)
add_subdirectory(Cryptonite.Yacl.BZip2.Managed)
add_subdirectory(Cryptonite.Yacl.GZip)
add_subdirectory(Cryptonite.Yacl.XZ)
add_subdirectory(Cryptonite.Yacl.XZ.Native)
add_subdirectory(Cryptonite.Yacl.XZ.Managed)
add_subdirectory(Cryptonite.Yacl.Tests.Common)
add_subdirectory(Cryptonite.Yacl.Tests.BZip2)
add_subdirectory(Cryptonite.Yacl.Cmd)
