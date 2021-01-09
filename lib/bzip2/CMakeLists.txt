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

set(BZIP2_PATH ./1.0.8)

add_library(bzip2 SHARED
    ${BZIP2_PATH}/blocksort.c
    ${BZIP2_PATH}/huffman.c
    ${BZIP2_PATH}/crctable.c
    ${BZIP2_PATH}/randtable.c
    ${BZIP2_PATH}/compress.c
    ${BZIP2_PATH}/decompress.c
    ${BZIP2_PATH}/bzlib.c
    )
if(MSVC)
    target_sources(bzip2 PRIVATE ${BZIP2_PATH}/libbz2.def)
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
    PUBLIC ${BZIP2_PATH}
    )
target_compile_definitions(bzip2 PRIVATE _FILE_OFFSET_BITS=64)
