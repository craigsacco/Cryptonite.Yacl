#!/bin/bash

set -e

mkdir -p build
pushd build
if [[ "$OS" == "Windows_NT" ]]; then
    cmake ../src
else
    # C++/CLI not supported on non-Windows platforms - disable BZip2 and XZ
    cmake -DYACL_BZIP2=OFF -DYACL_XZ=OFF ../src
fi
popd
