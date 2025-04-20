using System.IO;

namespace Mpeg4Lib.Boxes;

public class StblBox : Box
{
	public StblBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		LoadChildren(file);
	}
	public StsdBox Stsd => GetChildOrThrow<StsdBox>();
	public SttsBox Stts => GetChildOrThrow<SttsBox>();
	public IChunkOffsets COBox => GetChild<StcoBox>() ?? (IChunkOffsets)GetChildOrThrow<Co64Box>();
	public IStszBox? Stsz => GetChild<StszBox>() ?? (GetChild<Stz2Box>() as IStszBox);
	public StscBox Stsc => GetChildOrThrow<StscBox>();

	protected override void Render(Stream file)
	{
		return;
	}
}