using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    abstract class AacDecoder :IDisposable
    {
        protected SafeHandle Handle { get; set; }

        internal const int BITS_PER_SAMPLE = 16;

        protected const int AAC_FRAME_SIZE = 1024 * BITS_PER_SAMPLE / 8;
        public int Channels { get; }
        public int SampleRate { get; }

        private static readonly int[] asc_samplerates = { 96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350 };
        public AacDecoder(byte[] asc)
        {
            SampleRate = asc_samplerates[(asc[0] & 7) << 1 | asc[1] >> 7];

            Channels = (asc[1] >> 3) & 7;

        }
        public abstract byte[] DecodeBytes(byte[] aacFrame);

        private bool _disposed = false;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Handle?.Dispose();
            }

            _disposed = true;
        }
    }
}
