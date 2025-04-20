namespace Mpeg4Lib.Boxes;

public interface IStszBox : IBox
{
	uint SampleSize { get; }
	int SampleCount { get; }
	/// <summary>The largest sample size in the box</summary>
	int MaxSize { get; }
	/// <summary>Sum of all sample sizes in the box</summary>
	long TotalSize { get; }
	int GetSizeAtIndex(int index);
	long SumFirstNSizes(int firstN);

	/// <summary>
	/// Retrieves the frame sizes for <paramref name="numFrames"/> frames starting at <paramref name="firstFrameIndex"/>
	/// </summary>
	/// <param name="firstFrameIndex">The first frame to retrieve the size of</param>
	/// <param name="numFrames">The number of frames to retrieve sizes of</param>
	/// <returns>A tuple containing the size of each frame in an int[] and the total size of all the frames. </returns>
	(int[] frameSizes, int framesSizeTotal) GetFrameSizes(uint firstFrameIndex, uint numFrames);
}
