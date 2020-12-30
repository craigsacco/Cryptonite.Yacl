add_library(Cryptonite.Yacl.GZip SHARED
    GZipBaseStream.cs
    GZipCompressionLevelSetting.cs
    GZipCompressSettings.cs
    GZipCompressStream.cs
    GZipDecompressSettings.cs
    GZipDecompressStream.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.GZip
    Cryptonite.Yacl.Common
    )

# language definition
set_property(TARGET Cryptonite.Yacl.GZip PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.GZip PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
