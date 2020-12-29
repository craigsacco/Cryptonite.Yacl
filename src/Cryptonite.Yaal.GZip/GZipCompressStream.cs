using System;
using System.IO;
using System.IO.Compression;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public class GZipCompressStream : GZipBaseStream
    {
        private readonly GZipCompressStreamSettings m_options;

        public GZipCompressStream(Stream innerStream) : this(innerStream, new GZipCompressStreamSettings())
        {
        }

        public GZipCompressStream(Stream innerStream, GZipCompressStreamSettings options)
        {
            m_options = options;
            m_stream = new GZipStream(innerStream, m_options.CompressionLevel.Value);
        }
    }
}
