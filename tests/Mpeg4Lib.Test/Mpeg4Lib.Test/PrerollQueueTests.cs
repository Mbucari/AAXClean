using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;

namespace Mpeg4Lib.Test;

[TestClass]
public class PrerollQueueTests
{
	private static FrameEntry Frame() => new() { SamplesInFrame = 1024, FrameData = new byte[1] };

	[TestMethod]
	public void Queue_HoldsFramesSinceMostRecentSync_InclusiveOldestFirst()
	{
		SyncPrerollQueue q = new();
		FrameEntry sync1 = Frame(), dep1 = Frame(), sync2 = Frame(), dep2 = Frame();
		q.Push(sync1, 0, isSync: true);
		q.Push(dep1, 1024, isSync: false);
		q.Push(sync2, 2048, isSync: true);   //restarts the preroll at itself
		q.Push(dep2, 3072, isSync: false);
		CollectionAssert.AreEqual(new[] { sync2, dep2 }, q.Frames.Select(f => f.frame).ToArray());
		Assert.AreEqual(2048L, q.Frames.First().start);
	}

	[TestMethod]
	public void Queue_AllSyncCodec_HoldsExactlyTheLastFrame()
	{
		SyncPrerollQueue q = new();
		for (int i = 0; i < 5; i++)
			q.Push(Frame(), i * 1024, isSync: true);
		Assert.HasCount(1, q.Frames);
		Assert.AreEqual(4096L, q.Frames.First().start);
	}

	[TestMethod]
	public void Queue_IsBounded()
	{
		SyncPrerollQueue q = new();
		q.Push(Frame(), 0, isSync: true);
		for (int i = 1; i <= 5000; i++)
			q.Push(Frame(), i * 1024L, isSync: false);
		Assert.HasCount(4096, q.Frames);
	}
}
