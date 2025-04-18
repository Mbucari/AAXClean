namespace Mpeg4Lib.Chunks
{
	public class TrackChunk
	{
		public required ChunkEntry Entry { get; init; }
		public required int TrackNum { get; init; }
	}
}
