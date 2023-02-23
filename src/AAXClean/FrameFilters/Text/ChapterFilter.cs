using System;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Text
{
	public class ChapterFilter : FrameFinalBase<FrameEntry>
	{
		public event EventHandler<FrameEntry> ChapterRead;
		protected override int InputBufferSize => 1;
		protected override Task FlushAsync() => Task.CompletedTask;
		protected override Task PerformFilteringAsync(FrameEntry input) => Task.CompletedTask;
		public override Task AddInputAsync(FrameEntry input)
		{
			ChapterRead?.Invoke(this, input);
			return Task.CompletedTask;
		}
	}
}
