add_library(Cryptonite.Yaal.BZip2.Tests SHARED
    NativeInterfaceTests.cs
    StreamTests.cs
    #Properties/AssemblyInfo.cs
    #Properties/Settings.Designer.cs
    #Properties/Settings.settings
    )

# project dependencies
target_link_libraries(Cryptonite.Yaal.BZip2.Tests
    Cryptonite.Yaal.BZip2
    )

# language definition
set_property(TARGET Cryptonite.Yaal.BZip2.Tests PROPERTY LINKER_LANGUAGE CSharp)

# .NET target version and references
set_property(TARGET Cryptonite.Yaal.BZip2.Tests PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION ${DOTNET_VERSION})
set_property(TARGET Cryptonite.Yaal.BZip2.Tests PROPERTY VS_DOTNET_REFERENCES
    "Microsoft.CSharp"
    "System"
    "System.Core"
    "WindowsBase"
    )
set_property(TARGET Cryptonite.Yaal.BZip2.Tests PROPERTY VS_PACKAGE_REFERENCES
    "xunit_2.4.1"
    "xunit.abstractions_2.0.3"
    "xunit.assert_2.4.1"
    "xunit.core_2.4.1"
    "xunit.extensibility.core_2.4.1"
    "xunit.extensibility.execution_2.4.1"
    "xunit.runner.console_2.4.1"
    "xunit.runner.visualstudio_2.4.3"
    )
