﻿using AAXClean.AudioFilters;
using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AAXClean.Chunks
{
    internal class Mp4AudioChunkHandler : IChunkHandler
    {
        public bool Success { get; private set; } = true;
        public double TimeScale { get; }
        public ISampleFilter SampleFilter { get; set; }
        public TrakBox Track { get; }
        public bool InputStreamSeekable { get; }
        public TimeSpan ProcessPosition => FrameToTime(lastFrameProcessed);

        private uint lastFrameProcessed { get; set; }
        private SttsBox.SampleEntry[] Samples { get; }

        public Mp4AudioChunkHandler(uint timeScale, TrakBox trak, bool inputCanSeek)
        {
            TimeScale = timeScale;
            Track = trak;
            Samples = Track.Mdia.Minf.Stbl.Stts.Samples.ToArray();
            InputStreamSeekable = inputCanSeek;
        }

        public virtual byte[] ReadBlock(Stream file, int size)
        {
            return file.ReadBlock(size);
        }

        public bool ChunkAvailable(Stream file, uint chunkIndex, uint frameIndex, int totalChunkSize, int[] frameSizes)
        {
            for (uint fIndex = 0; fIndex < frameSizes.Length; fIndex++)
            {
                byte[] andioFrame = ReadBlock(file, frameSizes[fIndex]);

                if ((AV_RB16(andioFrame) & 0xfff0) == 0xfff0)
                {
                    Success = false;
                    return Success;
                }

                lastFrameProcessed = frameIndex + fIndex;
                SampleFilter?.FilterSample(chunkIndex, lastFrameProcessed, andioFrame);
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
        //Defined at
        //http://man.hubwiz.com/docset/FFmpeg.docset/Contents/Resources/Documents/api/intreadwrite_8h_source.html
        private static ushort AV_RB16(byte[] frame)
        {
            return (ushort)(frame[0] << 8 | frame[1]);
        }
    }
}