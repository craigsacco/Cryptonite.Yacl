using System;
using System.IO;
using System.IO.Compression;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.GZip
{
    public class GZipDecompressStream : GZipBaseStream
    {
        public GZipDecompressStream(Stream innerStream) : this(innerStream, new GZipDecompressSettings())
        {
        }

        public GZipDecompressStream(Stream innerStream, GZipDecompressSettings options)
        {
            m_stream = new GZipStream(innerStream, CompressionMode.Decompress);
            m_innerStream = innerStream;
        }
    }
}
