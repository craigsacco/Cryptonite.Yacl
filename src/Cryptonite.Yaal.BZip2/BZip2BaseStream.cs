using System;
using System.IO;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public abstract class BZip2BaseStream : BaseStream
    {
        protected NativeInterface.StreamHandle m_streamHandle;

        public override bool CanSeek => false;
        
        public override long Length => m_streamHandle.UncompressedLength;

        public override long CompressedLength => m_streamHandle.CompressedLength;

        protected bool IsClosed => m_streamHandle.IsEnded;

        public override long Position
        {
            get => m_streamHandle.UncompressedLength;
            set => throw new InvalidOperationException("Stream position is not settable");
        }

        public override void Flush()
        {
            throw new InvalidOperationException("Stream is not flushable");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException("Stream is not seekable");
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException("Stream length is not adjustible");
        }
    }
}
