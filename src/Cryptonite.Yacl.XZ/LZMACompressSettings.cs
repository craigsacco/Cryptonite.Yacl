using Cryptonite.Yacl.Common;
using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.XZ
{
    public class LZMACompressSettings : Settings
    {
        public LZMACompressionPresetSetting CompressionPreset { get; private set; }

        public LZMAExtremeCompressionSetting ExtremeCompression { get; private set; }

        public override IList<ISetting> Items
        {
            get
            {
                ISetting[] settings = { CompressionPreset, ExtremeCompression };
                return new List<ISetting>(settings);
            }
        }

        public LZMACompressSettings() : base()
        {
            CompressionPreset = new LZMACompressionPresetSetting();
            ExtremeCompression = new LZMAExtremeCompressionSetting();
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new LZMACompressStream(innerStream, this);
        }
    }
}
