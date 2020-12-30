add_library(Cryptonite.Yacl.Common SHARED
    BaseStream.cs
    BoolSetting.cs
    EnumSetting.cs
    Int32Setting.cs
    ISetting.cs
    ISettings.cs
    Setting.cs
    Settings.cs
    StreamUtilities.cs
    UInt32Setting.cs
    )

# language definition
set_property(TARGET Cryptonite.Yacl.Common PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.Common PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
