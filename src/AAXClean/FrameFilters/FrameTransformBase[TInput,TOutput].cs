using System;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameTransformBase<TInput, TOutput> : FrameFilterBase<TInput>
	{
		public FrameFilterBase<TOutput> Linked { get; private set; }
		public void LinkTo(FrameFilterBase<TOutput> nextFilter)
		{
			Linked = nextFilter;
			Linked.Parent = this;
		}
		protected abstract TOutput PerformFiltering(TInput input);
		protected virtual TOutput PerformFinalFiltering() => default;
		protected sealed override void Flush()
		{
			TOutput filteredData = PerformFinalFiltering();
			if (filteredData != null)
				Linked?.AddInput(filteredData);
			Linked?.CompleteAsync().Wait();
		}
		protected sealed override void HandleInputData(TInput input)
		{
			TOutput filteredData = PerformFiltering(input);

#if DEBUG
			if (Linked is null)
			{
				//Allow unlined for testing purposes
				if (filteredData is IDisposable dis)
					dis.Dispose();
				return;
			}
#endif
			Linked.AddInput(filteredData);
		}

		public override void Start()
		{
			base.Start();
#if !DEBUG
			if (Linked is null)
				throw new InvalidOperationException($"Cannot start a {nameof(FrameTransformBase<TInput, TOutput>)} without a linked filter.");
#endif
			//Starts propagate down
			Linked?.Start();
		}

		public sealed override async Task CancelAsync()
		{
			await base.CancelAsync();
			//Cancellations propagate down
			if (Linked is not null)
				await Linked.CancelAsync();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				Linked?.Dispose();
			}
		}
	}
}
