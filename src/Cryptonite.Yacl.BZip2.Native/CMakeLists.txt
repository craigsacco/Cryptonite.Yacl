add_library(Cryptonite.Yacl.BZip2.Native STATIC
    ${YACL_LIB_BZIP2_PATH}/blocksort.c
    ${YACL_LIB_BZIP2_PATH}/huffman.c
    ${YACL_LIB_BZIP2_PATH}/crctable.c
    ${YACL_LIB_BZIP2_PATH}/randtable.c
    ${YACL_LIB_BZIP2_PATH}/compress.c
    ${YACL_LIB_BZIP2_PATH}/decompress.c
    ${YACL_LIB_BZIP2_PATH}/bzlib.c
    )

# language definition
set_property(TARGET Cryptonite.Yacl.BZip2.Native PROPERTY LINKER_LANGUAGE C)

# C includes and preprocessor defines
target_include_directories(Cryptonite.Yacl.BZip2.Native
    PUBLIC ${YACL_LIB_BZIP2_PATH}
    )
target_compile_definitions(Cryptonite.Yacl.BZip2.Native PRIVATE _FILE_OFFSET_BITS=64)
