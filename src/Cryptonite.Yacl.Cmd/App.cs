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

using Cryptonite.Yacl.BZip2;
using Cryptonite.Yacl.Common;
using Cryptonite.Yacl.GZip;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

#if false
using Cryptonite.Yacl.XZ;
#endif

namespace Cryptonite.Yacl.Cmd
{
    public class App
    {
        public const String BZip2CompressMethod = "bzip2";
        public const String BZip2DecompressMethod = "bunzip2";
        public const String GZipCompressMethod = "gzip";
        public const String GZipDecompressMethod = "gunzip";
#if false
        public const String LZMACompressMethod = "lzma";
        public const String LZMADecompressMethod = "unlzma";
#endif

        public static readonly IReadOnlyCollection<String> Methods = new ReadOnlyCollection<String>(
            new List<string> {
                BZip2CompressMethod,
                BZip2DecompressMethod,
                GZipCompressMethod,
                GZipDecompressMethod,
#if false
                LZMACompressMethod,
                LZMADecompressMethod,
#endif
            }
        );

        private String m_method = null;
        private FileInfo m_inputFile = null;
        private FileInfo m_outputFile = null;
        private ISettings m_processSettings = null;

        public App()
        {
        }

        public void ParseArgs(string[] args)
        {
            ParseMandatoryArgs(args);
            ParseSettingArgs(args);
        }

        public void Run()
        {
            Console.WriteLine($"Method: {m_method}");
            foreach (var setting in m_processSettings.Items)
            {
                Console.WriteLine($"    {setting.Name}: {setting.DisplayValue}");
            }
            Console.WriteLine();

            if (m_processSettings.IsCompressSettings)
            {
                using (var inputStream = File.OpenRead(m_inputFile.FullName))
                {
                    using (var outputStream = File.Create(m_outputFile.FullName))
                    {
                        using (var processStream = m_processSettings.CreateStream(outputStream))
                        {
                            Console.WriteLine("Compression Process:");
                            byte[] inputData = new byte[1024 * 1024];  // 1MB
                            while (true)
                            {
                                var inputBytes = inputStream.Read(inputData, 0, inputData.Length);
                                if (inputBytes == 0)
                                {
                                    break;
                                }
                                processStream.Write(inputData, 0, inputBytes);
                                Console.WriteLine($"    Uncompressed: {processStream.Length} => Compressed: {processStream.CompressedLength}");
                            }
                        }
                    }
                }

                Console.WriteLine($"    Done - Compressed: {m_outputFile.Length}");
            }
            else
            {
                using (var inputStream = File.OpenRead(m_inputFile.FullName))
                {
                    using (var processStream = m_processSettings.CreateStream(inputStream))
                    {
                        using (var outputStream = File.Create(m_outputFile.FullName))
                        {
                            Console.WriteLine("Decompression Process:");
                            byte[] inputData = new byte[1024 * 1024];  // 1MB
                            while (true)
                            {
                                var inputBytes = processStream.Read(inputData, 0, inputData.Length);
                                if (inputBytes == 0)
                                {
                                    break;
                                }
                                outputStream.Write(inputData, 0, inputBytes);
                                Console.WriteLine($"    Compressed: {processStream.CompressedLength} => Uncompressed: {processStream.Length}");
                            }
                        }
                    }
                }

                Console.WriteLine("    Done");
            }
        }

        private void ParseMandatoryArgs(string[] args)
        {
            // generate option set
            var optionSet = new OptionSet();
            optionSet.Add("input=", value => m_inputFile = new FileInfo(value));
            optionSet.Add("output=", value => m_outputFile = new FileInfo(value));
            optionSet.Add("method=", value => m_method = value);

            // parse args
            optionSet.Parse(args);

            // validate args
            if (m_inputFile == null || m_inputFile.Exists == false)
            {
                throw new ArgumentException("Input file is not set, or does not exist");
            }
            if (m_outputFile == null)
            {
                throw new ArgumentException("Output file is not set");
            }
            if (m_inputFile.FullName == m_outputFile.FullName)
            {
                throw new ArgumentException("The input and output files cannot be the same");
            }
            if (m_method == null || Methods.Count(value => value == m_method) == 0)
            {
                throw new ArgumentException("Method is not set, or is not valid");
            }

            // construct process settings and compression mode based on method
            switch (m_method)
            {
                case BZip2CompressMethod:
                    m_processSettings = new BZip2CompressSettings();
                    break;

                case BZip2DecompressMethod:
                    m_processSettings = new BZip2DecompressSettings();
                    break;

                case GZipCompressMethod:
                    m_processSettings = new GZipCompressSettings();
                    break;

                case GZipDecompressMethod:
                    m_processSettings = new GZipDecompressSettings();
                    break;

#if false
                case LZMACompressMethod:
                    m_processSettings = new LZMACompressSettings();
                    break;

                case LZMADecompressMethod:
                    m_processSettings = new LZMADecompressSettings();
                    break;
#endif

                default:
                    throw new InvalidOperationException(@"Method name {m_method} is not handled");
            }
        }

        private void ParseSettingArgs(string[] args)
        {
            // generate option set
            var optionSet = new OptionSet();
            foreach (var setting in m_processSettings.Items)
            {
                optionSet.Add($"{setting.AbbreviatedName}=", setting.Name, value => setting.Parse(value));
            }

            // parse args
            optionSet.Parse(args);
        }

        static void Main(string[] args)
        {
            var app = new App();
            app.ParseArgs(args);
            app.Run();
        }
    }
}
