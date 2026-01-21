using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Video;

internal class VideoPassthrough : FrameFinalBase<FrameEntry>
{
	protected override int InputBufferSize => 1;
	protected override Task FlushAsync() => Task.CompletedTask;
	protected override Task PerformFilteringAsync(FrameEntry input) => Task.CompletedTask;
	public override Task AddInputAsync(FrameEntry input)
	{
		return Task.CompletedTask;
	}
}
