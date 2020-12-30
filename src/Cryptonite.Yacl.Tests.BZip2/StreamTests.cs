using System;
using System.IO;
using System.Text;
using Cryptonite.Yacl.BZip2;
using Cryptonite.Yacl.Tests.Common;
using Xunit;

namespace Cryptonite.Yacl.Tests.BZip2
{
    public class StreamTests
    {
        [Fact]
        public void CompressionStreamTests()
        {
            // serialise test data
            byte[] originalData = Encoding.ASCII.GetBytes(TestResources.LoremIpsum);

            // compress data using a single write operation
            var memoryStream1 = new MemoryStream();
            using (var compressStream = new BZip2CompressStream(memoryStream1))
            {
                compressStream.Write(originalData, 0, originalData.Length);
            }

            // compress data using a multiple write operations
            var memoryStream2 = new MemoryStream();
            using (var compressStream = new BZip2CompressStream(memoryStream2))
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

            // compare the two compressed streams
            Assert.True(memoryStream1.Position == memoryStream2.Position);
            Assert.Equal(memoryStream1.ToArray(), memoryStream2.ToArray());
        }

        [Fact]
        public void DecompressionStreamTests()
        {
            // serialise test data
            byte[] originalData = Encoding.ASCII.GetBytes(TestResources.LoremIpsum);

            // compress data using a single write operation
            var memoryStream = new MemoryStream();
            using (var compressStream = new BZip2CompressStream(memoryStream))
            {
                compressStream.Write(originalData, 0, originalData.Length);
            }

            // decompress data into a large buffer
            memoryStream.Position = 0;  // reset position
            using (var decompressStream = new BZip2DecompressStream(memoryStream))
            {
                var decompressData = new byte[originalData.Length + 10000];
                var decompressLength = decompressStream.Read(decompressData, 0, decompressData.Length);
                Assert.Equal(originalData.Length, decompressLength);
                Array.Resize(ref decompressData, decompressLength);
                Assert.Equal(originalData, decompressData);
            }

            // decompress data into a buffer of the same size as the original
            memoryStream.Position = 0;  // reset position
            using (var decompressStream = new BZip2DecompressStream(memoryStream))
            {
                var decompressData = new byte[originalData.Length];
                var decompressLength = decompressStream.Read(decompressData, 0, decompressData.Length);
                Assert.Equal(originalData.Length, decompressLength);
                Array.Resize(ref decompressData, decompressLength);
                Assert.Equal(originalData, decompressData);
            }

            // decompress data using multiple read operations
            memoryStream.Position = 0;  // reset position
            using (var decompressStream = new BZip2DecompressStream(memoryStream))
            {
                var decompressData = new byte[originalData.Length];
                var decompressLength = 0;
                int chunk = 500;
                while (decompressLength < originalData.Length)
                {
                    if (decompressLength + chunk > originalData.Length)
                    {
                        chunk = originalData.Length - decompressLength;
                    }
                    var length = decompressStream.Read(decompressData, decompressLength, chunk);
                    Assert.Equal(chunk, length);
                    decompressLength += length;
                }
                Assert.Equal(originalData.Length, decompressLength);
                Array.Resize(ref decompressData, decompressLength);
                Assert.Equal(originalData, decompressData);
            }
        }
    }
}
