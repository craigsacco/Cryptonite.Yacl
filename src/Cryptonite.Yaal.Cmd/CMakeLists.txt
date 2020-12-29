add_executable(Cryptonite.Yaal.Cmd
    App.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yaal.Cmd
    Cryptonite.Yaal.Common
    Cryptonite.Yaal.BZip2
    )

# language definition
set_property(TARGET Cryptonite.Yaal.Cmd PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yaal.Cmd PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
set_property(TARGET Cryptonite.Yaal.Cmd PROPERTY VS_PACKAGE_REFERENCES
    "NDesk.Options.Core_1.2.5"
    )
