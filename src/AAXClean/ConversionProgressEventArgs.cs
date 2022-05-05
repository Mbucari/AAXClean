using System;

namespace AAXClean
{
	public class ConversionProgressEventArgs : EventArgs
	{
		public TimeSpan ProcessPosition { get; internal init; }
		public TimeSpan TotalDuration { get; internal init; }
		public double ProcessSpeed { get; internal init; }
	}
}
