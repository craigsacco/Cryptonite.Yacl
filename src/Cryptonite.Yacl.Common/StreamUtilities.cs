using System;
using System.IO;
using System.Linq;

namespace Cryptonite.Yacl.Common
{
    public static class StreamUtilities
    {
        public static void ValidateBufferArguments(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (buffer.Length - offset < count)
            {
                throw new ArgumentException("Arguments would cause overrun");
            }
        }
    }
}
