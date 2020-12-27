add_library(Cryptonite.Yaal.BZip2.Native STATIC
    ${LIB_BZIP2_PATH}/blocksort.c
    ${LIB_BZIP2_PATH}/huffman.c
    ${LIB_BZIP2_PATH}/crctable.c
    ${LIB_BZIP2_PATH}/randtable.c
    ${LIB_BZIP2_PATH}/compress.c
    ${LIB_BZIP2_PATH}/decompress.c
    ${LIB_BZIP2_PATH}/bzlib.c
    )

# language definition
set_property(TARGET Cryptonite.Yaal.BZip2.Native PROPERTY LINKER_LANGUAGE C)

# C includes and preprocessor defines
target_include_directories(Cryptonite.Yaal.BZip2.Native
    PUBLIC ${LIB_BZIP2_PATH}
    )
target_compile_definitions(Cryptonite.Yaal.BZip2.Native PRIVATE _FILE_OFFSET_BITS=64)
