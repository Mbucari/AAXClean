using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;

namespace AAXClean.Chunks
{
    internal class Mp4AudioChunkHandler : IChunkHandler
    {
        public bool Success { get; private set; } = true;
        public double TimeScale { get; }
        public IFrameFilter FrameFilter { get; set; }
        public TrakBox Track { get; }
        public bool InputStreamSeekable { get; }
        public TimeSpan ProcessPosition => FrameToTime(lastFrameProcessed);

        private uint lastFrameProcessed { get; set; }
        private SttsBox.SampleEntry[] Samples { get; set; }

        public Mp4AudioChunkHandler(uint timeScale, TrakBox trak, bool inputCanSeek)
        {
            TimeScale = timeScale;
            Track = trak;
            Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
            InputStreamSeekable = inputCanSeek;
        }

        protected virtual bool ValidateFrame(byte[] frame) => (AV_RB16(frame) & 0xfff0) != 0xfff0;

        public bool ChunkAvailable(byte[][] andioFrame, uint chunkIndex, uint frameIndex, int totalChunkSize)
        {
            for (uint fIndex = 0; fIndex < andioFrame.Length; fIndex++)
            {
                lastFrameProcessed = frameIndex + fIndex;

                Success = ValidateFrame(andioFrame[fIndex]) && FrameFilter?.FilterFrame(chunkIndex, lastFrameProcessed, andioFrame[fIndex]) == true;

                if (!Success)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the playback timestamp of an audio frame.
        /// </summary>
        internal TimeSpan FrameToTime(uint frameIndex)
        {
            double beginDelta = 0;

            foreach (var entry in Samples)
            {
                if (frameIndex > entry.FrameCount)
                {
                    beginDelta += (ulong)entry.FrameCount * entry.FrameDelta;
                    frameIndex -= entry.FrameCount;
                }
                else
                {
                    beginDelta += (ulong)frameIndex * entry.FrameDelta;
                    break;
                }
            }
            return TimeSpan.FromSeconds(beginDelta / TimeScale);
        }

        //Defined at
        //http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
        private static ushort AV_RB16(byte[] frame)
        {
            return (ushort)(frame[0] << 8 | frame[1]);
        }

        bool disposed = false;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    FrameFilter?.Dispose();
                    Samples = null;
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                disposed = true;
            }
        }
    }
}
