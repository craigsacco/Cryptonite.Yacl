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
using System.IO.Compression;

namespace Cryptonite.Yacl.GZip
{
    public class GZipStream : BaseStream
    {
        protected readonly System.IO.Compression.GZipStream m_gzipStream = null;
        protected long m_length = 0;
        protected long m_compressedLength = 0;
        protected bool m_closed = false;

        public override bool CanSeek => false;

        public override long Length => m_length;

        public override long CompressedLength => m_compressedLength;

        public override bool IsClosed => m_closed;

        public override long Position
        {
            get => m_length;
            set => throw new NotSupportedException("Stream position is not settable");
        }

        public GZipStream(Stream innerStream, GZipCompressSettings settings)
            : base(innerStream, settings)
        {
            m_gzipStream = new System.IO.Compression.GZipStream(innerStream,
                settings.CompressionLevel.Value);
        }

        public GZipStream(Stream innerStream, GZipDecompressSettings settings)
            : base(innerStream, settings)
        {
            m_gzipStream = new System.IO.Compression.GZipStream(innerStream,
                CompressionMode.Decompress);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (CanRead)
            {
                // no nice way to determine length of compressed input, so will
                // need to be determined by the stream wrapped by GZipStream

                var positionBeforeRead = GetInnerStreamPosition();
                var returnedCount = m_gzipStream.Read(buffer, offset, count);
                var positionAfterRead = GetInnerStreamPosition();

                m_length += returnedCount;
                m_compressedLength += (positionAfterRead - positionBeforeRead);

                return returnedCount;
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
                // no nice way to determine length of compressed output, so will
                // need to be determined by the stream wrapped by GZipStream

                var positionBeforeWrite = GetInnerStreamPosition();
                m_gzipStream.Write(buffer, offset, count);
                var positionAfterWrite = GetInnerStreamPosition();

                m_length += count;
                m_compressedLength += (positionAfterWrite - positionBeforeWrite);
            }
            else
            {
                throw new NotSupportedException("Stream is not writable");
            }
        }

        public override void Close()
        {
            // no nice way to determine length of compressed I/O, so will
            // need to be determined by the stream wrapped by GZipStream

            var positionBeforeWrite = GetInnerStreamPosition();
            m_gzipStream.Close();
            var positionAfterWrite = GetInnerStreamPosition();

            m_compressedLength += (positionAfterWrite - positionBeforeWrite);
            m_closed = true;
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

        private long GetInnerStreamPosition()
        {
            // try to return position of the inner stream wrapped by
            // GZipStream, or return 0
            try
            {
                return m_innerStream.Position;
            }
            catch
            {
                return 0;
            }
        }
    }
}
