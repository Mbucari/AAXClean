using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean
{
	public class Mp4Operation<TOutput> : Mp4Operation
	{
		private readonly Func<Task, TOutput> _continuationFunc;
		private Task<TOutput> _continuation;
		protected override Task Continuation => _continuation;
		public new Task<TOutput> OperationTask => _continuation;
		internal Mp4Operation(Func<CancellationTokenSource, Task> startAction, Mp4File mp4File, Func<Task, TOutput> continuationFunc)
			: base(startAction, mp4File)
		{
			_continuationFunc = continuationFunc;
		}
		protected override void SetContinuation(Task readerTask)
		{
			_continuation = readerTask.ContinueWith(t =>
			{
				if (t.IsFaulted)
				{
					//Call the continuation delegate to cleanup disposables
					_continuationFunc(t);
					throw t.Exception;
				}
				return _continuationFunc(t);
			}, TaskContinuationOptions.ExecuteSynchronously);
		}
		public new TaskAwaiter<TOutput> GetAwaiter()
		{
			Start();
			return _continuation.GetAwaiter();
		}
	}
}
