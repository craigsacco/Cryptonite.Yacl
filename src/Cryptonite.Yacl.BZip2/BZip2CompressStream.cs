using System;
using System.IO;
using System.Linq;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2CompressStream : BZip2BaseStream
    {
        public BZip2CompressStream(Stream innerStream) : this(innerStream, new BZip2CompressSettings())
        {
        }

        public BZip2CompressStream(Stream innerStream, BZip2CompressSettings options)
        {
            m_streamHandle = NativeInterface.BeginCompress(innerStream,
                options.BlockSize.Value / 100, 0, options.WorkFactor.Value);
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
