using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace AAXClean.Boxes
{
	internal class StszBox : FullBox
	{
		public override long RenderSize => base.RenderSize + 8 + SampleSizes.Count * 4;
		internal uint SampleSize { get; }
		internal uint SampleCount { get; set; }
		internal List<int> SampleSizes { get; } = new List<int>();

		internal static StszBox CreateBlank(Box parent)
		{
			int size = 8 + 12 /* empty Box size*/;
			BoxHeader header = new BoxHeader((uint)size, "stsz");

			StszBox stszBox = new StszBox(new byte[] { 0, 0, 0, 0 }, header, parent);

			parent.Children.Add(stszBox);
			return stszBox;
		}

		private StszBox(byte[] versionFlags, BoxHeader header, Box parent) : base(versionFlags, header, parent) { }

		internal StszBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			SampleSize = file.ReadUInt32BE();
			SampleCount = file.ReadUInt32BE();

			if (SampleSize > 0) return;
			if (SampleCount > int.MaxValue)
				throw new NotSupportedException($"AAXClean does not support MPEG-4 files with more than {int.MaxValue} samples (frames)");

			for (int i = 0; i < SampleCount; i++)
			{
				int sampleSize = file.ReadInt32BE();
				SampleSizes.Add(sampleSize);
			}
		}
		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE(SampleSize);
			file.WriteUInt32BE((uint)SampleSizes.Count);
			foreach (int sampleSize in SampleSizes)
			{
				file.WriteInt32BE(sampleSize);
			}
		}

		private bool _disposed = false;
		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				SampleSizes.Clear();
			}

			_disposed = true;

			base.Dispose(disposing);
		}
	}
}
