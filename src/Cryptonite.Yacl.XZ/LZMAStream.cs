using Cryptonite.Yacl.Common;
using System;
using System.IO;
using System.Linq;

namespace Cryptonite.Yacl.XZ
{
    public class LZMAStream : BaseStream
    {
        protected readonly NativeInterface.StreamHandle m_streamHandle;

        public override bool CanSeek => false;

        public override long Length => m_streamHandle.UncompressedLength;

        public override long CompressedLength => m_streamHandle.CompressedLength;

        public override bool IsClosed => m_streamHandle.IsEnded;

        public override long Position
        {
            get => m_streamHandle.UncompressedLength;
            set => throw new NotSupportedException("Stream position is not settable");
        }

        public LZMAStream(Stream innerStream, LZMACompressSettings settings)
            : base(innerStream, settings)
        {
            m_streamHandle = NativeInterface.BeginCompress(innerStream,
                settings.CompressionPreset.Value, settings.ExtremeCompression.Value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (CanRead)
            {
                throw new NotImplementedException("Not implemented");
            }
            else
            {
                throw new NotSupportedException("Stream is not readable");
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (CanWrite)
            {
                StreamUtilities.ValidateBufferArguments(buffer, offset, count);
                byte[] slice = buffer.Skip(offset).Take(count).ToArray();
                NativeInterface.Compress(m_streamHandle, slice);
            }
            else
            {
                throw new NotSupportedException("Stream is not writable");
            }
        }

        public override void Close()
        {
            if (CanWrite)
            {
                throw new NotImplementedException("Not implemented");
            }
            else
            {
                NativeInterface.EndDecompress(m_streamHandle);
            }
        }

        public override void Flush()
        {
            throw new NotSupportedException("Stream is not flushable");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Stream is not seekable");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Stream length is not adjustible");
        }
    }
}
