using System;
using System.IO;
using System.Text;
using Cryptonite.Yaal.BZip2;
using Cryptonite.Yaal.Tests.Common;
using Xunit;

namespace Cryptonite.Yaal.Tests.BZip2
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
            var compressStream1 = new BZip2CompressStream(memoryStream1);
            compressStream1.Write(originalData, 0, originalData.Length);
            compressStream1.Close();

            // compress data using a multiple write operations
            var memoryStream2 = new MemoryStream();
            var compressStream2 = new BZip2CompressStream(memoryStream2);
            int offset = 0;
            int chunk = 500;
            while (offset < originalData.Length)
            {
                if (offset + chunk > originalData.Length)
                {
                    chunk = originalData.Length - offset;
                }
                compressStream2.Write(originalData, offset, chunk);
                offset += chunk;
            }
            compressStream2.Close();

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
            var compressStream = new BZip2CompressStream(memoryStream);
            compressStream.Write(originalData, 0, originalData.Length);
            compressStream.Close();

            // decompress data into a large buffer
            memoryStream.Position = 0;  // reset position
            var decompressStream1 = new BZip2DecompressStream(memoryStream);
            var decompressData1 = new byte[originalData.Length + 10000];
            var decompressLength1 = decompressStream1.Read(decompressData1, 0, decompressData1.Length);
            decompressStream1.Close();
            Assert.Equal(originalData.Length, decompressLength1);
            Array.Resize(ref decompressData1, decompressLength1);
            Assert.Equal(originalData, decompressData1);

            // decompress data into a buffer of the same size as the original
            memoryStream.Position = 0;  // reset position
            var decompressStream2 = new BZip2DecompressStream(memoryStream);
            var decompressData2 = new byte[originalData.Length];
            var decompressLength2 = decompressStream2.Read(decompressData2, 0, decompressData2.Length);
            decompressStream2.Close();
            Assert.Equal(originalData.Length, decompressLength2);
            Array.Resize(ref decompressData2, decompressLength2);
            Assert.Equal(originalData, decompressData2);

            // decompress data using multiple read operations
            memoryStream.Position = 0;  // reset position
            var decompressStream3 = new BZip2DecompressStream(memoryStream);
            var decompressData3 = new byte[originalData.Length];
            var decompressLength3 = 0;
            int chunk = 500;
            while (decompressLength3 < originalData.Length)
            {
                if (decompressLength3 + chunk > originalData.Length)
                {
                    chunk = originalData.Length - decompressLength3;
                }
                var length = decompressStream3.Read(decompressData3, decompressLength3, chunk);
                Assert.Equal(chunk, length);
                decompressLength3 += length;
            }
            decompressStream3.Close();
            Assert.Equal(originalData.Length, decompressLength3);
            Array.Resize(ref decompressData3, decompressLength3);
            Assert.Equal(originalData, decompressData3);
        }
    }
}
