using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class SencBox : FullBox
{
    public bool UseSubSampleEncryption => (Flags & 2) == 2;

    public byte[][] IVs { get; }

    public SencBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
    {
        var sampleCount = file.ReadInt32BE();

        if (UseSubSampleEncryption)
            throw new NotSupportedException(nameof(UseSubSampleEncryption));

        var ivSize = (int)((header.TotalBoxSize - 16) / sampleCount);

        IVs = new byte[sampleCount][];

        for (int i = 0; i < sampleCount; i++)
        {
            IVs[i] = file.ReadBlock(ivSize);
        }
    }
}
