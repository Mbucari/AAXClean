using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public interface IFrameFilterBase : IDisposable
	{
		void Fault(Exception ex);
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

		protected abstract void Flush();
		protected abstract void HandleInputData(TInput input);

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

		public async Task CompleteAsync()
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

		public void Fault(Exception exception)
		{
			CancellationSource.Cancel();
			CompletionSource.TrySetException(exception);
			//Faults propagate up
			Parent?.Fault(exception);
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
				while (InputBuffer.TryTake(out var message, -1, CancellationSource.Token))
				{
					HandleInputData(message);
				}
				Flush();
			}
			catch (OperationCanceledException) { }
			catch (Exception ex)
			{
				Fault(ex);
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
					CancelAsync().Wait();
				InputBuffer.Dispose();
				CancellationSource.Dispose();
				EncoderLoopTask?.Dispose();
			}
			Disposed = true;
		}
	}
}
