using System;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameFinalBase<TInput> : FrameFilterBase<TInput>
	{
		protected void FinalEncoderLoop()
		{
			try
			{
				while (InputBuffer.TryTake(out TInput message, -1, CancellationSource.Token))
				{
					PerformFiltering(message);
				}
			}
			catch (OperationCanceledException) { }
			catch (Exception ex)
			{
				Task.Run(() => FaultAsync(ex));
			}
		}

		protected abstract void PerformFiltering(TInput input);

		public override async Task StartAsync()
		{
			EncoderLoopTask = Task.Run(FinalEncoderLoop);
			await Task.Delay(0);
		}

		public override async Task CompleteAsync()
		{
			InputBuffer.CompleteAdding();
			
			await WaitForEncoderLoop();
			if (!CompletionSource.Task.IsCompleted)
				CompletionSource.SetResult();
		}
		public override async Task CancelAsync()
		{
			CancellationSource.Cancel();
			await WaitForEncoderLoop();
			if (!CompletionSource.Task.IsCompleted)
				CompletionSource.SetCanceled();
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
	}
}
