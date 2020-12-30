using System;
using System.IO;
using System.Collections.Generic;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.GZip
{
    public class GZipCompressSettings : Settings
    {
        public GZipCompressionLevelSetting CompressionLevel { get; private set; }

        public override IList<ISetting> Items
        {
            get
            {
                ISetting[] settings = { CompressionLevel };
                return new List<ISetting>(settings);
            }
        }

        public GZipCompressSettings() : base()
        {
            CompressionLevel = new GZipCompressionLevelSetting();
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new GZipCompressStream(innerStream, this);
        }
    }
}
