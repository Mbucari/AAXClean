using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean
{
	public class Mp4Operation
	{
		public event EventHandler<ConversionProgressEventArgs> ConversionProgressUpdate;
		public bool IsRunning => Continuation?.IsCompleted is false;
		public TimeSpan CurrentProcessPosition => _lastArgs?.ProcessPosition ?? TimeSpan.Zero;
		public double ProcessSpeed => _lastArgs?.ProcessSpeed ?? 0;
		public Task OperationTask => Continuation;
		public Mp4File Mp4File { get; }

		protected virtual Task Continuation => _continuation;

		private readonly CancellationTokenSource _cancellationSource = new();
		private readonly Func<CancellationTokenSource, Task> _startAction;
		private readonly Action<Task> _continuationAction;
		private ConversionProgressEventArgs _lastArgs;
		private Task _continuation;
		private Task _readerTask;

		internal Mp4Operation(Func<CancellationTokenSource, Task> startAction, Mp4File mp4File, Action<Task> continuationTask)
			: this(startAction, mp4File)
		{
			_continuationAction = continuationTask;
		}

		protected Mp4Operation(Func<CancellationTokenSource, Task> startAction, Mp4File mp4File)
		{
			_startAction = startAction;
			Mp4File = mp4File;
		}

		/// <summary>
		/// Cancel the operation
		/// </summary>
		public void Cancel() => _cancellationSource.Cancel();

		/// <summary>
		/// Start the Mp4 operation;
		/// </summary>
		public void Start()
		{
			if (_readerTask is null)
			{
				_readerTask = _startAction(_cancellationSource);
				SetContinuation(_readerTask);
			}
		}

		protected virtual void SetContinuation(Task readerTask)
		{
			_continuation = readerTask.ContinueWith(t =>
			{
				//Call the continuation delegate to cleanup disposables
				_continuationAction(t);
				if (t.IsFaulted)
					throw t.Exception;
			},
			TaskContinuationOptions.ExecuteSynchronously);
		}

		public TaskAwaiter GetAwaiter()
		{
			Start();
			return Continuation.GetAwaiter();
		}

		internal void OnProggressUpdate(ConversionProgressEventArgs args)
		{
			_lastArgs = args;
			ConversionProgressUpdate?.Invoke(this, args);
		}

		public static Mp4Operation CompletedOperation => new Mp4Operation(cs => Task.CompletedTask, null, t => { });
	}
}
