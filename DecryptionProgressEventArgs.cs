using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
