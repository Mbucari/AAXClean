using System;

namespace AAXClean
{
	public class ConversionProgressEventArgs : EventArgs
	{
		public TimeSpan ProcessPosition { get; }
		public TimeSpan StartTime { get; }
		public TimeSpan EndTime { get; }
		public double ProcessSpeed { get; }
		public double FractionCompleted { get; }
		internal ConversionProgressEventArgs(TimeSpan startTime, TimeSpan endTime, TimeSpan processPosition, double processSpeed)
		{
			StartTime = startTime;
			EndTime = endTime;
			ProcessPosition = processPosition;
			ProcessSpeed = processSpeed;
			FractionCompleted = (processPosition - startTime) / (endTime - startTime);
		}
	}
}
