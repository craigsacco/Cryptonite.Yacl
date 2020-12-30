#pragma once

#include <cstdint>

#using <mscorlib.dll>
#using <System.dll>

namespace Cryptonite
{
    namespace Yaal
    {
        namespace BZip2
        {
            public ref class NativeInterfaceException : public System::Exception
            {
            public:
                enum class ErrorType : int32_t
                {
                    SequenceError = -1,
                    ParameterError = -2,
                    MemoryError = -3,
                    DataError = -4,
                    DataErrorMagic = -5,
                    IOError = -6,
                    UnexpectedEOF = -7,
                    OutputBufferFull = -8,
                    ConfigError = -9,

                    FOpenError = -100,
                    FCloseError = -101,
                    StreamHandleError = -102,
                };

                static NativeInterfaceException^ CreateException(ErrorType type);

            private:
                NativeInterfaceException(System::String^ message);
            };
        }
    }
}
