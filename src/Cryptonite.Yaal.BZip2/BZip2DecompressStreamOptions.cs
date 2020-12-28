using System;
using System.Collections.Generic;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2DecompressStreamOptions : StreamOptions
    {
        public BZip2SmallDecompressStreamOption SmallDecompress { get; private set; }

        public BZip2DecompressStreamOptions()
        {
            SmallDecompress = new BZip2SmallDecompressStreamOption();
        }

        public override IList<IStreamOption> Options
        {
            get
            {
                IStreamOption[] options = { SmallDecompress };
                return new List<IStreamOption>(options);
            }
        }
    }
}
