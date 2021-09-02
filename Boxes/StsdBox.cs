using AAXClean.Util;
using System.IO;

namespace AAXClean.Boxes
{
	internal class StsdBox : FullBox
	{
		public override long RenderSize => base.RenderSize + 4;
		public uint EntryCount { get; }
		internal StsdBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			EntryCount = file.ReadUInt32BE();

			var hdlr = Parent.Parent.Parent.GetChild<HdlrBox>();

			for (int i = 0; i < EntryCount; i++)
			{
				var h = new BoxHeader(file);
				if (hdlr.HandlerType == "soun")
				{
					var audioSampleBox = new AudioSampleEntry(file, h, this);
					Children.Add(audioSampleBox);
				}
				else
				{
					var unknownSampleEntry = new UnknownBox(file, h, this);
					Children.Add(unknownSampleEntry);
				}
			}
		}

		public AudioSampleEntry AudioSampleEntry => GetChild<AudioSampleEntry>();
		protected override void Render(Stream file)
		{
			base.Render(file);
			file.WriteUInt32BE(EntryCount);
		}
	}
}
