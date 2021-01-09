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
        namespace XZ
        {
            NativeInterface::StreamHandle^ NativeInterface::BeginCompress(System::IO::Stream^ writeStream, System::UInt32 presetLevel, System::Boolean extremeFlag)
            {
                // create stream
                lzma_stream* pStream = reinterpret_cast<lzma_stream*>(malloc(sizeof(lzma_stream)));
                if (pStream == nullptr)
                {
                    throw gcnew System::OutOfMemoryException();
                }
                *pStream = LZMA_STREAM_INIT;

                // initiate the encoder
                uint32_t presetValue = presetLevel | (extremeFlag ? LZMA_PRESET_EXTREME : 0);
                int ret = lzma_easy_encoder(pStream, presetValue, LZMA_CHECK_CRC64);
                if (ret != LZMA_OK)
                {
                    free(pStream);
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                };

                // return handle
                StreamHandle^ streamHandle = gcnew StreamHandle();
                streamHandle->LZMAStreamPointer = gcnew System::IntPtr(pStream);
                streamHandle->Stream = writeStream;
                streamHandle->WorkingBuffer = gcnew array<System::Byte>(BUFSIZ);
                streamHandle->IsWriting = true;
                streamHandle->IsEnded = false;
                streamHandle->CompressedLength = 0;
                streamHandle->UncompressedLength = 0;
                return streamHandle;
            }

            void NativeInterface::Compress(StreamHandle^ streamHandle, array<System::Byte>^ inputData)
            {
                if (streamHandle->IsWriting == false || streamHandle->LZMAStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }
                
                // recast stream pointer
                lzma_stream* pStream = reinterpret_cast<lzma_stream*>(streamHandle->LZMAStreamPointer->ToPointer());

                // bail early if input data is empty
                if (inputData->Length == 0)
                {
                    return;
                }

                // initiate compression operation
                pin_ptr<System::Byte> pInputData = &inputData[0];
                pStream->avail_in = inputData->Length;
                pStream->next_in = pInputData;
                pin_ptr<System::Byte> pCompressedData = &streamHandle->WorkingBuffer[0];
                while (true)
                {
                    // compress next chunk
                    pStream->avail_out = BUFSIZ;
                    pStream->next_out = reinterpret_cast<decltype(pStream->next_out)>(pCompressedData);
                    int ret = lzma_code(pStream, LZMA_RUN);
                    if (ret != LZMA_OK && ret != LZMA_STREAM_END)
                    {
                        throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                    }

                    // write data to output stream
                    if (pStream->avail_out != 0)
                    {
                        streamHandle->Stream->Write(streamHandle->WorkingBuffer, 0, BUFSIZ - pStream->avail_out);
                    }

                    // break out if no more data is available
                    if (pStream->avail_in == 0)
                    {
                        break;
                    }
                }

                // update lengths
                uint64_t progressIn, progressOut;
                lzma_get_progress(pStream, &progressIn, &progressOut);
                streamHandle->UncompressedLength = static_cast<int64_t>(progressIn);
                streamHandle->CompressedLength = static_cast<int64_t>(progressOut);
            }

            void NativeInterface::EndCompress(StreamHandle^ streamHandle)
            {
                if (streamHandle->IsWriting == false || streamHandle->LZMAStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }

                // recast stream pointer
                lzma_stream* pStream = reinterpret_cast<lzma_stream*>(streamHandle->LZMAStreamPointer->ToPointer());

                // compress remaining data in the working buffer
                pin_ptr<System::Byte> pCompressedData = &streamHandle->WorkingBuffer[0];
                while (true)
                {
                    // compress any outstanding data in the working buffer
                    pStream->avail_out = BUFSIZ;
                    pStream->next_out = reinterpret_cast<decltype(pStream->next_out)>(pCompressedData);
                    int ret = lzma_code(pStream, LZMA_FINISH);
                    if (ret != LZMA_OK && ret != LZMA_STREAM_END)
                    {
                        throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                    }

                    // write data to output stream
                    if (pStream->avail_out < BUFSIZ)
                    {
                        streamHandle->Stream->Write(streamHandle->WorkingBuffer, 0, BUFSIZ - pStream->avail_out);
                    }

                    // break out if no more data is available
                    if (pStream->avail_in == 0)
                    {
                        break;
                    }
                }
                
                // update lengths
                uint64_t progressIn, progressOut;
                lzma_get_progress(pStream, &progressIn, &progressOut);
                streamHandle->UncompressedLength = static_cast<int64_t>(progressIn);
                streamHandle->CompressedLength = static_cast<int64_t>(progressOut);

                // complete the compression process
                lzma_end(pStream);

                // free resources
                free(pStream);
                streamHandle->LZMAStreamPointer = System::IntPtr::Zero;
                streamHandle->IsEnded = true;
            }

            NativeInterface::StreamHandle^ NativeInterface::BeginDecompress(System::IO::Stream^ readStream)
            {
                // create stream
                lzma_stream* pStream = reinterpret_cast<lzma_stream*>(malloc(sizeof(lzma_stream)));
                if (pStream == nullptr)
                {
                    throw gcnew System::OutOfMemoryException();
                }
                *pStream = LZMA_STREAM_INIT;

                // initiate the encoder
                int ret = lzma_stream_decoder(pStream, UINT64_MAX, LZMA_CONCATENATED);
                if (ret != LZMA_OK)
                {
                    free(pStream);
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                };


                // return handle
                pStream->avail_in = 0;
                StreamHandle^ streamHandle = gcnew StreamHandle();
                streamHandle->LZMAStreamPointer = gcnew System::IntPtr(pStream);
                streamHandle->Stream = readStream;
                streamHandle->WorkingBuffer = gcnew array<System::Byte>(BUFSIZ);
                streamHandle->IsWriting = false;
                streamHandle->IsEnded = false;
                streamHandle->CompressedLength = 0;
                streamHandle->UncompressedLength = 0;
                return streamHandle;
            }

            array<System::Byte>^ NativeInterface::Decompress(StreamHandle^ streamHandle, System::Int32 maxSize)
            {
                return gcnew array<System::Byte>(maxSize);

                if (streamHandle->IsWriting == false || streamHandle->LZMAStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }

                // recast stream pointer
                lzma_stream* pStream = reinterpret_cast<lzma_stream*>(streamHandle->LZMAStreamPointer->ToPointer());

                // bail early if maximum size is zero
                if (maxSize == 0)
                {
                    return gcnew array<System::Byte>(0);
                }

                // initiate decompression operation
                array<System::Byte>^ outputData = gcnew array<System::Byte>(maxSize);
                pin_ptr<System::Byte> pOutputData = &outputData[0];
                pStream->avail_out = outputData->Length;
                pStream->next_out = pOutputData;
                pin_ptr<System::Byte> pCompressedData = &streamHandle->WorkingBuffer[0];
                while (true)
                {
                    // read in next chunk from stream
                    if (pStream->avail_in == 0)
                    {
                        pStream->avail_in = streamHandle->Stream->Read(streamHandle->WorkingBuffer, 0, streamHandle->WorkingBuffer->Length);
                        pStream->next_in = pCompressedData;
                    }

                    bool done = false;
                    if (pStream->avail_in != 0)
                    {
                        // decompress chunk
                        int ret = lzma_code(pStream, LZMA_RUN);
                        if (ret != LZMA_OK && ret != LZMA_STREAM_END)
                        {
                            throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(ret));
                        }

                        // if reached the end of stream then truncate return buffer
                        if (ret == LZMA_STREAM_END)
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
                        uint64_t progressIn, progressOut;
                        lzma_get_progress(pStream, &progressIn, &progressOut);
                        streamHandle->CompressedLength = static_cast<int64_t>(progressIn);
                        streamHandle->UncompressedLength = static_cast<int64_t>(progressOut);
                    }
                }
            }

            void NativeInterface::EndDecompress(StreamHandle^ streamHandle)
            {
                if (streamHandle->IsWriting == true || streamHandle->LZMAStreamPointer->ToPointer() == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::StreamHandleError);
                }
                
                // recast stream pointer
                lzma_stream* pStream = reinterpret_cast<lzma_stream*>(streamHandle->LZMAStreamPointer->ToPointer());

                // complete the decompression process and free resources
                lzma_end(pStream);

                // free resources
                free(pStream);
                streamHandle->LZMAStreamPointer = System::IntPtr::Zero;
                streamHandle->IsEnded = true;
            }
        }
    }
}
