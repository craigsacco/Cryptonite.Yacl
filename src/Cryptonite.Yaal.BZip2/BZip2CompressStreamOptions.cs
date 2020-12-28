using System;
using System.Collections.Generic;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2CompressStreamOptions : StreamOptions
    {
        public BZip2BlockSizeStreamOption BlockSize { get; private set; }

        public BZip2WorkFactorStreamOption WorkFactor { get; private set; }

        public BZip2CompressStreamOptions()
        {
            BlockSize = new BZip2BlockSizeStreamOption();
            WorkFactor = new BZip2WorkFactorStreamOption();
        }

        public override IList<IStreamOption> Options
        {
            get
            {
                IStreamOption[] options = { BlockSize, WorkFactor };
                return new List<IStreamOption>(options);
            }
        }
    }
}
