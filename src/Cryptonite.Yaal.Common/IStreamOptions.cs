using System;
using System.Collections.Generic;

namespace Cryptonite.Yaal.Common
{
    public interface IStreamOptions
    {
        IList<IStreamOption> Options { get; }
    }
}
