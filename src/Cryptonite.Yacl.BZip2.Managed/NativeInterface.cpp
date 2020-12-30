#include "NativeInterface.h"
#include "NativeInterfaceException.h"

#include <string>
#include <cstdint>
#include <msclr/marshal.h>
#include <vcclr.h>

namespace Cryptonite
{
    namespace Yacl
    {
        namespace BZip2
        {
            NativeInterface::StreamHandle^ NativeInterface::BeginCompress(System::IO::Stream^ writeStream, System::Int32 blockSize100k, System::Int32 verbosity, System::Int32 workFactor)
            {
                // create stream
                bz_stream* pStream = reinterpret_cast<bz_stream*>(malloc(sizeof(bz_stream)));
                if (pStream == nullptr)
                {
                    throw gcnew System::OutOfMemoryException();
                }

                // initiate the compression stream
                pStream->bzalloc = nullptr;
                pStream->bzfree = nullptr;
                pStream->opaque = nullptr;
                int ret = BZ2_bzCompressInit(pStream, blockSize100k, verbosity, workFactor);
                if (ret != BZ_OK)
                {
                    free(pStream);
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                };

                // return handle
                pStream->avail_in = 0;
                StreamHandle^ streamHandle = gcnew StreamHandle();
                streamHandle->BZipStreamPointer = gcnew System::IntPtr(pStream);
                streamHandle->Stream = writeStream;
                streamHandle->WorkingBuffer = gcnew array<System::Byte>(BZ_MAX_UNUSED);
                streamHandle->IsWriting = true;
                streamHandle->IsEnded = false;
                streamHandle->CompressedLength = 0;
                streamHandle->UncompressedLength = 0;
                return streamHandle;
            }

            void NativeInterface::Compress(StreamHandle^ streamHandle, array<System::Byte>^ inputData)
            {
                if (streamHandle->IsWriting == false || streamHandle->BZipStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }

                // recast stream pointer
                bz_stream* pStream = reinterpret_cast<bz_stream*>(streamHandle->BZipStreamPointer->ToPointer());

                // bail early if input data is empty
                if (inputData->Length == 0)
                {
                    return;
                }

                // initiate compression operation
                pin_ptr<System::Byte> pInputData = &inputData[0];
                pStream->avail_in = inputData->Length;
                pStream->next_in = reinterpret_cast<char*>(pInputData);
                pin_ptr<System::Byte> pCompressedData = &streamHandle->WorkingBuffer[0];
                while (true)
                {
                    // compress next chunk
                    pStream->avail_out = BZ_MAX_UNUSED;
                    pStream->next_out = reinterpret_cast<decltype(pStream->next_out)>(pCompressedData);
                    int ret = BZ2_bzCompress(pStream, BZ_RUN);
                    if (ret != BZ_RUN_OK)
                    {
                        throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                    }

                    // write data to output stream
                    if (pStream->avail_out < BZ_MAX_UNUSED)
                    {
                        streamHandle->Stream->Write(streamHandle->WorkingBuffer, 0, BZ_MAX_UNUSED - pStream->avail_out);
                    }

                    // break out if no more data is available
                    if (pStream->avail_in == 0)
                    {
                        break;
                    }
                }

                // update lengths
                streamHandle->UncompressedLength = (static_cast<int64_t>(pStream->total_in_hi32) << 32) + pStream->total_in_lo32;
                streamHandle->CompressedLength = (static_cast<int64_t>(pStream->total_out_hi32) << 32) + pStream->total_out_lo32;
            }

            void NativeInterface::EndCompress(StreamHandle^ streamHandle, System::Boolean abandon)
            {
                if (streamHandle->IsWriting == false || streamHandle->BZipStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }

                // recast stream pointer
                bz_stream* pStream = reinterpret_cast<bz_stream*>(streamHandle->BZipStreamPointer->ToPointer());

                // compress remaining data in the working buffer
                if (abandon == false)
                {
                    pin_ptr<System::Byte> pCompressedData = &streamHandle->WorkingBuffer[0];
                    while (true)
                    {
                        // compress any outstanding data in the working buffer
                        pStream->avail_out = BZ_MAX_UNUSED;
                        pStream->next_out = reinterpret_cast<decltype(pStream->next_out)>(pCompressedData);
                        int ret = BZ2_bzCompress(pStream, BZ_FINISH);
                        if (ret != BZ_FINISH_OK && ret != BZ_STREAM_END)
                        {
                            throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                        }

                        // write data to stream
                        if (pStream->avail_out < BZ_MAX_UNUSED)
                        {
                            streamHandle->Stream->Write(streamHandle->WorkingBuffer, 0, BZ_MAX_UNUSED - pStream->avail_out);
                        }

                        // break out if the input stream has been depleted
                        if (ret == BZ_STREAM_END)
                        {
                            break;
                        }
                    }
                }

                // complete the compression process
                BZ2_bzCompressEnd(pStream);

                // update lengths
                streamHandle->UncompressedLength = (static_cast<int64_t>(pStream->total_in_hi32) << 32) + pStream->total_in_lo32;
                streamHandle->CompressedLength = (static_cast<int64_t>(pStream->total_out_hi32) << 32) + pStream->total_out_lo32;

                // free resources
                free(pStream);
                streamHandle->BZipStreamPointer = System::IntPtr::Zero;
                streamHandle->IsEnded = true;
            }

