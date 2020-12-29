using System;
using System.IO;
using System.IO.Compression;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public abstract class GZipBaseStream : BaseStream
    {
        protected GZipStream m_stream = null;
        protected Stream m_innerStream = null;

        protected long m_length = 0;
        protected long m_compressedLength = 0;

        public override long CompressedLength => m_compressedLength;

        public override bool CanRead => m_stream.CanRead;

        public override bool CanSeek => false;

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
            // no nice way to determine length of compressed input, so will
            // need to be determined by the stream wrapped by GZipStream

            var positionBeforeRead = GetInnerStreamPosition();
            var returnedCount = m_stream.Read(buffer, offset, count);
            var positionAfterRead = GetInnerStreamPosition();

            m_length += returnedCount;
            m_compressedLength += (positionAfterRead - positionBeforeRead);

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
            // no nice way to determine length of compressed output, so will
            // need to be determined by the stream wrapped by GZipStream

            var positionBeforeWrite = GetInnerStreamPosition();
            m_stream.Write(buffer, offset, count);
            var positionAfterWrite = GetInnerStreamPosition();

            m_length += count;
            m_compressedLength += (positionAfterWrite - positionBeforeWrite);
        }

        public override void Close()
        {
            // no nice way to determine length of compressed I/O, so will
            // need to be determined by the stream wrapped by GZipStream

            var positionBeforeWrite = GetInnerStreamPosition();
            m_stream.Close();
            var positionAfterWrite = GetInnerStreamPosition();

            m_compressedLength += (positionAfterWrite - positionBeforeWrite);
        }

        private long GetInnerStreamPosition()
        {
            // try to return position of the inner stream wrapped by
            // GZipStream, or return 0
            try
            {
                return m_innerStream.Position;
            }
            catch
            {
                return 0;
            }
        }
    }
}
