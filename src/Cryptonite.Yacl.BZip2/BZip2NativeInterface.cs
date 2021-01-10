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
using System.Runtime.InteropServices;

namespace Cryptonite.Yacl.BZip2
{
    internal static class BZip2NativeInterface
    {
        // redefinition of C type bz_stream (in bzlib.h)
        [StructLayout(LayoutKind.Sequential)]
        public struct Stream
        {
            public IntPtr next_in;      // char *next_in;
            public uint avail_in;       // unsigned int avail_in;
            public uint total_in_lo32;  // unsigned int total_in_lo32;
            public uint total_in_hi32;  // unsigned int total_in_hi32;
            public IntPtr next_out;     // char *next_out;
            public uint avail_out;      // unsigned int avail_out;
            public uint total_out_lo32; // unsigned int total_out_lo32;
            public uint total_out_hi32; // unsigned int total_out_hi32;
            public IntPtr state;        // void *state;
            public IntPtr bzalloc;      // void *(*bzalloc)(void *,int,int);
            public IntPtr bzfree;       // void (*bzfree)(void *,void *);
            public IntPtr opaque;       // void *opaque;
        }

        // redefinition of action definitions (in bzlib.h)
        public enum Action : int
        {
            Run = 0,                // #define BZ_RUN               0
            Flush = 1,              // #define BZ_FLUSH             1
            Finish = 2,             // #define BZ_FINISH            2
        }

        // redefinition of result definitions (in bzlib.h)
        public enum Result : int
        {
            OK = 0,                 // #define BZ_OK                0
            RunOK = 1,              // #define BZ_RUN_OK            1
            FlushOK = 2,            // #define BZ_FLUSH_OK          2
            FinishOK = 3,           // #define BZ_FINISH_OK         3
            StreamEnd = 4,          // #define BZ_STREAM_END        4

            SequenceError = -1,     // #define BZ_SEQUENCE_ERROR    (-1)
            ParamError = -2,        // #define BZ_PARAM_ERROR       (-2)
            MemError = -3,          // #define BZ_MEM_ERROR         (-3)
            DataError = -4,         // #define BZ_DATA_ERROR        (-4)
            DataErrorMagic = -5,    // #define BZ_DATA_ERROR_MAGIC  (-5)
            IOError = -6,           // #define BZ_IO_ERROR          (-6)
            UnexpectedEOF = -7,     // #define BZ_UNEXPECTED_EOF    (-7)
            OutputBufferFull = -8,  // #define BZ_OUTBUFF_FULL      (-8)
            ConfigError = -9        // #define BZ_CONFIG_ERROR      (-9)
        }

        // redefinition of default working buffer size (in bzlib.h)
        public const int BufferSize = 5000; // #define BZ_MAX_UNUSED 5000

        public static class Windows
        {
            public const string DllName = "libbz2.dll";
            public const CallingConvention DllCallingConvention = CallingConvention.StdCall;

            // redefinition for "int BZ2_bzCompressInit(bz_stream* strm, int blockSize100k, int verbosity, int workFactor)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompressInit(ref Stream strm, int blockSize100k, int verbosity, int workFactor);

            // redefinition for "int BZ2_bzCompress(bz_stream* strm, int action)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompress(ref Stream strm, Action action);

            // redefinition for "int BZ2_bzCompressEnd(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompressEnd(ref Stream strm);

            // redefinition for "int BZ2_bzDecompressInit(bz_stream* strm, int verbosity, int small)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompressInit(ref Stream strm, int verbosity, int small);

            // redefinition for "int BZ2_bzDecompress(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompress(ref Stream strm);

            // redefinition for "int BZ2_bzDecompressEnd(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompressEnd(ref Stream strm);
        }

        public static class Linux
        {
            public const string DllName = "libbz2.so";
            public const CallingConvention DllCallingConvention = CallingConvention.StdCall;

            // redefinition for "int BZ2_bzCompressInit(bz_stream* strm, int blockSize100k, int verbosity, int workFactor)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompressInit(ref Stream strm, int blockSize100k, int verbosity, int workFactor);

