# MIT License
#
# Copyright (c) 2020-2021 Craig Sacco
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
# 
# The above copyright notice and this permission notice shall be included in all
# copies or substantial portions of the Software.
# 
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
# SOFTWARE.

# .NET SDK project generation is not currently supported under CMake
# see: https://gitlab.kitware.com/cmake/cmake/-/issues/20227

# need to include a C# project file explicitly
include_external_msproject(Cryptonite.Yacl.Cmd ${CMAKE_CURRENT_LIST_DIR}/Cryptonite.Yacl.Cmd.csproj PLATFORM AnyCPU
    Cryptonite.Yacl.Common
    Cryptonite.Yacl.BZip2
    Cryptonite.Yacl.GZip
    Cryptonite.Yacl.XZ
    )
set_target_properties(Cryptonite.Yacl.Cmd PROPERTIES
    MAP_IMPORTED_CONFIG_DEBUG Debug
    MAP_IMPORTED_CONFIG_RELEASE Release
    )
