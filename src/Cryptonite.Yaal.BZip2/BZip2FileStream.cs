using System;
using System.IO;
using System.Linq;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2FileStream : Stream
    {
        public BZip2FileStream(String file, bool writing)
        {
            m_internalHandle = writing ? NativeInterface.WriteOpen(file) : NativeInterface.ReadOpen(file);
            m_length = 0;
        }

        private NativeInterface.InternalHandle m_internalHandle;
        private long m_length;

        public override bool CanRead => m_internalHandle.Writing == false;

        public override bool CanSeek => false;

        public override bool CanWrite => m_internalHandle.Writing == true;

        public override long Length => m_length;

        public override long Position { get => m_length; set => throw new InvalidOperationException("Stream position is not settable"); }

        public override void Flush()
        {
            if (m_internalHandle.Writing == true)
            {
                NativeInterface.WriteFlush(m_internalHandle);
            }
            else
            {
                throw new InvalidOperationException("Stream is not open in write mode");
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (m_internalHandle.Writing == false)
            {
                StreamUtilities.ValidateBufferArguments(buffer, offset, count);

                byte[] data = NativeInterface.Read(m_internalHandle, count);
                m_length += data.Length;

                data.CopyTo(buffer, offset);

                return data.Length;
            }
            else
            {
                throw new InvalidOperationException("Stream is not open in read mode");
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException("Stream is not seekable");
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException("Stream length is not adjustible");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (m_internalHandle.Writing == true)
            {
                StreamUtilities.ValidateBufferArguments(buffer, offset, count);

                byte[] slice = buffer.Skip(offset).Take(count).ToArray();
                NativeInterface.Write(m_internalHandle, slice);
                m_length += count;
            }
            else
            {
                throw new InvalidOperationException("Stream is not open in write mode");
            }
        }

        public override void Close()
        {
            if (m_internalHandle.Writing == true)
            {
                NativeInterface.WriteClose(m_internalHandle);
            }
            else
            {
                NativeInterface.ReadClose(m_internalHandle);
            }
        }
    }
}
