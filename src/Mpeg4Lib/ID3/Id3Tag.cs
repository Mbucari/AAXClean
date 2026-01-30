using System.IO;
using System.Linq;

namespace Mpeg4Lib.ID3;

public class Id3Tag : Frame
{
	public override int Size => Children.Sum(f => f.Size + f.Header.HeaderSize) + EmptyFrame.GetEmptyFrameSize(Version);
	private Id3Header Id3Header { get; }
	public override ushort Version => Id3Header.Version;

	private Id3Tag(Stream file, Id3Header header) : base(header, null!)
	{
		var endPosition = file.Position + Header.Size;
		Id3Header = header;
		if (Id3Header.Flags[1])
			Children.Add(Id3ExtendedHeader.Create(file, this));
		LoadChildren(file, endPosition);
	}

	public void Save(Stream file)
	{
		Save(file, Id3Header.Version);
		new EmptyFrame(this).Save(file, Id3Header.Version);
	}

	public static Id3Tag? Create(Stream file)
	{
		var id3Header = Id3Header.Create(file);
		if (id3Header is null || id3Header.Version is not (0x200 or 0x300 or 0x400))
			return null;

		try
		{
			return new Id3Tag(file, id3Header);
		}
		catch
		{
			return null;
		}
	}

	public void Add(Frame frame) => Children.Add(frame);

	public override void Render(Stream file) { }
}
