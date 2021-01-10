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
using System.Runtime.InteropServices;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2Stream : BaseStream
    {
        private BZip2NativeInterface.Stream m_bzStream;
        private long m_length = 0;
        private long m_compressedLength = 0;
        private byte[] m_workingBuffer = null;

        public override bool CanSeek => false;
        
        public override long Length => m_length;
        
        public override long CompressedLength => m_compressedLength;
        
        public override bool IsClosed => m_workingBuffer == null;

        public override long Position
        {
            get => m_length;
            set => throw new NotSupportedException("Stream position is not settable");
        }

        public BZip2Stream(Stream innerStream, BZip2CompressSettings settings)
            : base(innerStream, settings)
        {
            // initiate the compression stream
            m_bzStream.bzalloc = IntPtr.Zero;
            m_bzStream.bzalloc = IntPtr.Zero;
            m_bzStream.bzalloc = IntPtr.Zero;
            var result = BZip2NativeInterface.BZ2_bzCompressInit(ref m_bzStream,
                settings.BlockSize.Value / 100, 0, settings.WorkFactor.Value);
            if (result != BZip2NativeInterface.Result.OK)
            {
                throw new IOException($"Could not create compression stream (bzip error {result})");
            }

            // setup internals
            m_bzStream.avail_in = 0;
            m_length = 0;
            m_compressedLength = 0;
            m_workingBuffer = new byte[BZip2NativeInterface.BufferSize];
        }

        public BZip2Stream(Stream innerStream, BZip2DecompressSettings settings)
            : base(innerStream, settings)
        {
            // initiate the compression stream
            m_bzStream.bzalloc = IntPtr.Zero;
            m_bzStream.bzalloc = IntPtr.Zero;
            m_bzStream.bzalloc = IntPtr.Zero;
            unsafe
            {
                var result = BZip2NativeInterface.BZ2_bzDecompressInit(ref m_bzStream,
                0, settings.SmallDecompress.Value ? 1 : 0);
                if (result != BZip2NativeInterface.Result.OK)
                {
                    throw new IOException($"Could not create compression stream (bzip error {result})");
                }
            }

            // setup internals
            m_bzStream.avail_in = 0;
            m_length = 0;
            m_compressedLength = 0;
            m_workingBuffer = new byte[BZip2NativeInterface.BufferSize];
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (CanRead)
            {
                StreamUtilities.ValidateBufferArguments(buffer, offset, count);
                if (count == 0)
                {
                    return 0;
                }

                bool done = false;
                int readLength = 0;
                unsafe
                {
                    fixed (byte* outputBufferPtr = buffer, workingBufferPtr = m_workingBuffer)
                    {
                        // initiate decompression operation
                        m_bzStream.avail_out = (uint)count;
                        m_bzStream.next_out = new IntPtr(outputBufferPtr + offset);

                        while (!done)
                        {
                            // read in next chunk from stream
                            if (m_bzStream.avail_in == 0)
                            {
                                m_bzStream.avail_in = (uint)m_innerStream.Read(m_workingBuffer, 0, m_workingBuffer.Length);
                                m_bzStream.next_in = new IntPtr(workingBufferPtr);
                            }

                            if (m_bzStream.avail_in != 0)
                            {
                                // decompress chunk
                                var result = BZip2NativeInterface.BZ2_bzDecompress(ref m_bzStream);
                                if (result != BZip2NativeInterface.Result.OK && result != BZip2NativeInterface.Result.StreamEnd)
                                {
                                    throw new IOException($"Could not decompress the stream (bzip error {result})");
                                }

                                // if reached the end of stream then truncate return buffer
                                if (result == BZip2NativeInterface.Result.StreamEnd)
                                {
                                    done = true;
                                    readLength = count - (int)m_bzStream.avail_out;
                                }

                                // if there is no more data available in the return buffer then we're done
                                if (m_bzStream.avail_out == 0)
                                {
                                    done = true;
                                    readLength = count;
                                }
                            }
                            else
                            {
                                // nothing left in the input - we're done
                                done = true;
                                readLength = count - (int)m_bzStream.avail_out;
                            }
                        }
                    }
                }

                // update lengths and return the read length
                m_compressedLength = (long)((ulong)m_bzStream.total_in_hi32 << 32) + m_bzStream.total_in_lo32;
                m_length = (long)((ulong)m_bzStream.total_out_hi32 << 32) + m_bzStream.total_out_lo32;
                return readLength;
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
                if (count == 0)
                {
                    return;
                }

                byte[] inputBuffer = buffer.Skip(offset).Take(count).ToArray();

                unsafe
                {
                    fixed (byte* inputBufferPtr = inputBuffer, workingBufferPtr = m_workingBuffer)
                    {
                        // init the compression operation
                        m_bzStream.avail_in = (uint)inputBuffer.Length;
                        m_bzStream.next_in = new IntPtr(inputBufferPtr);

                        while (true)
                        {
                            // compress next chunk
                            m_bzStream.avail_out = (uint)m_workingBuffer.Length;
                            m_bzStream.next_out = new IntPtr(workingBufferPtr);
                            var result = BZip2NativeInterface.BZ2_bzCompress(ref m_bzStream, BZip2NativeInterface.Action.Run);
                            if (result != BZip2NativeInterface.Result.RunOK)
                            {
                                throw new IOException($"Could not compress the stream (bzip error {result})");
                            }

                            // write data to output stream
                            if (m_bzStream.avail_out < m_workingBuffer.Length)
                            {
                                m_innerStream.Write(m_workingBuffer, 0, (int)(m_workingBuffer.Length - m_bzStream.avail_out));
                            }

                            // break out if no more data is available
                            if (m_bzStream.avail_in == 0)
                            {
                                break;
                            }
                        }
                    }
                }

                // update lengths
                m_length = (long)((ulong)m_bzStream.total_in_hi32 << 32) + m_bzStream.total_in_lo32;
                m_compressedLength = (long)((ulong)m_bzStream.total_out_hi32 << 32) + m_bzStream.total_out_lo32;
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
                unsafe
                {
                    fixed (byte* workingBufferPtr = m_workingBuffer)
                    {
                        while (true)
                        {
                            // compress any outstanding data in the working buffer
                            m_bzStream.avail_out = (uint)m_workingBuffer.Length;
                            m_bzStream.next_out = new IntPtr(workingBufferPtr);
                            var result = BZip2NativeInterface.BZ2_bzCompress(ref m_bzStream, BZip2NativeInterface.Action.Finish);
                            if (result != BZip2NativeInterface.Result.FinishOK && result != BZip2NativeInterface.Result.StreamEnd)
                            {
                                throw new IOException($"Could not complete compression the stream (bzip error {result})");
                            }

                            // write data to output stream
                            if (m_bzStream.avail_out < m_workingBuffer.Length)
                            {
                                m_innerStream.Write(m_workingBuffer, 0, (int)(m_workingBuffer.Length - m_bzStream.avail_out));
                            }

                            // break out if the input stream has been depleted
                            if (result == BZip2NativeInterface.Result.StreamEnd)
                            {
                                break;
                            }
                        }
                    }

                    // complete the compression process
                    BZip2NativeInterface.BZ2_bzCompressEnd(ref m_bzStream);
                }

                // update lengths and internal state
                m_length = (long)((ulong)m_bzStream.total_in_hi32 << 32) + m_bzStream.total_in_lo32;
                m_compressedLength = (long)((ulong)m_bzStream.total_out_hi32 << 32) + m_bzStream.total_out_lo32;
            }
            else
            {
                unsafe
                {
                    // complete the decompression process
                    BZip2NativeInterface.BZ2_bzDecompressEnd(ref m_bzStream);
                }

                // update lengths and internal state
                m_compressedLength = (long)((ulong)m_bzStream.total_in_hi32 << 32) + m_bzStream.total_in_lo32;
                m_length = (long)((ulong)m_bzStream.total_out_hi32 << 32) + m_bzStream.total_out_lo32;
            }

            // dispose the working buffer
            m_workingBuffer = null;
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
