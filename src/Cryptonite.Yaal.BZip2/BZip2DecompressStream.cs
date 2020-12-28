using System;
using System.IO;
using System.Linq;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2DecompressStream : BZip2BaseStream
    {
        private readonly BZip2DecompressStreamOptions m_options;

        public BZip2DecompressStream(Stream innerStream) : this(innerStream, new BZip2DecompressStreamOptions())
        {
        }

        public BZip2DecompressStream(Stream innerStream, BZip2DecompressStreamOptions options)
        {
            m_options = options;
            m_streamHandle = NativeInterface.BeginDecompress(innerStream, 0, m_options.SmallDecompress.Value);
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override int Read(byte[] buffer, int offset, int count)
        {
            StreamUtilities.ValidateBufferArguments(buffer, offset, count);

            byte[] data = NativeInterface.Decompress(m_streamHandle, count);
            
            data.CopyTo(buffer, offset);

            return data.Length;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException("Stream is not writable");
        }

        public override void Close()
        {
            NativeInterface.EndDecompress(m_streamHandle);
        }
    }
}
