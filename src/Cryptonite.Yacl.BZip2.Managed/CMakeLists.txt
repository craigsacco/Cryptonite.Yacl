add_library(Cryptonite.Yacl.BZip2.Managed SHARED
    NativeInterface.cpp
    NativeInterfaceException.cpp
    
    NativeInterface.h
    NativeInterfaceException.h
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.BZip2.Managed
    PRIVATE Cryptonite.Yacl.BZip2.Native
    )

# language definition
set_property(TARGET Cryptonite.Yacl.BZip2.Managed PROPERTY LINKER_LANGUAGE CXX)

# CLR mode
set_property(TARGET Cryptonite.Yacl.BZip2.Managed PROPERTY COMMON_LANGUAGE_RUNTIME "")

# C++ includes
target_include_directories(Cryptonite.Yacl.BZip2.Managed
    PRIVATE .
    )