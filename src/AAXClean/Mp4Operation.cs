﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean
{
	public class Mp4Operation
	{
		public event EventHandler<ConversionProgressEventArgs>? ConversionProgressUpdate;
		public bool IsCompleted => Continuation?.IsCompleted is true;
		public bool IsFaulted => _readerTask?.IsFaulted is true;
		public bool IsCanceled => _readerTask?.IsCanceled is true;
		public bool IsCompletedSuccessfully => _readerTask?.IsCompletedSuccessfully is true && Continuation?.IsCompletedSuccessfully is true;
		public TimeSpan CurrentProcessPosition => _lastArgs?.ProcessPosition ?? TimeSpan.Zero;
		public double ProcessSpeed => _lastArgs?.ProcessSpeed ?? 0;
		public TaskStatus TaskStatus => _readerTask?.Status ?? TaskStatus.Created;
		public Task OperationTask => Continuation;
		public Mp4File? Mp4File { get; }

		protected virtual Task Continuation => _continuation ?? Task.CompletedTask;

		private readonly CancellationTokenSource _cancellationSource = new();
		private readonly Func<CancellationTokenSource, Task> _startAction;
		private readonly Action<Task>? _continuationAction;
		private ConversionProgressEventArgs? _lastArgs;
		private Task? _continuation;
		private Task? _readerTask;

		internal Mp4Operation(Func<CancellationTokenSource, Task> startAction, Mp4File? mp4File, Action<Task> continuationTask)
			: this(startAction, mp4File)
		{
			_continuationAction = continuationTask;
		}

		protected Mp4Operation(Func<CancellationTokenSource, Task> startAction, Mp4File? mp4File)
		{
			_startAction = startAction;
			Mp4File = mp4File;
		}

		/// <summary>Cancel the operation</summary>
		public Task CancelAsync()
		{
			_cancellationSource.Cancel();
			return Continuation is null ? Task.CompletedTask : Continuation;
		}

		/// <summary>Start the Mp4 operation</summary>
		public void Start()
		{
			if (_readerTask is null)
			{
				_readerTask = Task.Run(() => _startAction(_cancellationSource));
				SetContinuation(_readerTask);
			}
		}

		protected virtual void SetContinuation(Task readerTask)
		{
			_continuation = readerTask.ContinueWith(t =>
			{
				//Call the continuation delegate to cleanup disposables
				_continuationAction?.Invoke(t);
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

		internal void OnProgressUpdate(ConversionProgressEventArgs args)
		{
			_lastArgs = args;
			ConversionProgressUpdate?.Invoke(this, args);
		}

		public static Mp4Operation FromCompleted(Mp4File mp4File)
			=> new Mp4Operation(c => Task.CompletedTask, mp4File, _ => { });
	}
}