            // redefinition for "int BZ2_bzCompress(bz_stream* strm, int action)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompress(ref Stream strm, Action action);

            // redefinition for "int BZ2_bzCompressEnd(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompressEnd(ref Stream strm);

            // redefinition for "int BZ2_bzDecompressInit(bz_stream* strm, int verbosity, int small)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompressInit(ref Stream strm, int verbosity, int small);

            // redefinition for "int BZ2_bzDecompress(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompress(ref Stream strm);

            // redefinition for "int BZ2_bzDecompressEnd(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompressEnd(ref Stream strm);
        }

        public static class MacOSX
        {
            public const string DllName = "libbz2.dylib";
            public const CallingConvention DllCallingConvention = CallingConvention.StdCall;

            // redefinition for "int BZ2_bzCompressInit(bz_stream* strm, int blockSize100k, int verbosity, int workFactor)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompressInit(ref Stream strm, int blockSize100k, int verbosity, int workFactor);

            // redefinition for "int BZ2_bzCompress(bz_stream* strm, int action)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompress(ref Stream strm, Action action);

            // redefinition for "int BZ2_bzCompressEnd(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzCompressEnd(ref Stream strm);

            // redefinition for "int BZ2_bzDecompressInit(bz_stream* strm, int verbosity, int small)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompressInit(ref Stream strm, int verbosity, int small);

            // redefinition for "int BZ2_bzDecompress(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompress(ref Stream strm);

            // redefinition for "int BZ2_bzDecompressEnd(bz_stream* strm)"
            [DllImport(DllName, CallingConvention = DllCallingConvention)]
            public static extern Result BZ2_bzDecompressEnd(ref Stream strm);
        }

        public static Result BZ2_bzCompressInit(ref Stream strm, int blockSize100k, int verbosity, int workFactor)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BZ2_bzCompressInit(ref strm, blockSize100k, verbosity, workFactor);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BZ2_bzCompressInit(ref strm, blockSize100k, verbosity, workFactor);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSX.BZ2_bzCompressInit(ref strm, blockSize100k, verbosity, workFactor);
            }
            else
            {
                throw new NotSupportedException("OS is not supported");
            }
        }

        public static Result BZ2_bzCompress(ref Stream strm, Action action)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BZ2_bzCompress(ref strm, action);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BZ2_bzCompress(ref strm, action);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSX.BZ2_bzCompress(ref strm, action);
            }
            else
            {
                throw new NotSupportedException("OS is not supported");
            }
        }

        public static Result BZ2_bzCompressEnd(ref Stream strm)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BZ2_bzCompressEnd(ref strm);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BZ2_bzCompressEnd(ref strm);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSX.BZ2_bzCompressEnd(ref strm);
            }
            else
            {
                throw new NotSupportedException("OS is not supported");
            }
        }

        public static Result BZ2_bzDecompressInit(ref Stream strm, int verbosity, int small)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BZ2_bzDecompressInit(ref strm, verbosity, small);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BZ2_bzDecompressInit(ref strm, verbosity, small);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSX.BZ2_bzDecompressInit(ref strm, verbosity, small);
            }
            else
            {
                throw new NotSupportedException("OS is not supported");
            }
        }

        public static Result BZ2_bzDecompress(ref Stream strm)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BZ2_bzDecompress(ref strm);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BZ2_bzDecompress(ref strm);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSX.BZ2_bzDecompress(ref strm);
            }
            else
            {
                throw new NotSupportedException("OS is not supported");
            }
        }

        public static Result BZ2_bzDecompressEnd(ref Stream strm)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Windows.BZ2_bzDecompressEnd(ref strm);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Linux.BZ2_bzDecompressEnd(ref strm);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MacOSX.BZ2_bzDecompressEnd(ref strm);
            }
            else
            {
                throw new NotSupportedException("OS is not supported");
            }
        }
    }
}
