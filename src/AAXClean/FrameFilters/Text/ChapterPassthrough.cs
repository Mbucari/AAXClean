using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Text
{
	internal class ChapterPassthrough : FrameFinalBase<FrameEntry>
	{
		Thread processThread;
		public Queue<FrameEntry> FoundChapters { get; } = new Queue<FrameEntry>();
		protected override void PerformFiltering(FrameEntry input)
		{
			lock (FoundChapters)
			{
				FoundChapters.Enqueue(input);
			}
		}
		public override async Task StartAsync()
		{
			processThread = new(FinalEncoderLoop);
			processThread.Priority = ThreadPriority.Highest;
			processThread.IsBackground = true;
			processThread.Start();
			await Task.Delay(0);
		}
		protected override async Task WaitForEncoderLoop()
		{
			processThread.Join();
			await Task.Delay(0);
		}
	}
}
