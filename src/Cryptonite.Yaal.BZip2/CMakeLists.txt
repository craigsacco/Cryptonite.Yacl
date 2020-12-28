add_library(Cryptonite.Yaal.BZip2 SHARED
    BZip2BaseStream.cs
    BZip2CompressStream.cs
    BZip2DecompressStream.cs
    )

# project dependencies
target_link_libraries(Cryptonite.Yaal.BZip2
    Cryptonite.Yaal.Common
    Cryptonite.Yaal.BZip2.Managed
    )

# language definition
set_property(TARGET Cryptonite.Yaal.BZip2 PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yaal.BZip2 PROPERTY VS_DOTNET_REFERENCES
    "Microsoft.CSharp"
    "System"
    "System.Core"
    "WindowsBase"
    )
