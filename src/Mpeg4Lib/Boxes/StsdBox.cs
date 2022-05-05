using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public class StsdBox : FullBox
	{
		public override long RenderSize => base.RenderSize + 4;
		public uint EntryCount { get; }

		public StsdBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{
			EntryCount = file.ReadUInt32BE();

			HdlrBox hdlr = Parent.Parent.Parent.GetChild<HdlrBox>();

			for (int i = 0; i < EntryCount; i++)
			{
				BoxHeader h = new BoxHeader(file);
				if (hdlr.HandlerType == "soun")
				{
					AudioSampleEntry audioSampleBox = new AudioSampleEntry(file, h, this);
					Children.Add(audioSampleBox);
				}
				else
				{
					UnknownBox unknownSampleEntry = new UnknownBox(file, h, this);
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
