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

		protected sealed override void HandleInputData(TInput input)
		{
			TOutput filteredData = PerformFiltering(input);
			Linked?.AddInput(filteredData);
		}

		public override void Start()
		{
			base.Start();
			if (Linked is null)
				throw new InvalidOperationException($"Cannot start a {nameof(FrameTransformBase<TInput, TOutput>)} without a linked filter.");
			//Starts propagate down
			Linked.Start();
		}

		public override async Task CompleteAsync()
		{
			await base.CompleteAsync();
			//Completes propagate down
			if (Linked is not null)
				await Linked.CompleteAsync();
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
