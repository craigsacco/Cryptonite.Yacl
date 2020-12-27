add_library(Cryptonite.Yaal.BZip2.Managed SHARED
    NativeInterface.cpp
    NativeInterfaceException.cpp
    
    NativeInterface.h
    NativeInterfaceException.h
    )

# project dependencies
target_link_libraries(Cryptonite.Yaal.BZip2.Managed
    PRIVATE Cryptonite.Yaal.BZip2.Native
    )

# language definition
set_property(TARGET Cryptonite.Yaal.BZip2.Managed PROPERTY LINKER_LANGUAGE CXX)

# CLR mode
set_property(TARGET Cryptonite.Yaal.BZip2.Managed PROPERTY COMMON_LANGUAGE_RUNTIME "")

# C++ includes
target_include_directories(Cryptonite.Yaal.BZip2.Managed
    PRIVATE .
    )