using System;
using System.IO;

namespace Cryptonite.Yaal.Common
{
    public abstract class BaseStream : Stream
    {
        public abstract long CompressedLength { get; }
    }
}
