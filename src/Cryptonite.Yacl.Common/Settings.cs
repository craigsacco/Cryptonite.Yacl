using System;
using System.IO;
using System.Collections.Generic;

namespace Cryptonite.Yacl.Common
{
    public abstract class Settings : ISettings
    {
        public abstract IList<ISetting> Items { get; }

        public abstract BaseStream CreateStream(Stream innerStream);
    }
}
