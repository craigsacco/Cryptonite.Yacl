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

cmake_minimum_required(VERSION 3.12)

project(bzip2 VERSION 1.0.8 LANGUAGES C)

# only support Debug and Release configuration types
set(CMAKE_CONFIGURATION_TYPES "Debug;Release")

# project output directories
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG ${CMAKE_BINARY_DIR}/bin/Debug)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE ${CMAKE_BINARY_DIR}/bin/Release)

# bzip2 library
set(BZIP2_LIB_ROOT ./${CMAKE_PROJECT_VERSION})
add_library(bzip2 SHARED
    ${BZIP2_LIB_ROOT}/blocksort.c
    ${BZIP2_LIB_ROOT}/huffman.c
    ${BZIP2_LIB_ROOT}/crctable.c
    ${BZIP2_LIB_ROOT}/randtable.c
    ${BZIP2_LIB_ROOT}/compress.c
    ${BZIP2_LIB_ROOT}/decompress.c
    ${BZIP2_LIB_ROOT}/bzlib.c
    )
if(MSVC)
    target_sources(bzip2 PRIVATE ${BZIP2_LIB_ROOT}/libbz2.def)
endif()

# set output filename
if(WIN32)
    set_property(TARGET bzip2 PROPERTY OUTPUT_NAME libbz2)
else()
    set_property(TARGET bzip2 PROPERTY OUTPUT_NAME bz2)
endif()

# language definition
set_property(TARGET bzip2 PROPERTY LINKER_LANGUAGE C)

# C includes and preprocessor defines
target_include_directories(bzip2
    PUBLIC ${BZIP2_LIB_ROOT}
    )
target_compile_definitions(bzip2 PRIVATE _FILE_OFFSET_BITS=64)
