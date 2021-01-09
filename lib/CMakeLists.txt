cmake_minimum_required(VERSION 3.12)

# project definition
project(Cryptonite.Yacl.Libs VERSION 0.0.0 LANGUAGES C CXX)

# only support Debug and Release configuration types
set(CMAKE_CONFIGURATION_TYPES "Debug;Release") 

# project output directories
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG ${CMAKE_BINARY_DIR}/bin/Debug)
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE ${CMAKE_BINARY_DIR}/bin/Release)

# Library components
add_subdirectory(bzip2)
add_subdirectory(xz-utils)
