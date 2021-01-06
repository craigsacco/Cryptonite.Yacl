# .NET SDK project generation is not currently supported under CMake
# see: https://gitlab.kitware.com/cmake/cmake/-/issues/20227

# need to include a C# project file explicitly
include_external_msproject(Cryptonite.Yacl.XZ ${CMAKE_CURRENT_LIST_DIR}/Cryptonite.Yacl.XZ.csproj PLATFORM AnyCPU
	Cryptonite.Yacl.Common
	Cryptonite.Yacl.XZ.Managed
	)
set_target_properties(Cryptonite.Yacl.XZ PROPERTIES
	MAP_IMPORTED_CONFIG_DEBUG Debug
	MAP_IMPORTED_CONFIG_RELEASE Release
	)
