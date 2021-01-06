using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.Common
{
    public interface ISettings
    {
        IList<ISetting> Items { get; }

        BaseStream CreateStream(Stream innerStream);
    }
}
