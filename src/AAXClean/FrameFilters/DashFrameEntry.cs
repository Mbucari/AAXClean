#nullable enable
namespace AAXClean.FrameFilters;

internal class DashFrameEntry : FrameEntry
{
    public required byte[] IV { get; init; }
}