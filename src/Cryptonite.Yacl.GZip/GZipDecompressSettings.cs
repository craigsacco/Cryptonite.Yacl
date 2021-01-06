using Cryptonite.Yacl.Common;
using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.GZip
{
    public class GZipDecompressSettings : Settings
    {
        public override IList<ISetting> Items => new List<ISetting>();

        public GZipDecompressSettings() : base()
        {
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new GZipDecompressStream(innerStream, this);
        }
    }
}
