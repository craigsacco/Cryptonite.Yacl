add_library(Cryptonite.Yacl.XZ.Native STATIC
    ${LIB_XZUTILS_PATH}/src/common/tuklib_physmem.c
    ${LIB_XZUTILS_PATH}/src/common/tuklib_cpucores.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/common.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_util.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/easy_preset.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_common.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/hardware_physmem.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/index.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_flags_common.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/vli_size.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/hardware_cputhreads.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/alone_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_buffer_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_header_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/easy_buffer_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/easy_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/easy_encoder_memusage.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_buffer_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_flags_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/index_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_buffer_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_flags_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/vli_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/outqueue.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_encoder_mt.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/alone_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/auto_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_buffer_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/block_header_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/easy_decoder_memusage.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_buffer_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/filter_flags_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/index_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/index_hash.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_buffer_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/stream_flags_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/common/vli_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/check/crc32_table.c
    ${LIB_XZUTILS_PATH}/src/liblzma/check/crc32_fast.c
    ${LIB_XZUTILS_PATH}/src/liblzma/check/crc64_table.c
    ${LIB_XZUTILS_PATH}/src/liblzma/check/crc64_fast.c
    ${LIB_XZUTILS_PATH}/src/liblzma/check/sha256.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lz/lz_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lz/lz_encoder_mf.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lz/lz_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder_presets.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder_optimum_fast.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_encoder_optimum_normal.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/fastpos_table.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma2_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/lzma/lzma2_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/rangecoder/price_table.c
    ${LIB_XZUTILS_PATH}/src/liblzma/delta/delta_common.c
    ${LIB_XZUTILS_PATH}/src/liblzma/delta/delta_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/delta/delta_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/simple_coder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/simple_encoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/simple_decoder.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/x86.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/powerpc.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/ia64.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/arm.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/armthumb.c
    ${LIB_XZUTILS_PATH}/src/liblzma/simple/sparc.c
    )
    
# language definition
set_property(TARGET Cryptonite.Yacl.XZ.Native PROPERTY LINKER_LANGUAGE C)

# C includes and preprocessor defines
target_include_directories(Cryptonite.Yacl.XZ.Native
    PUBLIC  ${LIB_XZUTILS_PATH}/src/liblzma/api
    PRIVATE ${LIB_XZUTILS_PATH}/src/common
            ${LIB_XZUTILS_PATH}/src/liblzma/common
            ${LIB_XZUTILS_PATH}/src/liblzma/check
            ${LIB_XZUTILS_PATH}/src/liblzma/lz
            ${LIB_XZUTILS_PATH}/src/liblzma/lzma
            ${LIB_XZUTILS_PATH}/src/liblzma/rangecoder
            ${LIB_XZUTILS_PATH}/src/liblzma/delta
            ${LIB_XZUTILS_PATH}/src/liblzma/simple
    )
target_compile_definitions(Cryptonite.Yacl.XZ.Native PRIVATE HAVE_CONFIG_H=1)

# include appropriate config.h header depending on compiler
if(MSVC)
    if(MSVC_TOOLSET_VERSION EQUAL 120)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${LIB_XZUTILS_PATH}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 140)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${LIB_XZUTILS_PATH}/windows/vs2013)
    elseif(MSVC_TOOLSET_VERSION EQUAL 141)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${LIB_XZUTILS_PATH}/windows/vs2017)
    elseif(MSVC_TOOLSET_VERSION EQUAL 142)
        target_include_directories(Cryptonite.Yacl.XZ.Native PRIVATE ${LIB_XZUTILS_PATH}/windows/vs2019)
    else()
        error("Unsupported MSVC version")
    endif()
else()
    error("Only MSVC is supported")
endif()
