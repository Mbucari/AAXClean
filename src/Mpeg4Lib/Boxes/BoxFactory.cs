using System.IO;

namespace Mpeg4Lib.Boxes
{
	public static class BoxFactory
	{
		public static bool Read16bitSampleSizes { get; set; } = false;
		public static Box CreateBox(Stream file, Box parent)
		{
			BoxHeader header = new(file);

			return header.Type switch
			{
				"free" or "skip" => new FreeBox(file, header, parent),
				"ftyp" => new FtypBox(file, header, parent),
				"mdat" => new MdatBox(header, parent),
				"moov" => new MoovBox(file, header, parent),
				"mvhd" => new MvhdBox(file, header, parent),
				"trak" => new TrakBox(file, header, parent),
				"tkhd" => new TkhdBox(file, header, parent),
				"mdia" => new MdiaBox(file, header, parent),
				"minf" => new MinfBox(file, header, parent),
				"mdhd" => new MdhdBox(file, header, parent),
				"hdlr" => new HdlrBox(file, header, parent),
				"stbl" => new StblBox(file, header, parent),
				"stsd" => new StsdBox(file, header, parent),
				"esds" => new EsdsBox(file, header, parent),
				"btrt" => new BtrtBox(file, header, parent),
				"adrm" => new AdrmBox(file, header, parent),
				"stts" => new SttsBox(file, header, parent),
				"stsc" => new StscBox(file, header, parent),
				"stsz" => Read16bitSampleSizes ? new Stsz16(file, header, parent) : new Stsz32(file, header, parent),
				"stco" => new StcoBox(file, header, parent),
				"co64" => new Co64Box(file, header, parent),
				"udta" => new UdtaBox(file, header, parent),
				"meta" => new MetaBox(file, header, parent),
				"ilst" => new AppleListBox(file, header, parent),
				"data" => new AppleDataBox(file, header, parent),
				_ => new UnknownBox(file, header, parent),
			};
		}
	}
}
