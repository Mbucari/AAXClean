using System.Threading;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameTransformBase<TInput, TOutput> : FrameFilterBase<TInput>
	{
		private FrameFilterBase<TOutput>? Linked;

		public override void SetCancellationToken(CancellationToken cancellationToken)
		{
			base.SetCancellationToken(cancellationToken);
			Linked?.SetCancellationToken(cancellationToken);
		}

		public void LinkTo(FrameFilterBase<TOutput> nextFilter) => Linked = nextFilter;
		public abstract TOutput PerformFiltering(TInput input);
		protected virtual TOutput? PerformFinalFiltering() => default;

		protected sealed override async Task FlushAsync()
		{
			if (PerformFinalFiltering() is TOutput filteredData && Linked is not null)
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
			await (Linked?.CompleteAsync() ?? Task.CompletedTask);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				Linked?.Dispose();
			base.Dispose(disposing);
		}
	}
}
