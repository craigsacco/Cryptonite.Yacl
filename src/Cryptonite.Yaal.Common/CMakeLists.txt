add_library(Cryptonite.Yaal.Common SHARED
    BoolStreamSetting.cs
    Int32StreamSetting.cs
    IStreamSetting.cs
    IStreamSettings.cs
    StreamSetting.cs
    StreamSettings.cs
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
