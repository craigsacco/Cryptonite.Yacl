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

set(XZ_UTILS_PATH ./5.2.5)

add_library(xz-utils SHARED
    ${XZ_UTILS_PATH}/src/common/tuklib_physmem.c
    ${XZ_UTILS_PATH}/src/common/tuklib_cpucores.c
    ${XZ_UTILS_PATH}/src/liblzma/common/common.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_util.c
    ${XZ_UTILS_PATH}/src/liblzma/common/easy_preset.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_common.c
    ${XZ_UTILS_PATH}/src/liblzma/common/hardware_physmem.c
    ${XZ_UTILS_PATH}/src/liblzma/common/index.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_flags_common.c
    ${XZ_UTILS_PATH}/src/liblzma/common/vli_size.c
    ${XZ_UTILS_PATH}/src/liblzma/common/hardware_cputhreads.c
    ${XZ_UTILS_PATH}/src/liblzma/common/alone_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_buffer_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_header_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/easy_buffer_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/easy_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/easy_encoder_memusage.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_buffer_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_flags_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/index_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_buffer_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_flags_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/vli_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/outqueue.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_encoder_mt.c
    ${XZ_UTILS_PATH}/src/liblzma/common/alone_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/auto_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_buffer_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/block_header_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/easy_decoder_memusage.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_buffer_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/filter_flags_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/index_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/index_hash.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_buffer_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/stream_flags_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/common/vli_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/check/check.c
    ${XZ_UTILS_PATH}/src/liblzma/check/crc32_table.c
    ${XZ_UTILS_PATH}/src/liblzma/check/crc32_fast.c
    ${XZ_UTILS_PATH}/src/liblzma/check/crc64_table.c
    ${XZ_UTILS_PATH}/src/liblzma/check/crc64_fast.c
    ${XZ_UTILS_PATH}/src/liblzma/check/sha256.c
    ${XZ_UTILS_PATH}/src/liblzma/lz/lz_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/lz/lz_encoder_mf.c
    ${XZ_UTILS_PATH}/src/liblzma/lz/lz_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma_encoder_presets.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma_encoder_optimum_fast.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma_encoder_optimum_normal.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/fastpos_table.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma2_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/lzma/lzma2_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/rangecoder/price_table.c
    ${XZ_UTILS_PATH}/src/liblzma/delta/delta_common.c
    ${XZ_UTILS_PATH}/src/liblzma/delta/delta_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/delta/delta_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/simple_coder.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/simple_encoder.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/simple_decoder.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/x86.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/powerpc.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/ia64.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/arm.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/armthumb.c
    ${XZ_UTILS_PATH}/src/liblzma/simple/sparc.c
    )
if(MSVC)
    target_sources(xz-utils PRIVATE ${XZ_UTILS_PATH}/src/liblzma/liblzma_w32res.rc)
endif()

# set output filename
if(WIN32)
    set_property(TARGET xz-utils PROPERTY OUTPUT_NAME liblzma)
else()
    set_property(TARGET xz-utils PROPERTY OUTPUT_NAME lzma)
endif()

# language definition
set_property(TARGET xz-utils PROPERTY LINKER_LANGUAGE C)

# C includes and preprocessor defines
target_include_directories(xz-utils
    PUBLIC  ${XZ_UTILS_PATH}/src/liblzma/api
    PRIVATE ${XZ_UTILS_PATH}/src/common
            ${XZ_UTILS_PATH}/src/liblzma/common
            ${XZ_UTILS_PATH}/src/liblzma/check
            ${XZ_UTILS_PATH}/src/liblzma/lz
            ${XZ_UTILS_PATH}/src/liblzma/lzma
            ${XZ_UTILS_PATH}/src/liblzma/rangecoder
            ${XZ_UTILS_PATH}/src/liblzma/delta
            ${XZ_UTILS_PATH}/src/liblzma/simple
    )
target_compile_definitions(xz-utils PRIVATE HAVE_CONFIG_H=1 DLL_EXPORT LIBLZMADLL_EXPORTS)

# include appropriate config.h header depending on compiler
if(MSVC)
    if(MSVC_TOOLSET_VERSION EQUAL 120)
        target_include_directories(xz-utils PRIVATE ${XZ_UTILS_PATH}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 140)
        target_include_directories(xz-utils PRIVATE ${XZ_UTILS_PATH}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 141)
        target_include_directories(xz-utils PRIVATE ${XZ_UTILS_PATH}/windows/vs2017)
    elseif(MSVC_TOOLSET_VERSION EQUAL 142)
        target_include_directories(xz-utils PRIVATE ${XZ_UTILS_PATH}/windows/vs2019)
    else()
        error("Unsupported MSVC version")
    endif()
endif()
