namespace Mpeg4Lib.Chunks;

public class ChunkEntry
{
	/// <summary>
	/// The track's header ID number
	/// </summary>
	public required uint TrackId { get; init; }
	/// <summary>
	/// Index of the chunk in <see cref="Boxes.StcoBox.ChunkOffsets"/> or <see cref="Boxes.Co64Box.ChunkOffsets"/>
	/// </summary>
	public required uint ChunkIndex { get; init; }
	/// <summary>
	/// File-based offset to start of chunk
	/// </summary>
	public required long ChunkOffset { get; init; }
	/// <summary>
	/// Size, in bytes, of all frames in the chunk
	/// </summary>
	public required long FirstSample { get; init; }
	/// <summary>
	/// Sizes of the frames in the chunk.  The sum of these sizes is equal to <see cref="ChunkSize"/>
	/// </summary>
	public required int ChunkSize { get; init; }
	/// <summary>
	/// The first sample in the chunk, counting from the beginning of the track.
	/// </summary>
	public required int[] FrameSizes { get; init; }
	/// <summary>
	/// The number of samples in each frame.
	/// </summary>
	public required uint[] FrameDurations { get; init; }
	public object? ExtraData { get; init; }
}
