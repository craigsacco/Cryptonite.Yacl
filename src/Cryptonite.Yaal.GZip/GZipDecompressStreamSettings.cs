using System;
using System.Collections.Generic;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public class GZipDecompressStreamSettings : StreamSettings
    {
        public GZipDecompressStreamSettings()
        {
        }

        public override IList<IStreamSetting> Settings => new List<IStreamSetting>();
    }
}
