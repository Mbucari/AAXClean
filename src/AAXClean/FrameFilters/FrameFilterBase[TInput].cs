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
		protected Task EncoderLoopTask;
		public bool Disposed { get; private set; }
		public IFrameFilterBase Parent { get; set; }
		public Task Completion => CompletionSource.Task;
		protected virtual int BufferSize => 200;

		protected readonly TaskCompletionSource CompletionSource = new();
		protected readonly BlockingCollection<TInput> InputBuffer;
		protected readonly CancellationTokenSource CancellationSource = new();

		public FrameFilterBase()
		{
			InputBuffer = new(BufferSize);
		}

		public abstract Task StartAsync();
		public abstract Task CompleteAsync();
		public abstract Task CancelAsync();
		public abstract Task FaultAsync(Exception ex);

		public void AcceptInput(TInput input)
		{
			try
			{
				InputBuffer.TryAdd(input, -1, CancellationSource.Token);
			}
			catch (OperationCanceledException) { }
		}

		public void Start() => StartAsync().GetAwaiter().GetResult();
		public void Complete() => CompleteAsync().GetAwaiter().GetResult();
		public void Cancel() => CancelAsync().GetAwaiter().GetResult();
		public void Fault(Exception ex) => FaultAsync(ex).GetAwaiter().GetResult();

		protected virtual async Task WaitForEncoderLoop()
		{
			if (EncoderLoopTask is not null)
				await EncoderLoopTask;
		}

		public void Dispose() => Dispose(true);
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
