using System;
using System.Collections.Generic;

namespace Cryptonite.Yaal.Common
{
    public abstract class StreamOptions : IStreamOptions
    {
        public abstract IList<IStreamOption> Options { get; }
    }
}
