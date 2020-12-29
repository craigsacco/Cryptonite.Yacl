using System;
using System.Collections.Generic;

namespace Cryptonite.Yaal.Common
{
    public interface IStreamSettings
    {
        IList<IStreamSetting> Settings { get; }
    }
}
