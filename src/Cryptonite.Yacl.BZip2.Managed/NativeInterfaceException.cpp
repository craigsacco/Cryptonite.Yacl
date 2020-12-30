#include "NativeInterfaceException.h"

namespace Cryptonite
{
    namespace Yaal
    {
        namespace BZip2
        {
            NativeInterfaceException::NativeInterfaceException(System::String^ message)
                : System::Exception(message)
            {
            }

            NativeInterfaceException^ NativeInterfaceException::CreateException(ErrorType type)
            {
                switch (type)
                {
                case ErrorType::SequenceError:
                    return gcnew NativeInterfaceException("BZip sequence error");
                case ErrorType::ParameterError:
                    return gcnew NativeInterfaceException("BZip parameter error");
                case ErrorType::MemoryError:
                    return gcnew NativeInterfaceException("BZip memory error");
                case ErrorType::DataError:
                    return gcnew NativeInterfaceException("BZip data error");
                case ErrorType::DataErrorMagic:
                    return gcnew NativeInterfaceException("BZip magic data error");
                case ErrorType::IOError:
                    return gcnew NativeInterfaceException("BZip I/O error");
                case ErrorType::UnexpectedEOF:
                    return gcnew NativeInterfaceException("BZip unexpected EOF error");
                case ErrorType::OutputBufferFull:
                    return gcnew NativeInterfaceException("BZip output buffer full");
                case ErrorType::ConfigError:
                    return gcnew NativeInterfaceException("BZip config error");
                case ErrorType::FOpenError:
                    return gcnew NativeInterfaceException("Native interface fopen() error");
                case ErrorType::FCloseError:
                    return gcnew NativeInterfaceException("Native interface fclose() error");
                case ErrorType::StreamHandleError:
                    return gcnew NativeInterfaceException("Native interface stream handle");
                default:
                    return gcnew NativeInterfaceException("Unknown error");
                }
            }
        }
    }
}
