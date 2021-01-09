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
