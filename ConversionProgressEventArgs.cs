using System;

namespace AAXClean
{
    public class ConversionProgressEventArgs : EventArgs
    {
        internal ConversionProgressEventArgs(TimeSpan position, double speed)
        {
            ProcessPosition = position;
            ProcessSpeed = speed;
        }
        public TimeSpan ProcessPosition { get; }
        public double ProcessSpeed { get; }
    }
}
