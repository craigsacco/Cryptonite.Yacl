using System;
using System.IO;
using System.Collections.Generic;

namespace Cryptonite.Yacl.Common
{
    public interface ISettings
    {
        IList<ISetting> Items { get; }

        BaseStream CreateStream(Stream innerStream);
    }
}
