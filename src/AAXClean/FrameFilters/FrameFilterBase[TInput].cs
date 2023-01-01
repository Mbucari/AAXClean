using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public interface IFrameFilterBase : IDisposable
	{
		Task FaultAsync(Exception ex);
	}

	public abstract class FrameFilterBase<TInput> : IFrameFilterBase
	{
		public bool Disposed { get; private set; }
		public IFrameFilterBase Parent { get; set; }
		public Task Completion => CompletionSource.Task;
		protected virtual int BufferSize => 200;

		private readonly TaskCompletionSource CompletionSource = new();
		private readonly CancellationTokenSource CancellationSource = new();
		private readonly BlockingCollection<TInput> InputBuffer;
		private Task EncoderLoopTask;

		public FrameFilterBase()
		{
			InputBuffer = new(BufferSize);
		}

		protected abstract void HandleInputData(TInput input);
		public void Complete() => CompleteAsync().GetAwaiter().GetResult();
		public void Cancel() => CancelAsync().GetAwaiter().GetResult();
		public void Fault(Exception ex) => FaultAsync(ex).GetAwaiter().GetResult();

		public void AddInput(TInput input)
		{
			try
			{
				InputBuffer.TryAdd(input, -1, CancellationSource.Token);
			}
			catch (OperationCanceledException) { }
		}

		public virtual void Start()
		{
			EncoderLoopTask = Task.Run(EncoderLoop);
		}

		public virtual async Task CompleteAsync()
		{
			InputBuffer.CompleteAdding();
			await WaitForEncoderLoop();
			CompletionSource.TrySetResult();
		}

		public virtual async Task CancelAsync()
		{
			CancellationSource.Cancel();
			await WaitForEncoderLoop();
			CompletionSource.TrySetCanceled();
		}

		public async Task FaultAsync(Exception exception)
		{
			CancellationSource.Cancel();
			await WaitForEncoderLoop();
			CompletionSource.TrySetException(exception);
			//Faults propagate up
			if (Parent is not null)
				await Parent.FaultAsync(exception);
		}

		private async Task WaitForEncoderLoop()
		{
			if (EncoderLoopTask is not null)
				await EncoderLoopTask;
		}

		private void EncoderLoop()
		{
			try
			{
				while (InputBuffer.TryTake(out TInput message, -1, CancellationSource.Token))
				{
					HandleInputData(message);
				}
			}
			catch (OperationCanceledException) { }
			catch (Exception ex)
			{
				CancellationSource.Cancel();
				CompletionSource.TrySetException(ex);
				//Faults propagate up
				if (Parent is not null)
					Parent.FaultAsync(ex).Wait();

			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!Completion.IsCompleted)
					Cancel();
				InputBuffer.Dispose();
				CancellationSource.Dispose();
				EncoderLoopTask?.Dispose();
			}
			Disposed = true;
		}
	}
}
