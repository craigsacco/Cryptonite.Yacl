using Cryptonite.Yacl.Common;
using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.GZip
{
    public class GZipCompressSettings : Settings
    {
        public override bool IsCompressSettings => true;

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
            return new GZipStream(innerStream, this);
        }
    }
}
