using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameFilterBase<TInput>
	{
		protected bool Disposed { get; private set; }
		protected abstract int InputBufferSize { get; }

		private CancellationToken CancellationToken;
		private readonly Channel<(int numEntries, TInput[] entries)> FilterChannel;
		private Task FilterLoop;
		private TInput[] buffer;
		private int bufferPosition = 0;

		public FrameFilterBase()
		{
			FilterChannel = Channel.CreateBounded<(int, TInput[])>(new BoundedChannelOptions(2) { SingleReader = true, SingleWriter = true });
			buffer = new TInput[InputBufferSize];
		}

		public virtual void SetCancellationToken(CancellationToken cancellationToken) => CancellationToken = cancellationToken;
		protected abstract Task FlushAsync();
		protected abstract Task HandleInputDataAsync(TInput input);

		public async Task AddInputAsync(TInput input)
		{
			FilterLoop ??= Task.Run(Encoder, CancellationToken);

			if (CancellationToken.IsCancellationRequested)
				return;

			buffer[bufferPosition++] = input;

			if (bufferPosition == InputBufferSize)
			{
				if (await FilterChannel.Writer.WaitToWriteAsync(CancellationToken))
				{
					await FilterChannel.Writer.WriteAsync((bufferPosition, buffer), CancellationToken);
					bufferPosition = 0;
					buffer = new TInput[InputBufferSize];
				}
			}
		}

		private async Task Encoder()
		{
			try
			{
				while (await FilterChannel.Reader.WaitToReadAsync(CancellationToken))
				{
					await foreach (var messages in FilterChannel.Reader.ReadAllAsync(CancellationToken))
					{
						for (int i = 0; i < messages.numEntries; i++)
							await HandleInputDataAsync(messages.entries[i]);
					}
				}
				await FlushAsync();
			}
			catch (Exception ex)
			{
				FilterChannel.Writer.Complete(ex);
				throw;
			}
		}

		protected virtual async Task CompleteInternalAsync()
		{
			try
			{
				await FilterChannel.Writer.WriteAsync((bufferPosition, buffer), CancellationToken);
				FilterChannel.Writer.Complete();
			}
			catch (OperationCanceledException) { }

			if (FilterLoop is not null)
				await FilterLoop;
		}

		public Task CompleteAsync() => CompleteInternalAsync();

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
					FilterChannel.Writer.TryComplete();
			}
			Disposed = true;
		}
	}
}
