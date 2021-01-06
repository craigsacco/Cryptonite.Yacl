using System.IO;

namespace Cryptonite.Yacl.Common
{
    public abstract class BaseStream : Stream
    {
        public abstract long CompressedLength { get; }
    }
}
