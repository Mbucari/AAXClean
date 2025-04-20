using Mpeg4Lib.Util;
using System;
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

		public static FtypBox Create(string majorBrand, int majorVersion)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(majorBrand, nameof(majorBrand));
			if (majorBrand.Length != 4)
				throw new ArgumentException("Major brand must be 4 chars long.", nameof(majorBrand));

			return new FtypBox(new BoxHeader(16, "ftyp"), majorBrand, majorVersion);
		}

		private FtypBox(BoxHeader header, string majorBrand, int majorVersion) : base(header, null)
		{
			MajorBrand = majorBrand;
			MajorVersion = majorVersion;
		}

		public FtypBox(Stream file, BoxHeader header) : base(header, null)
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
