using System;
using System.IO;
using System.Collections.Generic;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2CompressSettings : Settings
    {
        public BZip2BlockSizeSetting BlockSize { get; private set; }

        public BZip2WorkFactorSetting WorkFactor { get; private set; }

        public override IList<ISetting> Items
        {
            get
            {
                ISetting[] settings = { BlockSize, WorkFactor };
                return new List<ISetting>(settings);
            }
        }

        public BZip2CompressSettings() : base()
        {
            BlockSize = new BZip2BlockSizeSetting();
            WorkFactor = new BZip2WorkFactorSetting();
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new BZip2CompressStream(innerStream, this);
        }
    }
}
