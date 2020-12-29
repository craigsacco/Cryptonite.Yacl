using System;
using System.IO;
using System.IO.Compression;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public abstract class GZipBaseStream : BaseStream
    {
        protected GZipStream m_stream = null;

        protected long m_length = 0;
        protected long m_compressedLength = 0;

        public override long CompressedLength => m_compressedLength;

        public override bool CanRead => m_stream.CanRead;

        public override bool CanSeek => m_stream.CanSeek;

        public override bool CanWrite => m_stream.CanWrite;

        public override long Length => m_length;

        public override long Position
        {
            get => m_length;
            set => throw new NotSupportedException("Stream position is not settable");
        }

        public override void Flush()
        {
            throw new NotSupportedException("Stream is not flushable");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int returnedCount = m_stream.Read(buffer, offset, count);
            m_length += returnedCount;
            return returnedCount;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Stream is not seekable");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Stream length is not adjustible");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            m_stream.Write(buffer, offset, count);
            m_length += count;
        }
        public override void Close()
        {
            m_stream.Close();
        }
    }
}
