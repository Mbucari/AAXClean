using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Boxes;

public abstract class HeaderBox : FullBox
{
	public override long RenderSize => base.RenderSize + 3 * (RequireVersionOne ? 8 : 4);
	public DateTimeOffset CreationTime { get; set; }
	public DateTimeOffset ModificationTime { get; set; }
	public ulong Duration { get; set; }
	private bool RequireVersionOne => Version == 1 || Duration > uint.MaxValue;

	static readonly DateTimeOffset Datum = new DateTimeOffset(1904, 1, 1, 0, 0, 0, TimeSpan.Zero);

	protected abstract void ReadBeforeDuration(Stream file);
	protected abstract void WriteBeforeDuration(Stream file);
	public HeaderBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		if (Version == 0)
		{
			CreationTime = Datum.AddSeconds(file.ReadUInt32BE());
			ModificationTime = Datum.AddSeconds(file.ReadUInt32BE());
			ReadBeforeDuration(file);
			Duration = file.ReadUInt32BE();
		}
		else
		{
			CreationTime = Datum.AddSeconds(file.ReadUInt64BE());
			ModificationTime = Datum.AddSeconds(file.ReadUInt64BE());
			ReadBeforeDuration(file);
			Duration = file.ReadUInt64BE();
		}
	}
	protected override void Render(Stream file)
	{
		if (RequireVersionOne)
		{
			//Even if we don't need a version 1 box, preserve files which are already version 1
			Version = 1;
			base.Render(file);
			file.WriteUInt64BE((ulong)(CreationTime - Datum).TotalSeconds);
			file.WriteUInt64BE((ulong)(ModificationTime - Datum).TotalSeconds);
			WriteBeforeDuration(file);
			file.WriteUInt64BE(Duration);
		}
		else
		{
			base.Render(file);
			file.WriteUInt32BE((uint)(CreationTime - Datum).TotalSeconds);
			file.WriteUInt32BE((uint)(ModificationTime - Datum).TotalSeconds);
			WriteBeforeDuration(file);
			file.WriteUInt32BE((uint)Duration);
		}
	}
}
