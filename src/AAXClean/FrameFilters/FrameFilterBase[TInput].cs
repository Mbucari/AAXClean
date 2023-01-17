using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public interface IFrameFilter<TInput> : IDisposable
	{
		public void SetCancellationSource(CancellationTokenSource cancellationSource);
		Task CancelAsync();
		Task CompleteAsync();
		Task AddInputAsync(TInput input);		 
	}
	public abstract class FrameFilterBase<TInput> : IFrameFilter<TInput>
	{
		protected bool Disposed { get; private set; }
		protected abstract int InputBufferSize { get; }

		private CancellationTokenSource CancellationTokenSource;
		private readonly Channel<(int numEntries, TInput[] entries)> FilterChannel;
		private Task FilterLoop;
		private TInput[] buffer;
		private int bufferPosition = 0;

		public FrameFilterBase()
		{
			FilterChannel = Channel.CreateBounded<(int, TInput[])>(new BoundedChannelOptions(2) { SingleReader = true, SingleWriter = true });
			buffer = new TInput[InputBufferSize];
		}

		public virtual void SetCancellationSource(CancellationTokenSource cancellationSource) => CancellationTokenSource = cancellationSource;
		protected abstract Task FlushAsync();
		protected abstract Task HandleInputDataAsync(TInput input);

		public async Task AddInputAsync(TInput input)
		{
			try
			{
				FilterLoop ??= Task.Run(Encoder);

				if (CancellationTokenSource.IsCancellationRequested)
					return;

				buffer[bufferPosition++] = input;

				if (bufferPosition == InputBufferSize)
				{
					if (await FilterChannel.Writer.WaitToWriteAsync(CancellationTokenSource.Token))
					{
						await FilterChannel.Writer.WriteAsync((bufferPosition, buffer), CancellationTokenSource.Token);
						bufferPosition = 0;
						buffer = new TInput[InputBufferSize];
					}
				}
			}
			catch (OperationCanceledException) { }
			catch
			{
				Cancel();
				throw;
			}
		}

		private async Task Encoder()
		{
			try
			{
				while(await FilterChannel.Reader.WaitToReadAsync(CancellationTokenSource.Token))
				{
					await foreach (var messages in FilterChannel.Reader.ReadAllAsync(CancellationTokenSource.Token))
					{
						for (int i = 0; i < messages.numEntries; i++)
							await HandleInputDataAsync(messages.entries[i]);
					}
				}
				await FlushAsync();
			}
			catch (OperationCanceledException) { }
			catch
			{
				Cancel();
				throw;
			}
		}

		protected virtual async Task CompleteInternalAsync()
		{
			try
			{
				await FilterChannel.Writer.WriteAsync((bufferPosition, buffer), CancellationTokenSource.Token);
				FilterChannel.Writer.Complete();
			}
			catch(OperationCanceledException) { }
			await FilterLoop;
		}

		private void Cancel() => CancellationTokenSource?.Cancel();
		public Task CompleteAsync() => CompleteInternalAsync();


		public async Task CancelAsync()
		{
			Cancel();
			await FilterLoop;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
			{
				if (FilterLoop?.IsCompleted is false)
					Cancel();
			}
			Disposed = true;
		}
	}
}
