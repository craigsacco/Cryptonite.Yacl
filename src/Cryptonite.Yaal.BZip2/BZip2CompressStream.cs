using System;
using System.IO;
using System.Linq;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2CompressStream : BZip2BaseStream
    {
        public BZip2CompressStream(Stream innerStream)
        {
            m_streamHandle = NativeInterface.BeginCompress(innerStream, 1, 0, 0);
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
