using System;

namespace AAXClean
{
    public class DecryptionProgressEventArgs : EventArgs
    {
        internal DecryptionProgressEventArgs(TimeSpan position, double speed)
        {
            ProcessPosition = position;
            ProcessSpeed = speed;
        }
        public TimeSpan ProcessPosition { get; }
        public double ProcessSpeed { get; }
    }
}
