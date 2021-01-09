using Cryptonite.Yacl.XZ;
using Cryptonite.Yacl.Tests.Common;
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
