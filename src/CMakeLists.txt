cmake_minimum_required(VERSION 3.16)

# project definition
project(Cryptonite.Yacl VERSION 0.0.0 LANGUAGES C CXX CSharp)

# project output directories
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

# only support Debug and Release configuration types
set(CMAKE_CONFIGURATION_TYPES "Debug;Release") 

# .NET target framework
set(CMAKE_DOTNET_TARGET_FRAMEWORK "netcoreapp3.1")

# paths to dependent 3rd-party library
set(YACL_LIB_BZIP2_PATH ../../lib/bzip2/1.0.8)
set(YACL_LIB_XZUTILS_PATH ../../lib/xz-utils/5.2.5)

# options
option(YACL_GZIP "GZip support" ON)
option(YACL_BZIP2 "BZip support" ON)
option(YACL_XZ "XZ support" ON)
option(YACL_TESTS "Build test projects" ON)

# Common components
add_subdirectory(Cryptonite.Yacl.Common)
if(YACL_TESTS)
    add_subdirectory(Cryptonite.Yacl.Tests.Common)
endif()

# GZip components
if(YACL_GZIP)
    add_subdirectory(Cryptonite.Yacl.GZip)
    if(YACL_TESTS)
        add_subdirectory(Cryptonite.Yacl.Tests.GZip)
    endif()
endif()

# BZip2 components
if(YACL_BZIP2)
    add_subdirectory(Cryptonite.Yacl.BZip2)
    add_subdirectory(Cryptonite.Yacl.BZip2.Native)
    add_subdirectory(Cryptonite.Yacl.BZip2.Managed)
    if(YACL_TESTS)
        add_subdirectory(Cryptonite.Yacl.Tests.BZip2)
    endif()
endif()

# XZ components
if(YACL_XZ)
    add_subdirectory(Cryptonite.Yacl.XZ)
    add_subdirectory(Cryptonite.Yacl.XZ.Native)
    add_subdirectory(Cryptonite.Yacl.XZ.Managed)
    if(YACL_TESTS)
        add_subdirectory(Cryptonite.Yacl.Tests.XZ)
    endif()
endif()

# Console application
if(YACL_GZIP AND YACL_BZIP2 AND YACL_XZ)
    add_subdirectory(Cryptonite.Yacl.Cmd)
endif()
