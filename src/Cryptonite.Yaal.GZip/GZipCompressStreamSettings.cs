using System;
using System.Collections.Generic;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public class GZipCompressStreamSettings : StreamSettings
    {
        public GZipCompressionLevelStreamSetting CompressionLevel { get; private set; }

        public GZipCompressStreamSettings()
        {
            CompressionLevel = new GZipCompressionLevelStreamSetting();
        }

        public override IList<IStreamSetting> Settings
        {
            get
            {
                IStreamSetting[] settings = { CompressionLevel };
                return new List<IStreamSetting>(settings);
            }
        }
    }
}
