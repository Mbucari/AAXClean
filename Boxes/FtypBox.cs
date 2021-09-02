﻿using AAXClean.Util;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
	internal class FtypBox : Box
	{
		public override long RenderSize => base.RenderSize + 8 + CompatibleBrands.Count * 4;

		public string MajorBrand { get; set; }
		public int MajorVersion { get; set; }
		public List<string> CompatibleBrands { get; } = new List<string>();


		internal static FtypBox Create(int size, Box parent)
		{
			var header = new BoxHeader((uint)size, "ftyp");
			var ftyp = new FtypBox(header, parent);
			return ftyp;
		}

		private FtypBox(BoxHeader header, Box parent) : base(header, parent) { }
		internal FtypBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
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
			foreach (var brand in CompatibleBrands)
				file.WriteType(brand);
		}

		private bool _disposed = false;
		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				CompatibleBrands.Clear();
			}

			_disposed = true;

			base.Dispose(disposing);
		}
	}
}
