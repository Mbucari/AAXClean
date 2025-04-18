﻿using System.Diagnostics;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public static class BoxFactory
	{
		public static IBox CreateBox(Stream file, IBox parent)
			=> CreateBox(new BoxHeader(file), file, parent);

		public static IBox CreateBox(BoxHeader header, Stream file, IBox parent)
		{
			IBox box = header.Type switch
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
				"stsz" => new StszBox(file, header, parent),
				"stz2" => new Stz2Box(file, header, parent),
				"stco" => new StcoBox(file, header, parent),
				"co64" => new Co64Box(file, header, parent),
				"udta" => new UdtaBox(file, header, parent),
				"meta" => new MetaBox(file, header, parent),
				"ilst" => new AppleListBox(file, header, parent),
				"data" => new AppleDataBox(file, header, parent),
				"mean" => new MeanBox(file, header, parent),
				"name" => new NameBox(file, header, parent),
				"pssh" => new PsshBox(file, header, parent),
				"sidx" => new SidxBox(file, header, parent),
				"moof" => new MoofBox(file, header, parent),
				"mfhd" => new MfhdBox(file, header, parent),
				"tfhd" => new TfhdBox(file, header, parent),
				"traf" => new TrafBox(file, header, parent),
				"tfdt" => new TfdtBox(file, header, parent),
				"trun" => new TrunBox(file, header, parent),
				"saiz" => new SaizBox(file, header, parent),
				"saio" => new SaioBox(file, header, parent),
				"schm" => new SchmBox(file, header, parent),
				"frma" => new FrmaBox(file, header, parent),
				"tenc" => new TencBox(file, header, parent),
				"schi" => new SchiBox(file, header, parent),
				"sinf" => new SinfBox(file, header, parent),
				"senc" => new SencBox(file, header, parent),
				"mvex" => new MvexBox(file, header, parent),
				"mehd" => new MehdBox(file, header, parent),
				_ => new UnknownBox(file, header, parent),
			};
			Debug.Assert(box.RenderSize == header.TotalBoxSize || box is MdatBox);
			return box;
		}
	}
}
