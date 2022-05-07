using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameTransformBase<TInput, TOutput> : FrameFilterBase<TInput>
	{
		private FrameFilterBase<TOutput> Linked;
		public void LinkTo(FrameFilterBase<TOutput> nextFilter)
		{
			Linked = nextFilter;
			Linked.Parent = this;
		}

		protected override void HandleInputData(TInput input)
		{
			TOutput filteredData = PerformFiltering(input);
			Linked?.AddInput(filteredData);
		}
		protected abstract TOutput PerformFiltering(TInput input);

		public override void Start()
		{
			base.Start();
			//Starts propagate down
			Linked?.Start();
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
