using System;

namespace AAXClean
{
	public class ConversionProgressEventArgs : EventArgs
	{
		public TimeSpan ProcessPosition { get; }
		public TimeSpan TotalDuration { get; }
		public double ProcessSpeed { get; }
		internal ConversionProgressEventArgs(TimeSpan totalDuration, TimeSpan processPosition, double processSpeed)
		{
			TotalDuration = totalDuration;
			ProcessPosition = processPosition;
			ProcessSpeed = processSpeed;
		}
	}
}
