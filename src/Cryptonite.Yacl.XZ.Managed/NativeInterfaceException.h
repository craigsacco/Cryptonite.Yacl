#pragma once

#include <cstdint>

#using <mscorlib.dll>
#using <System.dll>

namespace Cryptonite
{
    namespace Yacl
    {
        namespace XZ
        {
            public ref class NativeInterfaceException : public System::Exception
            {
            public:
                enum class ErrorType : int32_t
                {
                    StreamEnded = 1,
                    NoCheck = 2,
                    UnsupportedCheckError = 3,
                    GetCheck = 4,
                    MemoryError = 5,
                    MemoryLimitError = 6,
                    FormatError = 7,
                    OptionsError = 8,
                    DataError = 9,
                    BufferError = 10,
                    ProgrammingError = 11,

                    StreamHandleError = 100,
                };

                static NativeInterfaceException^ CreateException(ErrorType type);

            private:
                NativeInterfaceException(System::String^ message);
            };
        }
    }
}
