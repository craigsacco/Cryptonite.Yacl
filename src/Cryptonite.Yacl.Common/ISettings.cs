using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.Common
{
    public interface ISettings
    {
        bool IsCompressSettings { get; }

        IList<ISetting> Items { get; }

        BaseStream CreateStream(Stream innerStream);
    }
}
