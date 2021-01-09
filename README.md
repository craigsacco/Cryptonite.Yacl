# Cryptonite.Yacl

## Release Status

Branch  | Build Status
------- | ------------
main    | [![Build Status](https://travis-ci.com/craigsacco/Cryptonite.Yacl.svg?branch=main)](https://travis-ci.com/craigsacco/Cryptonite.Yacl)

## Introduction

**This is an experimental project to teach myself C#, C++/CLI and P/Invoke.**

Cryptonite.Yacl is a compression library integrating well-known compression formats, using the
standard System.IO.Stream interface for compressing and decompressing data.

Where needed, compression and decompression is offloaded to well-known native libraries. 

It provides support for the following formats:

* GZip
* BZip2 (Windows only)
* XZ (Windows only)

## Dependencies

* BZip2 1.0.8
* XZ-Utils 5.2.5
* NDesk.Options 1.2.5
* Xunit 2.4.1

## To Be Done

* Use P/Invoke instead of C++/CLI to support non-Windows platforms
* XZ support is currently broken
* Build on Windows platform via Travis CI
* ZSTD support

## License

This software is released under the MIT License.
