namespace AAXClean.Chunks
{
    internal class ChunkEntry
    {
        public uint ChunkIndex { get; init; }
        public int ChunkSize { get; init; }
        public uint FirstFrameIndex { get; init; }
        public int[] FrameSizes { get; init; }
        public long ChunkOffset { get; init; }
    }
}
