#!env bash

set -e

mkdir -p build
pushd build
cmake ../src
popd
