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

using Cryptonite.Yacl.Common;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using StreamCreateFunc = System.Func<System.IO.Stream, Cryptonite.Yacl.Common.BaseStream>;

namespace Cryptonite.Yacl.Tests.Common
{
    public class BaseStreamTests<StreamType, CompressedSettingsType, DecompressedSettingsType>
        where StreamType : BaseStream
        where CompressedSettingsType : ISettings
        where DecompressedSettingsType : ISettings
    {
        protected readonly ITestOutputHelper m_output;

        public BaseStreamTests(ITestOutputHelper output)
        {
            m_output = output;
        }

        protected void InternalCompleteBufferTest(int size, bool text,
            StreamCreateFunc compressStreamCreateFunc,
            StreamCreateFunc decompressStreamCreateFunc)
        {
            // generate random data
            var originalData = text ? TestResources.GenerateTextData(size) :
                TestResources.GenerateRandomData(size);
            m_output.WriteLine($"sizeof(originalData) = {originalData.Length}");

            // compress data using a single write operation
            var compressedOutputData = new MemoryStream();
            using (var compressStream = compressStreamCreateFunc(compressedOutputData))
            {
                compressStream.Write(originalData, 0, originalData.Length);
            }
            m_output.WriteLine($"sizeof(compressedOutputData) = {compressedOutputData.GetBuffer().Length}");

            // decompress from the compressed output
            var compressedInputData = new MemoryStream(compressedOutputData.GetBuffer());
            var decompressedOutputData = new byte[originalData.Length];
            using (var decompressStream = decompressStreamCreateFunc(compressedInputData))
            {
                decompressStream.Read(decompressedOutputData, 0, decompressedOutputData.Length);
            }
            m_output.WriteLine($"sizeof(decompressedOutputData) = {decompressedOutputData.Length}");

            // compare the decompressed output with the original data
            Assert.True(decompressedOutputData.Length == originalData.Length);
            Assert.Equal(decompressedOutputData, originalData);
        }

        protected void InternalChunkedBufferTest(int size, bool text,
            StreamCreateFunc compressStreamCreateFunc,
            StreamCreateFunc decompressStreamCreateFunc)
        {
            // generate random data
            var originalData = text ? TestResources.GenerateTextData(size) :
                TestResources.GenerateRandomData(size);
            m_output.WriteLine($"sizeof(originalData) = {originalData.Length}");

            // compress data using a multiple write operations
            var compressedOutputData = new MemoryStream();
            using (var compressStream = compressStreamCreateFunc(compressedOutputData))
            {
                int offset = 0;
                int chunk = 500;
                while (offset < originalData.Length)
                {
                    if (offset + chunk > originalData.Length)
                    {
                        chunk = originalData.Length - offset;
                    }
                    compressStream.Write(originalData, offset, chunk);
                    offset += chunk;
                }
            }
            m_output.WriteLine($"sizeof(compressedOutputData) = {compressedOutputData.GetBuffer().Length}");

            // decompress data using a multiple read operations
            var compressedInputData = new MemoryStream(compressedOutputData.GetBuffer());
            var decompressedOutputData = new byte[originalData.Length];
            using (var decompressStream = decompressStreamCreateFunc(compressedInputData))
            {
                int offset = 0;
                int chunk = 500;
                while (offset < decompressedOutputData.Length)
                {
                    if (offset + chunk > decompressedOutputData.Length)
                    {
                        chunk = decompressedOutputData.Length - offset;
                    }
                    decompressStream.Read(decompressedOutputData, offset, chunk);
                    offset += chunk;
                }
            }
            m_output.WriteLine($"sizeof(decompressedOutputData) = {decompressedOutputData.Length}");

            // compare the decompressed output with the original data
            Assert.True(decompressedOutputData.Length == originalData.Length);
            Assert.Equal(decompressedOutputData, originalData);
        }
    }
}
