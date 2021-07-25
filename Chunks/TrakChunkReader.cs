using AAXClean.Boxes;
using AAXClean.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Chunks
{
    class TrakChunkReader
    {
        private Stream InputStream { get; }
        private Queue<(long, ChunkTable)> ChunkQueue { get; }

        /// <summary>
        /// Reads track all chuncks sequentially
        /// </summary>
        /// <param name="file">input mp4 stream from which to read chunks</param>
        /// <param name="handlers">Codec handlers</param>
        public TrakChunkReader(Stream file, params IChunkHandler[] handlers)
        {
            InputStream = file;

            List<(long chunkOffset, ChunkTable)> chunkList = new();

            foreach (var handler in handlers)
            {
                var chunkTable = new ChunkTable(handler.Track.Mdia.Minf.Stbl, handler);

                if (handler.Track.Mdia.Minf.Stbl.Stco is null)
                    chunkList.AddRange(handler.Track.Mdia.Minf.Stbl.Co64.ChunkOffsets.Select(co => (co, chunkTable)));
                else
                {
                    long lastOffset = 0;
                    foreach (var co in handler.Track.Mdia.Minf.Stbl.Stco.ChunkOffsets)
                    {
                        if (co < lastOffset && lastOffset - co > uint.MaxValue / 2)
                        {
                            //Seems some files incorrectly use stco box with offsets > uint.MAXVALUE. This causes
                            //the offsets to wrap around. Unfottnately, somtimes chapters are out of order and
                            //co is supposed to be less than lastOffset (e.g. Altered Carbon). To attempt to detect
                            //this, only assume it's a wrap around if lastOffset - co > uint.MaxValue / 2
                            lastOffset = (1L << 32) + co;
                        }
                        else
                            lastOffset = co;
                        
                        chunkList.Add((lastOffset, chunkTable));

                    }
                }
            }

            chunkList.Sort((o1, o2) => o1.chunkOffset.CompareTo(o2.chunkOffset));

            ChunkQueue = new Queue<(long, ChunkTable)>(chunkList);
        }
        /// <summary>
        /// Reads the next available chunk.
        /// </summary>
        /// <returns></returns>
        public bool NextChunk()
        {
            if (ChunkQueue.Count == 0) return false;

            (long chunkOffset, ChunkTable table) = ChunkQueue.Dequeue();

            if (InputStream.Position == 0x01dcc62c5)
            {

            }

            if (InputStream.Position < chunkOffset)
            {
                //Unknown Track or data type. Read passed it to next known chunk.
                if (table.Handler.InputStreamSeekable)
                    InputStream.Position = chunkOffset;
                else
                    InputStream.ReadBlock((int)(chunkOffset - InputStream.Position));
            }
            else if (InputStream.Position > chunkOffset)
            {
                if (table.Handler.InputStreamSeekable)
                    InputStream.Position = chunkOffset;
                else
                    throw new Exception($"Input stream position 0x{InputStream.Position:X8} is past the chunk offset 0x{chunkOffset:X8} and is not seekable.");
            }

            (int totalChunkSize, uint frameIndex, int[] frameSizes) = table.GetNextChunk();

            return table.Handler.ChunkAvailable(InputStream, table.NextChunkIndex - 1, frameIndex, totalChunkSize, frameSizes);
        }

        private class ChunkTable
        {
            public IChunkHandler Handler { get; }
            public uint NextChunkIndex { get; set; } = 0;
            private int[] SampleSizes { get; }
            private StscBox.ChunkEntry[] ChunkEntries { get; }
            public ChunkTable(StblBox stbl, IChunkHandler handler)
            {
                Handler = handler;

                SampleSizes = stbl.Stsz.SampleSizes.ToArray();

                ChunkEntries = stbl.Stsc.Samples.ToArray();
            }

            public (int totalChunkSize, uint frameIndex, int[] frameSizes) GetNextChunk()
            {
                (uint firstFrame, uint numFrames) = NumSamplesInChunk(NextChunkIndex++);

                int[] frameSizes = new int[numFrames];
                int totalChunkSize = 0;

                for (uint i = 0; i < numFrames; i++)
                {
                    frameSizes[i] = SampleSizes[i + firstFrame];
                    totalChunkSize += frameSizes[i];
                }

                return (totalChunkSize, firstFrame, frameSizes);
            }

            private (uint firstFrame, uint numFrames) NumSamplesInChunk(uint chunk)
            {
                //Mp4 uses one-based counting
                chunk++;

                uint firstFrame = 0, numFrames = 0;

                for (int i = 0; i < ChunkEntries.Length; i++)
                {
                    if (i + 1 == ChunkEntries.Length || chunk < ChunkEntries[i + 1].FirstChunk)
                    {
                        firstFrame += (chunk - ChunkEntries[i].FirstChunk) * ChunkEntries[i].SamplesPerChunk;
                        numFrames = ChunkEntries[i].SamplesPerChunk;
                        break;
                    }
                    firstFrame += (ChunkEntries[i + 1].FirstChunk - ChunkEntries[i].FirstChunk) * ChunkEntries[i].SamplesPerChunk;
                }

                return (firstFrame, numFrames);
            }
        }
    }
}
