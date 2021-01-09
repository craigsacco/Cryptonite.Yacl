using System.IO;

namespace Cryptonite.Yacl.Common
{
    public abstract class BaseStream : Stream
    {
        protected Stream m_innerStream;
        private readonly bool m_isCompressStream;

        public abstract long CompressedLength { get; }

        public abstract bool IsClosed { get; }

        public override bool CanRead => (m_isCompressStream == false);

        public override bool CanWrite => (m_isCompressStream == true);

        protected BaseStream(Stream innerStream, ISettings settings)
        {
            m_isCompressStream = settings.IsCompressSettings;
        }
    }
}
