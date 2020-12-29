using System;
using System.Collections.Generic;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2DecompressStreamSettings : StreamSettings
    {
        public BZip2SmallDecompressStreamSetting SmallDecompress { get; private set; }

        public BZip2DecompressStreamSettings()
        {
            SmallDecompress = new BZip2SmallDecompressStreamSetting();
        }

        public override IList<IStreamSetting> Settings
        {
            get
            {
                IStreamSetting[] settings = { SmallDecompress };
                return new List<IStreamSetting>(settings);
            }
        }
    }
}
