using System.Collections.Generic;

namespace AAXClean.FrameFilters.Audio
{
	/// <summary>
	/// The frames since (and including) the most recent sync frame, oldest first, with
	/// each frame's exact media start position — so output can begin at an independently
	/// decodable entry point at or before a requested media position. Bounded: sync
	/// frames occur about once per second in every supported codec, and for codecs where
	/// every frame is sync the queue holds exactly one frame.
	/// </summary>
	public sealed class SyncPrerollQueue
	{
		private readonly Queue<(FrameEntry frame, long start)> queue = new();
		private const int MaxFrames = 4096;

		public IReadOnlyCollection<(FrameEntry frame, long start)> Frames => queue;

		/// <summary>Record a processed frame. A sync frame restarts the preroll at itself.</summary>
		public void Push(FrameEntry frame, long startSample, bool isSync)
		{
			if (isSync)
				queue.Clear();
			if (queue.Count == MaxFrames)
				queue.Dequeue();
			queue.Enqueue((frame, startSample));
		}
	}
}
