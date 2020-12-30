#pragma once

#include <lzma.h>

#using <mscorlib.dll>
#using <System.dll>

namespace Cryptonite
{
    namespace Yacl
    {
        namespace XZ
        {
            public ref class NativeInterface
            {
            public:
                ref class StreamHandle
                {
                public:
                    property System::IntPtr^ LZMAStreamPointer;
                    property System::IO::Stream^ Stream;
                    property array<System::Byte>^ WorkingBuffer;
                    property System::Boolean IsWriting;
                    property System::Boolean IsEnded;
                    property System::Int64 CompressedLength;
                    property System::Int64 UncompressedLength;
                };

                static StreamHandle^ BeginCompress(System::IO::Stream^ writeStream, System::UInt32 presetLevel, System::Boolean extremeFlag);
                static void Compress(StreamHandle^ streamHandle, array<System::Byte>^ inputData);
                static void EndCompress(StreamHandle^ streamHandle);

                static StreamHandle^ BeginDecompress(System::IO::Stream^ readStream);
                static array<System::Byte>^ Decompress(StreamHandle^ streamHandle, System::Int32 maxSize);
                static void EndDecompress(StreamHandle^ streamHandle);
            };
        }
    }
}
