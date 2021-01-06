using Cryptonite.Yacl.BZip2;
using Cryptonite.Yacl.Common;
using Cryptonite.Yacl.GZip;
using Cryptonite.Yacl.XZ;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Cryptonite.Yacl.Cmd
{
    public class App
    {
        public const String BZip2CompressMethod = "bzip2";
        public const String BZip2DecompressMethod = "bunzip2";
        public const String GZipCompressMethod = "gzip";
        public const String GZipDecompressMethod = "gunzip";
        public const String LZMACompressMethod = "lzma";

        public static readonly IReadOnlyCollection<String> Methods = new ReadOnlyCollection<String>(
            new List<string> {
                BZip2CompressMethod,
                BZip2DecompressMethod,
                GZipCompressMethod,
                GZipDecompressMethod,
                LZMACompressMethod
            }
        );

        private String m_method = null;
        private FileInfo m_inputFile = null;
        private FileInfo m_outputFile = null;
        private ISettings m_processSettings = null;
        private bool m_compress = false;

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

            if (m_compress)
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
                    m_compress = true;
                    break;

                case BZip2DecompressMethod:
                    m_processSettings = new BZip2DecompressSettings();
                    m_compress = false;
                    break;

                case GZipCompressMethod:
                    m_processSettings = new GZipCompressSettings();
                    m_compress = true;
                    break;

                case GZipDecompressMethod:
                    m_processSettings = new GZipDecompressSettings();
                    m_compress = false;
                    break;

                case LZMACompressMethod:
                    m_processSettings = new LZMACompressSettings();
                    m_compress = true;
                    break;

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
