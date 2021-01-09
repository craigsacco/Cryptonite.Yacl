# MIT License
#
# Copyright (c) 2020-2021 Craig Sacco
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.

cmake_minimum_required(VERSION 3.16)

project(xz VERSION 5.2.5 LANGUAGES C)

# only support Debug and Release configuration types
set(CMAKE_CONFIGURATION_TYPES "Debug;Release")

# project output directories
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG ${CMAKE_BINARY_DIR}/bin/Debug)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE ${CMAKE_BINARY_DIR}/bin/Release)

# xz-utils library
set(BUILD_SHARED_LIBS ON)
set(XZ_UTILS_LIB_ROOT ./${CMAKE_PROJECT_VERSION})
if(MSVC)
    add_compile_definitions(HAVE_CONFIG_H)
    if(MSVC_TOOLSET_VERSION EQUAL 120)
        include_directories(${XZ_UTILS_LIB_ROOT}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 140)
        include_directories(${XZ_UTILS_LIB_ROOT}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 141)
        include_directories(${XZ_UTILS_LIB_ROOT}/windows/vs2017)
    elseif(MSVC_TOOLSET_VERSION EQUAL 142)
        include_directories(${XZ_UTILS_LIB_ROOT}/windows/vs2019)
    else()
        error("Unsupported MSVC version")
    endif()
endif()
add_subdirectory(${XZ_UTILS_LIB_ROOT})
