using System;

namespace AAXClean
{
	public class ConversionProgressEventArgs : EventArgs
	{
		public TimeSpan ProcessPosition { get; }
		public TimeSpan TotalDuration { get; }
		public double ProcessSpeed { get; }
		public double FractionCompleted { get; }
		internal ConversionProgressEventArgs(TimeSpan totalDuration, TimeSpan processPosition, double processSpeed)
		{
			TotalDuration = totalDuration;
			ProcessPosition = processPosition;
			ProcessSpeed = processSpeed;
			FractionCompleted = processPosition / totalDuration;
		}
	}
}
