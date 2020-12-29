add_library(Cryptonite.Yaal.GZip SHARED
    GZipBaseStream.cs
    GZipCompressionLevelStreamSetting.cs
    GZipCompressStream.cs
    GZipCompressStreamSettings.cs
    GZipDecompressStream.cs
    GZipDecompressStreamSettings.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yaal.GZip
    Cryptonite.Yaal.Common
    )

# language definition
set_property(TARGET Cryptonite.Yaal.GZip PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yaal.GZip PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
