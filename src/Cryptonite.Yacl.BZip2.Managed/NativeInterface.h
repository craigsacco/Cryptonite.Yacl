#pragma once

#include <bzlib.h>

#using <mscorlib.dll>
#using <System.dll>

namespace Cryptonite
{
    namespace Yaal
    {
        namespace BZip2
        {
            public ref class NativeInterface
            {
            public:
                ref class StreamHandle
                {
                public:
                    property System::IntPtr^ BZipStreamPointer;
                    property System::IO::Stream^ Stream;
                    property array<System::Byte>^ WorkingBuffer;
                    property System::Boolean IsWriting;
                    property System::Boolean IsEnded;
                    property System::Int64 CompressedLength;
                    property System::Int64 UncompressedLength;
                };

                static StreamHandle^ BeginCompress(System::IO::Stream^ writeStream, System::Int32 blockSize100k, System::Int32 verbosity, System::Int32 workFactor);
                static void Compress(StreamHandle^ streamHandle, array<System::Byte>^ inputData);
                static void EndCompress(StreamHandle^ streamHandle, System::Boolean abandon);

                static StreamHandle^ BeginDecompress(System::IO::Stream^ readStream, System::UInt32 verbosity, System::Boolean small);
                static array<System::Byte>^ Decompress(StreamHandle^ streamHandle, System::Int32 maxSize);
                static void EndDecompress(StreamHandle^ streamHandle);
            };
        }
    }
}
