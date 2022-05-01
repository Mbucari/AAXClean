namespace AAXClean.Chunks
{
	internal class ChunkEntry
	{
		/// <summary>
		/// Index of the chunk in <see cref="AAXClean.Boxes.StcoBox.ChunkOffsets"/> or <see cref="AAXClean.Boxes.Co64Box.ChunkOffsets"/>
		/// </summary>
		public uint ChunkIndex { get; init; }
		/// <summary>
		/// File-based offset to start of chunk
		/// </summary>
		public long ChunkOffset { get; init; }
		/// <summary>
		/// Size, in bytes, of all frames (samples) in the chunk
		/// </summary>
		public int ChunkSize { get; init; }
		/// <summary>
		/// Index of the first frame (sample) in the chunk.
		/// </summary>
		public uint FirstFrameIndex { get; init; }
		/// <summary>
		/// Sizes of the frames (samples) in the cunk.  The sume of these sizes is equal to <see cref="ChunkSize"/>
		/// </summary>
		public int[] FrameSizes { get; init; }
	}
}
