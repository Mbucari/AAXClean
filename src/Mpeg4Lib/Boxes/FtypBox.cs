﻿using Mpeg4Lib.Util;
using System.Collections.Generic;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class FtypBox : Box
	{
		public override long RenderSize => base.RenderSize + 8 + CompatibleBrands.Count * 4;
		public string MajorBrand { get; set; }
		public int MajorVersion { get; set; }
		public List<string> CompatibleBrands { get; } = new List<string>();

		public static FtypBox Create(int size, IBox parent)
		{
			BoxHeader header = new BoxHeader((uint)size, "ftyp");
			FtypBox ftyp = new FtypBox(header, parent);
			return ftyp;
		}

		private FtypBox(BoxHeader header, IBox parent) : base(header, parent) { }
		public FtypBox(Stream file, BoxHeader header, IBox parent) : base(header, parent)
		{
			long endPos = header.FilePosition + header.TotalBoxSize;

			MajorBrand = file.ReadType();
			MajorVersion = file.ReadInt32BE();

			while (file.Position < endPos)
				CompatibleBrands.Add(file.ReadType());
		}

		protected override void Render(Stream file)
		{
			file.WriteType(MajorBrand);
			file.WriteInt32BE(MajorVersion);
			foreach (string brand in CompatibleBrands)
				file.WriteType(brand);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				CompatibleBrands.Clear();
			base.Dispose(disposing);
		}
	}
}
