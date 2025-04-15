namespace Mpeg4Lib.Chunks;

public class DashTrackChunk : TrackChunk
{
    public required byte[][] IVs { get; init; }
}
