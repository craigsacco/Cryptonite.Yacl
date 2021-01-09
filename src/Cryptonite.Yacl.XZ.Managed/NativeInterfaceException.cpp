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
