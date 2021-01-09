/*
 * MIT License
 *
 * Copyright (c) 2020-2021 Craig Sacco
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Cryptonite.Yacl.Common;
using System.Collections.Generic;
using System.IO;

namespace Cryptonite.Yacl.XZ
{
    public class LZMACompressSettings : Settings
    {
        public override bool IsCompressSettings => true;

        public LZMACompressionPresetSetting CompressionPreset { get; private set; }

        public LZMAExtremeCompressionSetting ExtremeCompression { get; private set; }

        public override IList<ISetting> Items
        {
            get
            {
                ISetting[] settings = { CompressionPreset, ExtremeCompression };
                return new List<ISetting>(settings);
            }
        }

        public LZMACompressSettings() : base()
        {
            CompressionPreset = new LZMACompressionPresetSetting();
            ExtremeCompression = new LZMAExtremeCompressionSetting();
        }

        public override BaseStream CreateStream(Stream innerStream)
        {
            return new LZMAStream(innerStream, this);
        }
    }
}
