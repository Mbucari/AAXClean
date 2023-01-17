using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameFinalBase<TInput> : FrameFilterBase<TInput>
	{
		protected sealed override Task HandleInputDataAsync(TInput input) => PerformFilteringAsync(input);
		protected abstract Task PerformFilteringAsync(TInput input);
	}
}
