using AAXClean.Chunks;
using System.IO;

namespace AAXClean.Boxes
{
    internal class BoxFactory
    {
        internal static Box CreateBox(Stream file, Box parent = null)
        {
            var header = new BoxHeader(file);

            return header.Type switch
            {
                "free" or "skip" => new FreeBox(file, header, parent),
                "ftyp" => new FtypBox(file, header, parent),
                "mdat" => new MdatBox(file, header, parent),
                "moov" => new MoovBox(file, header, parent),
                "trak" => new TrakBox(file, header, parent),
                "mdia" => new MdiaBox(file, header, parent),
                "minf" => new MinfBox(file, header, parent),
                "mdhd" => new MdhdBox(file, header, parent),
                "hdlr" => new HdlrBox(file, header, parent),
                "stbl" => new StblBox(file, header, parent),
                "stsd" => new StsdBox(file, header, parent),
                "esds" => new EsdsBox(file, header, parent),
                "btrt" => new BtrtBox(file, header, parent),
                "stts" => new SttsBox(file, header, parent),
                "stsc" => new StscBox(file, header, parent),
                "stsz" => new StszBox(file, header, parent),
                "stco" => new StcoBox(file, header, parent),
                "udta" => new UdtaBox(file, header, parent),
                "meta" => new MetaBox(file, header, parent),
                "ilst" => new AppleListBox(file, header, parent),
                "data" => new AppleDataBox(file, header, parent),
                _ => new UnknownBox(file, header, parent),
            };
        }
    }
    internal class MdatFactory
    {
        internal static MdatChunk CreateEntry(Stream file)
        {
            var header = new BoxHeader(file);

            switch (header.Type)
            {
                case "aavd":
                    return new AavdChunk(file, header);
                case "text":
                    return new TextChunk(file, header);
                default:
                    break;
            }
            return default;
        }
    }
}
