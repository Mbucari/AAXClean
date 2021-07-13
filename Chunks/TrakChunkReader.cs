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
        private Queue<(uint, ChunkTable)> ChunkQueue { get; }

        public TrakChunkReader(Stream file, params IChunkHandler[] handlers)
        {
            InputStream = file;

            List<(uint chunkOffset, ChunkTable)> chunkList = new();

            foreach (var handler in handlers)
            {
                var chunkTable = new ChunkTable(handler.Track.Mdia.Minf.Stbl, handler);

                chunkList.AddRange(handler.Track.Mdia.Minf.Stbl.Stco.ChunkOffsets.Select(co => (co, chunkTable)));

                handler.Init();
            }

            chunkList.Sort((o1, o2) => o1.chunkOffset.CompareTo(o2.chunkOffset));

            ChunkQueue = new Queue<(uint, ChunkTable)>(chunkList);
        }

        public bool NextChunk()
        {
            if (ChunkQueue.Count == 0) return false;

            (uint chunkOffset, ChunkTable table) = ChunkQueue.Dequeue();

            if (InputStream.Position < chunkOffset)
            {
                //Unknown Track or data type. Read passed it to next known chunk.
                InputStream.ReadBlock((int)(chunkOffset - InputStream.Position));
            }

            (int totalChunkSize, uint frameIndex, int[] frameSizes) = table.GetNextChunk();

            return table.Handler.ChunkAvailable(InputStream, table.NextChunk - 1, frameIndex, totalChunkSize, frameSizes);
        }

        private class ChunkTable
        {
            public IChunkHandler Handler { get; }
            public uint NextChunk { get; set; } = 0;
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
                (uint firstFrame, uint numFrames) = NumSamplesInChunk(NextChunk++);

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
