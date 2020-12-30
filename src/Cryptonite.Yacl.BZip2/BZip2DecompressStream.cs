using System;
using System.IO;
using System.Linq;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2DecompressStream : BZip2BaseStream
    {
        public BZip2DecompressStream(Stream innerStream) : this(innerStream, new BZip2DecompressSettings())
        {
        }

        public BZip2DecompressStream(Stream innerStream, BZip2DecompressSettings options)
        {
            m_streamHandle = NativeInterface.BeginDecompress(innerStream, 0, options.SmallDecompress.Value);
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
