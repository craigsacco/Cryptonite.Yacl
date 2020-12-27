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
                ref class InternalHandle
                {
                public:
                    InternalHandle(FILE* fileHandle, BZFILE* bzipHandle, bool write);

                    property System::IntPtr^ FileHandle
                    {
                    public:
                        System::IntPtr^ get();
                    }

                    property System::IntPtr^ BZipHandle
                    {
                    public:
                        System::IntPtr^ get();
                    }

                    property System::Boolean Writing
                    {
                    public:
                        System::Boolean get();
                    }

                    void Invalidate();
                    bool IsValid();
                    bool IsValid(bool expectWrite);

                private:
                    System::IntPtr^ m_fileHandle;
                    System::IntPtr^ m_bzipHandle;
                    bool m_writing;
                };

                static InternalHandle^ WriteOpen(System::String^ outputFile);
                static void Write(InternalHandle^ handle, array<System::Byte>^ inputData);
                static void WriteFlush(InternalHandle^ handle);
                static void WriteClose(InternalHandle^ handle);
                static InternalHandle^ ReadOpen(System::String^ inputFile);
                static array<System::Byte>^ Read(InternalHandle^ handle, System::Int32 maxBytes);
                static void ReadClose(InternalHandle^ handle);
            };
        }
    }
}
