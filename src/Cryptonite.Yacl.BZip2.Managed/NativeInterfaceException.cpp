/*
 * MIT License
 *
 * Copyright (c) 2020-2021 Craig Sacco
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#include "NativeInterfaceException.h"

namespace Cryptonite
{
    namespace Yacl
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
