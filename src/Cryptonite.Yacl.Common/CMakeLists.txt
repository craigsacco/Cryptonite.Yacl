# .NET SDK project generation is not currently supported under CMake
# see: https://gitlab.kitware.com/cmake/cmake/-/issues/20227

# need to include a C# project file explicitly
include_external_msproject(Cryptonite.Yacl.Common ${CMAKE_CURRENT_LIST_DIR}/Cryptonite.Yacl.Common.csproj PLATFORM AnyCPU)
set_target_properties(Cryptonite.Yacl.Common PROPERTIES
	MAP_IMPORTED_CONFIG_DEBUG Debug
	MAP_IMPORTED_CONFIG_RELEASE Release
	)
