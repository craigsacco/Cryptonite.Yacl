add_library(Cryptonite.Yacl.XZ.Managed SHARED
    NativeInterface.cpp
    NativeInterfaceException.cpp
    
    NativeInterface.h
    NativeInterfaceException.h
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.XZ.Managed
    PRIVATE Cryptonite.Yacl.XZ.Native
    )

# language definition
set_property(TARGET Cryptonite.Yacl.XZ.Managed PROPERTY LINKER_LANGUAGE CXX)

# CLR mode
set_property(TARGET Cryptonite.Yacl.XZ.Managed PROPERTY COMMON_LANGUAGE_RUNTIME "")

# C++ includes
target_include_directories(Cryptonite.Yacl.XZ.Managed
    PRIVATE .
    )