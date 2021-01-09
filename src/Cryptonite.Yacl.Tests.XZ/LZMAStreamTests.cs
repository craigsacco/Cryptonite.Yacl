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

using Cryptonite.Yacl.Tests.Common;
using Cryptonite.Yacl.XZ;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Cryptonite.Yacl.Tests.XZ
{
    public class LZMAStreamTests : BaseStreamTests<LZMAStream, LZMACompressSettings, LZMADecompressSettings>
    {
        private static readonly Func<Stream, LZMAStream> DefaultCompressStreamCreateFunc =
            innerStream => new LZMAStream(innerStream, new LZMACompressSettings());
        private static readonly Func<Stream, LZMAStream> DefaultDecompressStreamCreateFunc =
            innerStream => new LZMAStream(innerStream, new LZMADecompressSettings());

        public LZMAStreamTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(10, false)]
        [InlineData(30, false)]
        [InlineData(100, false)]
        [InlineData(300, false)]
        [InlineData(1000, false)]
        [InlineData(3000, false)]
        [InlineData(10000, false)]
        [InlineData(30000, false)]
        [InlineData(100000, false)]
        [InlineData(300000, false)]
        [InlineData(1000000, false)]
        [InlineData(0, true)]
        [InlineData(10, true)]
        [InlineData(30, true)]
        [InlineData(100, true)]
        [InlineData(300, true)]
        [InlineData(1000, true)]
        [InlineData(3000, true)]
        [InlineData(10000, true)]
        [InlineData(30000, true)]
        [InlineData(100000, true)]
        [InlineData(300000, true)]
        [InlineData(1000000, true)]
        public void CompleteBufferTest(int size, bool text)
        {
            InternalCompleteBufferTest(size, text,
                DefaultCompressStreamCreateFunc, DefaultDecompressStreamCreateFunc);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(10, false)]
        [InlineData(30, false)]
        [InlineData(100, false)]
        [InlineData(300, false)]
        [InlineData(1000, false)]
        [InlineData(3000, false)]
        [InlineData(10000, false)]
        [InlineData(30000, false)]
        [InlineData(100000, false)]
        [InlineData(300000, false)]
        [InlineData(1000000, false)]
        [InlineData(0, true)]
        [InlineData(10, true)]
        [InlineData(30, true)]
        [InlineData(100, true)]
        [InlineData(300, true)]
        [InlineData(1000, true)]
        [InlineData(3000, true)]
        [InlineData(10000, true)]
        [InlineData(30000, true)]
        [InlineData(100000, true)]
        [InlineData(300000, true)]
        [InlineData(1000000, true)]
        public void ChunkedBufferTest(int size, bool text)
        {
            InternalChunkedBufferTest(size, text, DefaultCompressStreamCreateFunc,
                DefaultDecompressStreamCreateFunc);
        }
    }
}
