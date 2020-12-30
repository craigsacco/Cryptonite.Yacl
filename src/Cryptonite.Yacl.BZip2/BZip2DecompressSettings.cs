using System;
using System.IO;
using System.Collections.Generic;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2DecompressSettings : Settings
    {
        public BZip2SmallDecompressSetting SmallDecompress { get; private set; }

        public override IList<ISetting> Items
        {
            get
            {
                ISetting[] settings = { SmallDecompress };
                return new List<ISetting>(settings);
            }
        }

        public BZip2DecompressSettings() : base()
        {
            SmallDecompress = new BZip2SmallDecompressSetting();
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new BZip2DecompressStream(innerStream, this);
        }
    }
}
