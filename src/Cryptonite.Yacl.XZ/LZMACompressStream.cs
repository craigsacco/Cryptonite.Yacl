using System;
using System.IO;
using System.Linq;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.XZ
{
    public class LZMACompressStream : LZMABaseStream
    {
        public LZMACompressStream(Stream innerStream) : this(innerStream, new LZMACompressSettings())
        {
        }

        public LZMACompressStream(Stream innerStream, LZMACompressSettings options)
        {
            m_streamHandle = NativeInterface.BeginCompress(innerStream,
                options.CompressionPreset.Value, options.ExtremeCompression.Value);
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
            NativeInterface.EndCompress(m_streamHandle);
        }
    }
}