            NativeInterface::StreamHandle^ NativeInterface::BeginDecompress(System::IO::Stream^ readStream, System::UInt32 verbosity, System::Boolean small)
            {
                // create stream
                bz_stream* pStream = reinterpret_cast<bz_stream*>(malloc(sizeof(bz_stream)));
                if (pStream == nullptr)
                {
                    throw gcnew System::OutOfMemoryException();
                }

                // initiate the decompression stream
                pStream->bzalloc = nullptr;
                pStream->bzfree = nullptr;
                pStream->opaque = nullptr;
                int ret = BZ2_bzDecompressInit(pStream, verbosity, small ? 1 : 0);
                if (ret != BZ_OK)
                {
                    free(pStream);
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                };

                // return handle
                pStream->avail_in = 0;
                StreamHandle^ streamHandle = gcnew StreamHandle();
                streamHandle->BZipStreamPointer = gcnew System::IntPtr(pStream);
                streamHandle->Stream = readStream;
                streamHandle->WorkingBuffer = gcnew array<System::Byte>(BZ_MAX_UNUSED);
                streamHandle->IsWriting = false;
                streamHandle->IsEnded = false;
                streamHandle->CompressedLength = 0;
                streamHandle->UncompressedLength = 0;
                return streamHandle;
            }

            array<System::Byte>^ NativeInterface::Decompress(StreamHandle^ streamHandle, System::Int32 maxSize)
            {
                if (streamHandle->IsWriting == true || streamHandle->BZipStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }

                // recast stream pointer
                bz_stream* pStream = reinterpret_cast<bz_stream*>(streamHandle->BZipStreamPointer->ToPointer());

                // bail early if maximum size is zero
                if (maxSize == 0)
                {
                    return gcnew array<System::Byte>(0);
                }

                // initiate decompression operation
                array<System::Byte>^ outputData = gcnew array<System::Byte>(maxSize);
                pin_ptr<System::Byte> pOutputData = &outputData[0];
                pStream->avail_out = outputData->Length;
                pStream->next_out = reinterpret_cast<char*>(pOutputData);
                pin_ptr<System::Byte> pCompressedData = &streamHandle->WorkingBuffer[0];
                while (true)
                {
                    // read in next chunk from stream
                    if (pStream->avail_in == 0)
                    {
                        pStream->avail_in = streamHandle->Stream->Read(streamHandle->WorkingBuffer, 0, streamHandle->WorkingBuffer->Length);;
                        pStream->next_in = reinterpret_cast<char*>(pCompressedData);
                    }

                    bool done = false;
                    if (pStream->avail_in != 0)
                    {
                        // decompress chunk
                        int ret = BZ2_bzDecompress(pStream);
                        if (ret != BZ_OK && ret != BZ_STREAM_END)
                        {
                            throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                        }

                        // if reached the end of stream then truncate return buffer
                        if (ret == BZ_STREAM_END)
                        {
                            done = true;
                            System::Array::Resize(outputData, maxSize - pStream->avail_out);
                        }

                        // if there is no more data available in the return buffer then we're done
                        if (pStream->avail_out == 0)
                        {
                            done = true;
                        }
                    }
                    else
                    {
                        // nothing left in the input - truncate the output buffer
                        done = true;
                        System::Array::Resize(outputData, maxSize - pStream->avail_out);
                    }

                    // if we're done then update lengths and return array
                    if (done)
                    {
                        // update lengths
                        streamHandle->CompressedLength = (static_cast<int64_t>(pStream->total_in_hi32) << 32) + pStream->total_in_lo32;
                        streamHandle->UncompressedLength = (static_cast<int64_t>(pStream->total_out_hi32) << 32) + pStream->total_out_lo32;
                        return outputData;
                    }
                }
            }

            void NativeInterface::EndDecompress(StreamHandle^ streamHandle)
            {
                if (streamHandle->IsWriting == true || streamHandle->BZipStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }

                // recast stream pointer
                bz_stream* pStream = reinterpret_cast<bz_stream*>(streamHandle->BZipStreamPointer->ToPointer());

                // complete the decompression process and free resources
                BZ2_bzDecompressEnd(pStream);

                // update lengths
                streamHandle->CompressedLength = (static_cast<int64_t>(pStream->total_in_hi32) << 32) + pStream->total_in_lo32;
                streamHandle->UncompressedLength = (static_cast<int64_t>(pStream->total_out_hi32) << 32) + pStream->total_out_lo32;

                // free resources
                free(pStream);
                streamHandle->BZipStreamPointer = System::IntPtr::Zero;
                streamHandle->IsEnded = true;
            }
        }
    }
}
