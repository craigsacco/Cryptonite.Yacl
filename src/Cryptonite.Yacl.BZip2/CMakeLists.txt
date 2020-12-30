add_library(Cryptonite.Yacl.BZip2 SHARED
    BZip2BaseStream.cs
    BZip2BlockSizeSetting.cs
    BZip2CompressSettings.cs
    BZip2CompressStream.cs
    BZip2DecompressSettings.cs
    BZip2DecompressStream.cs
    BZip2SmallDecompressSetting.cs
    BZip2WorkFactorSetting.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.BZip2
    Cryptonite.Yacl.Common
    Cryptonite.Yacl.BZip2.Managed
    )

# language definition
set_property(TARGET Cryptonite.Yacl.BZip2 PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.BZip2 PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
