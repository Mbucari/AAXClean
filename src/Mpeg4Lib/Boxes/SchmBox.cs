using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable enable
namespace Mpeg4Lib.Boxes;


public class SchmBox : FullBox
{
    public override long RenderSize => base.RenderSize + 8 + ((Flags & 1) == 1 && SchemeUri is string uri ? Encoding.UTF8.GetByteCount(uri) + (HasNullTerminator ? 1 : 0) : 0);

    public bool HasNullTerminator { get; set; }
    public SchemeType Type { get; }
    public uint SchemeVersion { get; }
    public string? SchemeUri { get; }
    public SchmBox(Stream file, BoxHeader header, IBox parent) : base(file, header, parent)
    {
        long endPos = Header.FilePosition + Header.TotalBoxSize;

        Type = (SchemeType)file.ReadUInt32BE();
        SchemeVersion = file.ReadUInt32BE();

        if ((Flags & 1) == 1)
        {
            List<byte> blist = new();
            while (file.Position < endPos)
            {
                byte lastByte = (byte)file.ReadByte();

                if (lastByte == 0)
                {
                    HasNullTerminator = true;
                    break;
                }

                blist.Add(lastByte);
            }
            SchemeUri = Encoding.UTF8.GetString(blist.ToArray());
        }
    }
    protected override void Render(Stream file)
    {
        base.Render(file);
        file.WriteUInt32BE((uint)Type);
        file.WriteUInt32BE(SchemeVersion);
        if ((Flags & 1) == 1 && SchemeUri is string uri)
        {
            file.Write(Encoding.UTF8.GetBytes(uri));
            if (HasNullTerminator)
                file.WriteByte(0);
        }
    }
    public enum SchemeType : uint
    {
        Unknown,
        Cenc = 0x63656e63,
        Cbc1 = 0x63626331,
        Cens = 0x63656e73,
        Cbcs = 0x63626373
    }
}
