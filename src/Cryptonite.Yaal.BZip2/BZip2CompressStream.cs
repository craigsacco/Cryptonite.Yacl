using System;
using System.IO;
using System.Linq;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2CompressStream : BZip2BaseStream
    {
        private readonly BZip2CompressStreamOptions m_options;

        public BZip2CompressStream(Stream innerStream) : this(innerStream, new BZip2CompressStreamOptions())
        {
        }

        public BZip2CompressStream(Stream innerStream, BZip2CompressStreamOptions options)
        {
            m_options = options;
            m_streamHandle = NativeInterface.BeginCompress(innerStream,
                m_options.BlockSize.Value / 100, 0, m_options.WorkFactor.Value);
        }

        public override bool CanRead => false;

        public override bool CanWrite => true;

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException("Stream is not readable");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            StreamUtilities.ValidateBufferArguments(buffer, offset, count);

            byte[] slice = buffer.Skip(offset).Take(count).ToArray();
            NativeInterface.Compress(m_streamHandle, slice);
        }
        public override void Close()
        {
            NativeInterface.EndCompress(m_streamHandle, false);
        }
    }
}
