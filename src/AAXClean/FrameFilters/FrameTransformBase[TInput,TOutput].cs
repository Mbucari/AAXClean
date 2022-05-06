using System;
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

		private void TransformEncoderLoop()
		{
			try
			{
				while (InputBuffer.TryTake(out TInput message, -1, CancellationSource.Token))
				{
					TOutput output = PerformFiltering(message);
					Linked?.AcceptInput(output);
				}
			}
			catch (OperationCanceledException) { }
			catch (Exception ex)
			{
				Task.Run(() => FaultAsync(ex));
			}
		}

		protected abstract TOutput PerformFiltering(TInput input);

		public override async Task StartAsync()
		{
			EncoderLoopTask = Task.Run(TransformEncoderLoop);
			if (Linked is not null)
				await Linked.StartAsync();
		}

		public override async Task CompleteAsync()
		{
			InputBuffer.CompleteAdding();
			await WaitForEncoderLoop();
			if (!CompletionSource.Task.IsCompleted)
				CompletionSource.SetResult();
			//Completes propagate down
			if (Linked is not null)
				await Linked.CompleteAsync();
		}
		public override async Task CancelAsync()
		{
			CancellationSource.Cancel();
			await WaitForEncoderLoop();
			if (!CompletionSource.Task.IsCompleted)
				CompletionSource.SetCanceled();
			//Cancellations propagate down
			if (Linked is not null)
				await Linked.CancelAsync();
		}
		public override async Task FaultAsync(Exception ex)
		{
			CancellationSource.Cancel();
			await WaitForEncoderLoop();
			if (!CompletionSource.Task.IsCompleted)
				CompletionSource.SetException(ex);
			//Faults propagate up
			if (Parent is not null)
				await Parent.FaultAsync(ex);
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
