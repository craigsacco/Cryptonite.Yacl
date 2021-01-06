# .NET SDK project generation is not currently supported under CMake
# see: https://gitlab.kitware.com/cmake/cmake/-/issues/20227

# need to include a C# project file explicitly
include_external_msproject(Cryptonite.Yacl.BZip2 ${CMAKE_CURRENT_LIST_DIR}/Cryptonite.Yacl.BZip2.csproj PLATFORM AnyCPU
	Cryptonite.Yacl.Common
	Cryptonite.Yacl.BZip2.Managed
	)
set_target_properties(Cryptonite.Yacl.BZip2 PROPERTIES
	MAP_IMPORTED_CONFIG_DEBUG Debug
	MAP_IMPORTED_CONFIG_RELEASE Release
	)
