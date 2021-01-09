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

#pragma once

#include <cstdint>

#using <mscorlib.dll>
#using <System.dll>

namespace Cryptonite
{
    namespace Yacl
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
