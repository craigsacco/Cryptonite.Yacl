#include "NativeInterface.h"
#include "NativeInterfaceException.h"

#include <string>
#include <cstdint>
#include <msclr/marshal.h>
#include <vcclr.h>

namespace Cryptonite
{
    namespace Yaal
    {
        namespace BZip2
        {
            NativeInterface::InternalHandle::InternalHandle(FILE* fileHandle, BZFILE* bzipHandle, bool writing)
                : m_fileHandle(gcnew System::IntPtr(fileHandle))
                , m_bzipHandle(gcnew System::IntPtr(bzipHandle))
                , m_writing(writing)
            {
            }

            System::IntPtr^ NativeInterface::InternalHandle::FileHandle::get()
            {
                return m_fileHandle;
            }

            System::IntPtr^ NativeInterface::InternalHandle::BZipHandle::get()
            {
                return m_bzipHandle;
            }

            System::Boolean NativeInterface::InternalHandle::Writing::get()
            {
                return m_writing;
            }

            void NativeInterface::InternalHandle::Invalidate()
            {
                m_fileHandle = System::IntPtr::Zero;
                m_bzipHandle = System::IntPtr::Zero;
            }

            bool NativeInterface::InternalHandle::IsValid()
            {
                return (m_fileHandle->ToPointer() != nullptr) && (m_bzipHandle->ToPointer() != nullptr);
            }

            bool NativeInterface::InternalHandle::IsValid(bool expectWriting)
            {
                return IsValid() && (m_writing == expectWriting);
            }

            NativeInterface::InternalHandle^ NativeInterface::WriteOpen(System::String^ outputFile)
            {
                // open target file
                pin_ptr<const wchar_t> outputFileWide = PtrToStringChars(outputFile);
                FILE* fileHandle = _wfopen(outputFileWide, L"wb");
                if (fileHandle == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::FOpenError);
                }

                // open BZip handle
                int32_t bzipResult;
                BZFILE* bzipHandle = BZ2_bzWriteOpen(&bzipResult, fileHandle, 1, 0, 0);
                if (bzipResult != BZ_OK)
                {
                    fclose(fileHandle);
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(bzipResult));
                }

                // create and return handle
                return gcnew InternalHandle(fileHandle, bzipHandle, true);
            }

            void NativeInterface::Write(InternalHandle^ handle, array<System::Byte>^ inputData)
            {
                // check if handle is valid for this function
                if (!handle->IsValid(true))
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::InternalHandleError);
                }

                // write buffer into compressed output
                int32_t bzipResult;
                pin_ptr<System::Byte> pInputData = &inputData[0];
                BZ2_bzWrite(&bzipResult, handle->BZipHandle->ToPointer(), pInputData, inputData->Length);
                if (bzipResult != BZ_OK)
                {
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(bzipResult));
                }
            }

            void NativeInterface::WriteFlush(InternalHandle^ handle)
            {
                // check if handle is valid for this function
                if (!handle->IsValid(true))
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::InternalHandleError);
                }

                // flush the file handle
                fflush(static_cast<FILE*>(handle->FileHandle->ToPointer()));
            }

            void NativeInterface::WriteClose(InternalHandle^ handle)
            {
                // check if handle is valid for this function
                if (!handle->IsValid(true))
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::InternalHandleError);
                }

                // close BZip handle
                int32_t bzipResult;
                uint32_t bytesInLow32, bytesInHigh32, bytesOutLow32, bytesOutHigh32;
                BZ2_bzWriteClose64(&bzipResult, handle->BZipHandle->ToPointer(), 0, &bytesInLow32, &bytesInHigh32, &bytesOutLow32, &bytesOutHigh32);
                if (bzipResult != BZ_OK)
                {
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(bzipResult));
                }

                // close file handle
                fclose(static_cast<FILE*>(handle->FileHandle->ToPointer()));

                // invalidate the handle
                handle->Invalidate();

                // calculate bytes in/out
                uint64_t bytesIn = (static_cast<uint64_t>(bytesInHigh32) << 32) + bytesInLow32;
                uint64_t bytesOut = (static_cast<uint64_t>(bytesOutHigh32) << 32) + bytesOutLow32;
            }

            NativeInterface::InternalHandle^ NativeInterface::ReadOpen(System::String^ inputFile)
            {
                // open source file
                pin_ptr<const wchar_t> inputFileWide = PtrToStringChars(inputFile);
                FILE* fileHandle = _wfopen(inputFileWide, L"rb");
                if (fileHandle == nullptr)
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::FOpenError);
                }

                // open BZip handle
                int32_t bzipResult;
                BZFILE* bzipHandle = BZ2_bzReadOpen(&bzipResult, fileHandle, 0, 0, nullptr, 0);
                if (bzipResult != BZ_OK)
                {
                    fclose(fileHandle);
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(bzipResult));
                }

                // create and return handle
                return gcnew InternalHandle(fileHandle, bzipHandle, false);
            }

            array<System::Byte>^ NativeInterface::Read(InternalHandle^ handle, System::Int32 maxBytes)
            {
                // check if handle is valid for this function
                if (!handle->IsValid(false))
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::InternalHandleError);
                }

                // read data from compressed input
                int32_t bzipResult = 0;
                array<System::Byte>^ outputData = gcnew array<System::Byte>(maxBytes);
                pin_ptr<System::Byte> pOutputData = &outputData[0];
                int32_t bytesRead = BZ2_bzRead(&bzipResult, handle->BZipHandle->ToPointer(), pOutputData, outputData->Length);
                if (bzipResult != BZ_OK && bzipResult != BZ_STREAM_END)
                {
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(bzipResult));
                }

                // if needed, truncate the array; then return it
                if (bytesRead != maxBytes)
                {
                    System::Array::Resize(outputData, bytesRead);
                }
                return outputData;
            }

            void NativeInterface::ReadClose(InternalHandle^ handle)
            {
                // check if handle is valid for this function
                if (!handle->IsValid(false))
                {
                    throw NativeInterfaceException::CreateException(NativeInterfaceException::ErrorType::InternalHandleError);
                }

                // close BZip handle
                int32_t bzipResult;
                BZ2_bzReadClose(&bzipResult, handle->BZipHandle->ToPointer());
                if (bzipResult != BZ_OK)
                {
                    throw NativeInterfaceException::CreateException(static_cast<NativeInterfaceException::ErrorType>(bzipResult));
                }

                // close file handle
                fclose(static_cast<FILE*>(handle->FileHandle->ToPointer()));

                // invalidate the handle
                handle->Invalidate();
            }
        }
    }
}
