using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.Common
{
    public abstract class Settings : ISettings
    {
        public abstract bool IsCompressSettings { get; }

        public abstract IList<ISetting> Items { get; }

        public abstract BaseStream CreateStream(Stream innerStream);
    }
}
