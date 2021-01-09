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

add_library(Cryptonite.Yacl.XZ.Native STATIC
    ${YACL_LIB_XZUTILS_PATH}/src/common/tuklib_physmem.c
    ${YACL_LIB_XZUTILS_PATH}/src/common/tuklib_cpucores.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/common.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_util.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/easy_preset.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_common.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/hardware_physmem.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/index.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_flags_common.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/vli_size.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/hardware_cputhreads.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/alone_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_buffer_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_header_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/easy_buffer_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/easy_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/easy_encoder_memusage.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_buffer_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_flags_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/index_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_buffer_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_flags_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/vli_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/outqueue.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_encoder_mt.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/alone_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/auto_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_buffer_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/block_header_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/easy_decoder_memusage.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_buffer_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/filter_flags_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/index_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/index_hash.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_buffer_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/stream_flags_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common/vli_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check/check.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check/crc32_table.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check/crc32_fast.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check/crc64_table.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check/crc64_fast.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check/sha256.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lz/lz_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lz/lz_encoder_mf.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lz/lz_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder_presets.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder_optimum_fast.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder_optimum_normal.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/fastpos_table.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma2_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma2_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/rangecoder/price_table.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/delta/delta_common.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/delta/delta_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/delta/delta_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/simple_coder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/simple_encoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/simple_decoder.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/x86.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/powerpc.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/ia64.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/arm.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/armthumb.c
    ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple/sparc.c
    )
    
# language definition
set_property(TARGET Cryptonite.Yacl.XZ.Native PROPERTY LINKER_LANGUAGE C)

# C includes and preprocessor defines
target_include_directories(Cryptonite.Yacl.XZ.Native
    PUBLIC  ${YACL_LIB_XZUTILS_PATH}/src/liblzma/api
    PRIVATE ${YACL_LIB_XZUTILS_PATH}/src/common
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/common
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/check
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lz
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/lzma
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/rangecoder
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/delta
            ${YACL_LIB_XZUTILS_PATH}/src/liblzma/simple
    )
target_compile_definitions(Cryptonite.Yacl.XZ.Native PRIVATE HAVE_CONFIG_H=1)

# include appropriate config.h header depending on compiler
if(MSVC)
    if(MSVC_TOOLSET_VERSION EQUAL 120)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${YACL_LIB_XZUTILS_PATH}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 140)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${YACL_LIB_XZUTILS_PATH}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 141)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${YACL_LIB_XZUTILS_PATH}/windows/vs2017)
    elseif(MSVC_TOOLSET_VERSION EQUAL 142)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${YACL_LIB_XZUTILS_PATH}/windows/vs2019)
    else()
        error("Unsupported MSVC version")
    endif()
else()
    error("Only MSVC is supported")
endif()
