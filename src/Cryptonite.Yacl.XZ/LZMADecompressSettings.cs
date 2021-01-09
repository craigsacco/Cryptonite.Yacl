using Cryptonite.Yacl.Common;
using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.XZ
{
    public class LZMADecompressSettings : Settings
    {
        public override bool IsCompressSettings => false;

        public override IList<ISetting> Items
        {
            get
            {
                return new List<ISetting>();
            }
        }

        public LZMADecompressSettings() : base()
        {
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new LZMAStream(innerStream, this);
        }
    }
}
