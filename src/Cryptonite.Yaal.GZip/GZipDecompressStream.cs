using System;
using System.IO;
using System.IO.Compression;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public class GZipDecompressStream : GZipBaseStream
    {
        private readonly GZipDecompressStreamSettings m_options;

        public GZipDecompressStream(Stream innerStream) : this(innerStream, new GZipDecompressStreamSettings())
        {
        }

        public GZipDecompressStream(Stream innerStream, GZipDecompressStreamSettings options)
        {
            m_options = options;
            m_stream = new GZipStream(innerStream, CompressionMode.Decompress);
            m_innerStream = innerStream;
        }
    }
}
