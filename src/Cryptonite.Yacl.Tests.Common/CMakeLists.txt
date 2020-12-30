add_library(Cryptonite.Yacl.Tests.Common SHARED
    TestResources.cs
    #Properties/AssemblyInfo.cs
    #Properties/Settings.Designer.cs
    #Properties/Settings.settings
    )

# language definition
set_property(TARGET Cryptonite.Yacl.Tests.Common PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.Tests.Common PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
set_property(TARGET Cryptonite.Yacl.Tests.Common PROPERTY VS_PACKAGE_REFERENCES
    "xunit_2.4.1"
    "xunit.abstractions_2.0.3"
    "xunit.assert_2.4.1"
    "xunit.core_2.4.1"
    "xunit.extensibility.core_2.4.1"
    "xunit.extensibility.execution_2.4.1"
    "xunit.runner.console_2.4.1"
    "xunit.runner.visualstudio_2.4.3"
    )
