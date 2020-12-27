add_library(Cryptonite.Yaal.Common SHARED
    StreamUtilities.cs
    )

# language definition
set_property(TARGET Cryptonite.Yaal.Common PROPERTY LINKER_LANGUAGE CSharp)

# .NET target version and references
set_property(TARGET Cryptonite.Yaal.Common PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION})
set_property(TARGET Cryptonite.Yaal.Common PROPERTY VS_DOTNET_REFERENCES
    "Microsoft.CSharp"
    "System"
    "System.Core"
    "WindowsBase"
    )
