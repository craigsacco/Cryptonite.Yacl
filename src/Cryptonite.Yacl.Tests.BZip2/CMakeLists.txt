add_library(Cryptonite.Yacl.Tests.BZip2 SHARED
    StreamTests.cs
    #Properties/AssemblyInfo.cs
    #Properties/Settings.Designer.cs
    #Properties/Settings.settings
    )

# project dependencies
target_link_libraries(Cryptonite.Yacl.Tests.BZip2
    Cryptonite.Yacl.BZip2
    Cryptonite.Yacl.Tests.Common
    )

# language definition
set_property(TARGET Cryptonite.Yacl.Tests.BZip2 PROPERTY LINKER_LANGUAGE CSharp)

# .NET references
set_property(TARGET Cryptonite.Yacl.Tests.BZip2 PROPERTY VS_DOTNET_REFERENCES
    "System"
    "System.Core"
    )
set_property(TARGET Cryptonite.Yacl.Tests.BZip2 PROPERTY VS_PACKAGE_REFERENCES
    "xunit_2.4.1"
    "xunit.abstractions_2.0.3"
    "xunit.assert_2.4.1"
    "xunit.core_2.4.1"
    "xunit.extensibility.core_2.4.1"
    "xunit.extensibility.execution_2.4.1"
    "xunit.runner.console_2.4.1"
    "xunit.runner.visualstudio_2.4.3"
    )