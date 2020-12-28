add_library(Cryptonite.Yaal.Common SHARED
    BoolStreamOption.cs
    Int32StreamOption.cs
    IStreamOption.cs
    IStreamOptions.cs
    StreamOption.cs
    StreamOptions.cs
    StreamUtilities.cs
    )

# language definition
set_property(TARGET Cryptonite.Yaal.Common PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yaal.Common PROPERTY VS_DOTNET_REFERENCES
    "Microsoft.CSharp"
    "System"
    "System.Core"
    "WindowsBase"
    )
