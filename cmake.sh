#!/bin/bash

set -e

if [[ "$OS" == "Windows_NT" ]]; then
    cmake -S src -B build
else
    # C++/CLI not supported on non-Windows platforms - disable BZip2 and XZ
    cmake -DYACL_BZIP2=OFF -DYACL_XZ=OFF -G Visual\ Studio\ 16\ 2019 -A x64 -S src -B build
fi
popd
