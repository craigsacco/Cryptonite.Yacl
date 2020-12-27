using System;
using Cryptonite.Yaal.BZip2;
using Xunit;

namespace Cryptonite.Yaal.BZip2.Tests
{
    public class NativeInterfaceTests
    {
        [Fact]
        public void CompleteWriteWorkflow()
        {
            // open output file
            NativeInterface.InternalHandle outputFile = NativeInterface.WriteOpen("Test.txt.bz2");
            Assert.True(outputFile.IsValid(true));

            // write a byte array to compressed output
            byte[] inputData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            NativeInterface.Write(outputFile, inputData);
            Assert.True(outputFile.IsValid(true));

            // finalise output file
            NativeInterface.WriteClose(outputFile);
            Assert.False(outputFile.IsValid(true));

            // open input file
            NativeInterface.InternalHandle inputFile = NativeInterface.ReadOpen("Test.txt.bz2");
            Assert.True(inputFile.IsValid(false));

            // read bytes from compressed output
            byte[] outputData = NativeInterface.Read(inputFile, 5000);
            Assert.True(inputFile.IsValid(false));

            // finalise input file
            NativeInterface.ReadClose(inputFile);
            Assert.False(inputFile.IsValid(false));

            // verify content
            Assert.Equal(inputData, outputData);
        }
    }
}
