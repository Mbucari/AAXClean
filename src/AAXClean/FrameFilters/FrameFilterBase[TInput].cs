using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters
{
	public abstract class FrameFilterBase<TInput>
	{
		protected abstract int InputBufferSize { get; }

		private CancellationToken CancellationToken;
		private Task filterLoop;
		private TInput[] buffer;
		private int bufferPosition = 0;
		private readonly Channel<(int numEntries, TInput[] entries)> filterChannel;

		public FrameFilterBase()
		{
			filterChannel = Channel.CreateBounded<(int, TInput[])>(new BoundedChannelOptions(2) { SingleReader = true, SingleWriter = true });
			buffer = new TInput[InputBufferSize];
		}

		public virtual void SetCancellationToken(CancellationToken cancellationToken) => CancellationToken = cancellationToken;
		protected abstract Task FlushAsync();
		protected abstract Task HandleInputDataAsync(TInput input);

		public virtual async Task AddInputAsync(TInput input)
		{
			filterLoop ??= Task.Run(Encoder, CancellationToken);

			if (CancellationToken.IsCancellationRequested)
				return;

			buffer[bufferPosition++] = input;

			if (bufferPosition == InputBufferSize)
			{
				if (await filterChannel.Writer.WaitToWriteAsync(CancellationToken))
				{
					await filterChannel.Writer.WriteAsync((bufferPosition, buffer), CancellationToken);
					bufferPosition = 0;
					buffer = new TInput[InputBufferSize];
				}
			}
		}

		private async Task Encoder()
		{
			try
			{
				while (await filterChannel.Reader.WaitToReadAsync(CancellationToken))
				{
					await foreach (var messages in filterChannel.Reader.ReadAllAsync(CancellationToken))
					{
						for (int i = 0; i < messages.numEntries; i++)
							await HandleInputDataAsync(messages.entries[i]);
					}
				}
				await FlushAsync();
			}
			catch (Exception ex)
			{
				filterChannel.Writer.Complete(ex);
				throw;
			}
		}

		protected virtual async Task CompleteInternalAsync()
		{
			try
			{
				await filterChannel.Writer.WriteAsync((bufferPosition, buffer), CancellationToken);
				filterChannel.Writer.Complete();
			}
			catch (OperationCanceledException) { }

			if (filterLoop is not null)
				await filterLoop;
		}

		public Task CompleteAsync() => CompleteInternalAsync();

		#region IDisposable
		protected bool Disposed { get; private set; }
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~FrameFilterBase()
		{
			Dispose(disposing: false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
			{
				if (filterLoop?.IsCompleted is false)
					filterChannel.Writer.TryComplete();
			}
			Disposed = true;
		}
		#endregion
	}
}
