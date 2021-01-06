using System.IO;
using System.IO.Compression;

namespace Cryptonite.Yacl.GZip
{
    public class GZipCompressStream : GZipBaseStream
    {
        public GZipCompressStream(Stream innerStream) : this(innerStream, new GZipCompressSettings())
        {
        }

        public GZipCompressStream(Stream innerStream, GZipCompressSettings options)
        {
            m_stream = new GZipStream(innerStream, options.CompressionLevel.Value);
            m_innerStream = innerStream;
        }
    }
}
