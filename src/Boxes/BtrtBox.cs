﻿using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
	internal class BtrtBox : Box
	{
		public override long RenderSize => base.RenderSize + 12;
		public uint BufferSizeDB { get; }
		public uint MaxBitrate { get; }
		public uint AvgBitrate { get; }

		internal BtrtBox(Stream file, BoxHeader header, Box parent) : base(header, parent)
		{
			BufferSizeDB = file.ReadUInt32BE();
			MaxBitrate = file.ReadUInt32BE();
			AvgBitrate = file.ReadUInt32BE();
		}

		public static BtrtBox Create(uint bufferSizeDB, uint maxBitrate, uint avgBitrate, Box parent)
		{
			BoxHeader header = new BoxHeader(20, "btrt");
			BtrtBox btrt = new BtrtBox(bufferSizeDB, maxBitrate, avgBitrate, header, parent);
			parent.Children.Add(btrt);
			return btrt;
		}

		private BtrtBox(uint bufferSizeDB, uint maxBitrate, uint avgBitrate, BoxHeader header, Box parent) : base(header, parent)
		{
			BufferSizeDB = bufferSizeDB;
			MaxBitrate = maxBitrate;
			AvgBitrate = avgBitrate;
		}

		protected override void Render(Stream file)
		{
			file.WriteUInt32BE(BufferSizeDB);
			file.WriteUInt32BE(MaxBitrate);
			file.WriteUInt32BE(AvgBitrate);
		}
	}
}
