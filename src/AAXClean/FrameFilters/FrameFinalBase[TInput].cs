
namespace AAXClean.FrameFilters
{
	public abstract class FrameFinalBase<TInput> : FrameFilterBase<TInput>
	{
		protected sealed override void HandleInputData(TInput input)
			=> PerformFiltering(input);

		protected abstract void PerformFiltering(TInput input);
	}
}
