using System.Threading;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameTransformBase<TInput, TOutput> : FrameFilterBase<TInput>
	{
		private FrameFilterBase<TOutput> Linked;
		public override void SetCancellationSource(CancellationTokenSource cancellationSource)
		{
			base.SetCancellationSource(cancellationSource);
			Linked.SetCancellationSource(cancellationSource);
		}
		public void LinkTo(FrameFilterBase<TOutput> nextFilter) => Linked = nextFilter;
		protected abstract TOutput PerformFiltering(TInput input);
		protected virtual TOutput PerformFinalFiltering() => default;
		protected sealed override async Task FlushAsync()
		{
			TOutput filteredData = PerformFinalFiltering();
			if (filteredData != null)
				await Linked.AddInputAsync(filteredData);
		}

		protected sealed override async Task HandleInputDataAsync(TInput input)
		{
			TOutput filteredData = PerformFiltering(input);
#if DEBUG
			//Allow unlinked for testing purposes
			if (Linked is null)
				return;
#endif
			await Linked.AddInputAsync(filteredData);
		}

		protected sealed override async Task CompleteInternalAsync()
		{
			await base.CompleteInternalAsync();
			await Linked.CompleteAsync();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				Linked?.Dispose();
			base.Dispose(disposing);
		}
	}
}
