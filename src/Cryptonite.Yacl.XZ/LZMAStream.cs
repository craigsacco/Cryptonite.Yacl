/*
 * MIT License
 *
 * Copyright (c) 2020-2021 Craig Sacco
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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

        public LZMAStream(Stream innerStream, LZMADecompressSettings settings)
            : base(innerStream, settings)
        {
            m_streamHandle = NativeInterface.BeginDecompress(innerStream);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (CanRead)
            {
                StreamUtilities.ValidateBufferArguments(buffer, offset, count);
                byte[] data = NativeInterface.Decompress(m_streamHandle, count);
                data.CopyTo(buffer, offset);
                return data.Length;
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
                NativeInterface.EndCompress(m_streamHandle);
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
