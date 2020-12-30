add_library(Cryptonite.Yacl.XZ SHARED
    LZMABaseStream.cs
    LZMACompressionPresetSetting.cs
    LZMACompressSettings.cs
    LZMACompressStream.cs
    LZMAExtremeCompressionSetting.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.XZ
    Cryptonite.Yacl.Common
    Cryptonite.Yacl.XZ.Managed
    )

# language definition
set_property(TARGET Cryptonite.Yacl.XZ PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.XZ PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
