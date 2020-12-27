using System;
using Cryptonite.Yaal.BZip2;
using Xunit;

namespace Cryptonite.Yaal.BZip2.Tests
{
    public class StreamTests
    {
        [Fact]
        public void CompleteWriteWorkflow()
        {
            // open output stream
            var outputStream = new BZip2FileStream("Test.txt.bz2", true);
            Assert.Equal(0, outputStream.Length);

            // write a byte array to compressed output
            byte[] inputData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            outputStream.Write(inputData, 0, inputData.Length);
            Assert.Equal(inputData.Length, outputStream.Length);

            // close output stream
            outputStream.Close();

            // open input file
            var inputStream = new BZip2FileStream("Test.txt.bz2", false);
            Assert.Equal(0, inputStream.Length);

            // read bytes from compressed output
            byte[] outputData = new byte[10];
            int outputLength = inputStream.Read(outputData, 0, outputData.Length);
            Assert.Equal(outputLength, outputData.Length);
            Assert.Equal(outputData.Length, inputStream.Length);

            // close input stream
            inputStream.Close();

            // verify content
            Assert.Equal(inputData, outputData);
        }
    }
}
