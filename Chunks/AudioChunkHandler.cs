using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AAXClean.Chunks
{
    internal abstract class AudioChunkHandler : IChunkHandler
    {
        public bool Success { get; private set; } = true;
        public double TimeScale { get; }
        public ISampleFilter SampleFilter { get; set; }
        public TrakBox Track { get; }
        public bool InputStreamSeekable { get; }
        public TimeSpan ProcessPosition => FrameToTime(lastFrameProcessed);

        private uint lastFrameProcessed { get; set; }
        private SttsBox.SampleEntry[] Samples { get; }

        public AudioChunkHandler(uint timeScale, TrakBox trak, bool seekable)
        {
            TimeScale = timeScale;
            Track = trak;
            Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
            InputStreamSeekable = seekable;
        }

        public abstract bool PreprocessSample(byte[] audioSample);

        public bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int totalChunkSize, int[] frameSizes)
        {
            for (uint fIndex = 0; fIndex < frameSizes.Length; fIndex++)
            {
                byte[] encBlocks = file.ReadBlock(frameSizes[fIndex]);

                lastFrameProcessed = frameIndex + fIndex;

                if (PreprocessSample(encBlocks))
                    SampleFilter?.FilterSample(chunkIndex, lastFrameProcessed, encBlocks);
                else
                {
                    Success = false;
                    return Success;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the playback timestamp of an audio frame.
        /// </summary>
        private TimeSpan FrameToTime(uint sampleNum)
        {
            double beginDelta = 0;

            foreach (var entry in Samples)
            {
                if (sampleNum > entry.SampleCount)
                {
                    beginDelta += entry.SampleCount * entry.SampleDelta;
                    sampleNum -= entry.SampleCount;
                }
                else
                {
                    beginDelta += sampleNum * entry.SampleDelta;
                    break;
                }
            }

            return TimeSpan.FromSeconds(beginDelta / TimeScale);
        }
    }
}
