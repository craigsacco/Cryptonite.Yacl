using System;
using System.Collections.Generic;

namespace Cryptonite.Yaal.Common
{
    public abstract class StreamSettings : IStreamSettings
    {
        public abstract IList<IStreamSetting> Settings { get; }
    }
}
