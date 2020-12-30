#include "NativeInterfaceException.h"

namespace Cryptonite
{
    namespace Yacl
    {
        namespace XZ
        {
            NativeInterfaceException::NativeInterfaceException(System::String^ message)
                : System::Exception(message)
            {
            }

            NativeInterfaceException^ NativeInterfaceException::CreateException(ErrorType type)
            {
                switch (type)
                {
                case ErrorType::UnsupportedCheckError:
                    return gcnew NativeInterfaceException("LZMA unsupported check error");
                case ErrorType::MemoryError:
                    return gcnew NativeInterfaceException("LZMA memory error");
                case ErrorType::MemoryLimitError:
                    return gcnew NativeInterfaceException("LZMA memory limit error");
                case ErrorType::FormatError:
                    return gcnew NativeInterfaceException("LZMA format error");
                case ErrorType::OptionsError:
                    return gcnew NativeInterfaceException("LZMA options error");
                case ErrorType::DataError:
                    return gcnew NativeInterfaceException("LZMA data error");
                case ErrorType::BufferError:
                    return gcnew NativeInterfaceException("LZMA buffer error");
                case ErrorType::ProgrammingError:
                    return gcnew NativeInterfaceException("LZMA programming error");
                case ErrorType::StreamHandleError:
                    return gcnew NativeInterfaceException("Native interface stream handle");
                default:
                    return gcnew NativeInterfaceException("Unknown error");
                }
            }
        }
    }
}
