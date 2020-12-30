add_executable(Cryptonite.Yacl.Cmd
    App.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.Cmd
    Cryptonite.Yacl.Common
    Cryptonite.Yacl.BZip2
    Cryptonite.Yacl.GZip
    Cryptonite.Yacl.XZ
    )

# language definition
set_property(TARGET Cryptonite.Yacl.Cmd PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.Cmd PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
set_property(TARGET Cryptonite.Yacl.Cmd PROPERTY VS_PACKAGE_REFERENCES
    "NDesk.Options.Core_1.2.5"
    )
