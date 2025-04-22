using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public class MdhdBox : HeaderBox
{
	public override long RenderSize => base.RenderSize + 8;
	public uint Timescale { get; set; }
	private string _language;
	public string Language
	{
		get => _language;
		set
		{
			if (value.Length != 3
				|| value[0] < 'a' || value[0] > 'z'
				|| value[1] < 'a' || value[1] > 'z'
				|| value[2] < 'a' || value[2] > 'z')
				throw new ArgumentException("value must be three, lowercase ASCII characters", nameof(Language));
			_language = value;
		}
	}

	public MdhdBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		var blob = file.ReadBlock(4);
		var reader = new BitReader(blob);
		reader.Position = 1;

		char c1 = (char)(reader.Read(5) + 0x60);
		char c2 = (char)(reader.Read(5) + 0x60);
		char c3 = (char)(reader.Read(5) + 0x60);
		_language = new string([c1, c2, c3]);
	}
	protected override void Render(Stream file)
	{
		base.Render(file);
		var writer = new BitWriter();
		writer.Write(0, 1);
		writer.Write((uint)Language[0] - 0x60, 5);
		writer.Write((uint)Language[1] - 0x60, 5);
		writer.Write((uint)Language[2] - 0x60, 5);
		writer.Write(0, 16);
		file.Write(writer.ToByteArray());
	}

	protected override void ReadBeforeDuration(Stream file)
	{
		Timescale = file.ReadUInt32BE();
	}

	protected override void WriteBeforeDuration(Stream file)
	{
		file.WriteUInt32BE(Timescale);
	}
}
