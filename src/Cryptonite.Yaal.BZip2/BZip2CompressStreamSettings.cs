using System;
using System.Collections.Generic;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2CompressStreamSettings : StreamSettings
    {
        public BZip2BlockSizeStreamSetting BlockSize { get; private set; }

        public BZip2WorkFactorStreamSetting WorkFactor { get; private set; }

        public BZip2CompressStreamSettings()
        {
            BlockSize = new BZip2BlockSizeStreamSetting();
            WorkFactor = new BZip2WorkFactorStreamSetting();
        }

        public override IList<IStreamSetting> Options
        {
            get
            {
                IStreamSetting[] options = { BlockSize, WorkFactor };
                return new List<IStreamSetting>(options);
            }
        }
    }
}
