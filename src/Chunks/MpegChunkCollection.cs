using AAXClean.Boxes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AAXClean.Chunks
{
    internal sealed class MpegChunkCollection : IEnumerable<TrackChunk>
    {
        private readonly TrackChunkCollection[] trackChunks;

        public MpegChunkCollection(IChunkHandler handler, params IChunkHandler[] handlers)
        {
            trackChunks = new TrackChunkCollection[handlers.Length + 1];
            trackChunks[0] = new TrackChunkCollection(handler);
            for (int i = 0; i < handlers.Length; i++)
                trackChunks[i + 1] = new TrackChunkCollection(handlers[i]);
        }
        public IEnumerator<TrackChunk> GetEnumerator()
        {
            return new MpegChunkEnumerator(trackChunks);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Tracks
        {
            public IEnumerator<ChunkEntry> ChunkEnumerator { get; set; }
            public IChunkHandler Handler { get; set; }
            public bool TrackEnded { get; set; }
        }

        /// <summary>
        /// Enumerates Chunks in one or more <see cref="TrakBox"/> in order of the chunk offset.
        /// </summary>
        private class MpegChunkEnumerator : IEnumerator<TrackChunk>
        {
            private bool ReachedEnd = false;
            private Tracks[] Tracks;
            public MpegChunkEnumerator(TrackChunkCollection[] trackChunks)
            {
                Tracks = new Tracks[trackChunks.Length];

                for (int i = 0; i < trackChunks.Length; i++)
                {
                    Tracks[i] = new Tracks
                    {
                        Handler = trackChunks[i].Handler,
                        ChunkEnumerator = trackChunks[i].GetEnumerator(),
                    };
                    Tracks[i].TrackEnded = !Tracks[i].ChunkEnumerator.MoveNext();
                }
            }
            public TrackChunk Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                for (int i = 0; i < Tracks.Length; i++)
                {
                    Tracks[i].ChunkEnumerator.Dispose();
                    Tracks[i].ChunkEnumerator = null;
                    Tracks[i].Handler = null;
                }
                Tracks = null;
            }

            public bool MoveNext()
            {
                if (ReachedEnd) return false;

                //Find the next chunk offset across all Tracks
                Tracks t = Tracks[0];

                for (int i = 1; i < Tracks.Length; i++)
                {
                    if (Tracks[i].ChunkEnumerator.Current.ChunkOffset < t.ChunkEnumerator.Current.ChunkOffset && !Tracks[i].TrackEnded)
                        t = Tracks[i];
                }

                Current = new TrackChunk
                {
                    Entry = t.ChunkEnumerator.Current,
                    Handler = t.Handler
                };

                t.TrackEnded = !t.ChunkEnumerator.MoveNext();

                bool theend = true;
                for (int i = 0; i < Tracks.Length; i++)
                    theend &= Tracks[i].TrackEnded;

                ReachedEnd = theend;

                return true;
            }

            public void Reset()
            {
                for (int i = 0; i < Tracks.Length; i++)
                    Tracks[i].ChunkEnumerator.Reset();
            }
        }

        /// <summary>
        /// Enumerates Chunks in a <see cref="TrakBox"/>
        /// </summary>
        private class TrackChunkCollection : IEnumerable<ChunkEntry>
        {
            private TrakBox Track => Handler.Track;
            public IChunkHandler Handler { get; }
            public TrackChunkCollection(IChunkHandler handler)
            {
                Handler = handler;
            }
            public IEnumerator<ChunkEntry> GetEnumerator()
            {
                return new TrachChunkEnumerator(Track);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            private class ChunkRemap
            {
                public uint OriginalIndex { get; init; }
                public uint NewIndex { get; init; }
            }

            private class TrachChunkEnumerator : IEnumerator<ChunkEntry>
            {
                private List<ChunkOffsetEntry> ChunkTable;
                private List<StscBox.ChunkEntry> SamplesToChunks;
                private List<int> SampleSizes;
                private readonly uint EntryCount;
                private uint chunkIndex = 0;
                public TrachChunkEnumerator(TrakBox track)
                {
                    SamplesToChunks = track.Mdia.Minf.Stbl.Stsc.Samples;
                    SampleSizes = track.Mdia.Minf.Stbl.Stsz.SampleSizes;

                    if (track.Mdia.Minf.Stbl.Stco is not null)
                    {
                        ChunkTable = track.Mdia.Minf.Stbl.Stco.ChunkOffsets;
                        EntryCount = track.Mdia.Minf.Stbl.Stco.EntryCount;
                    }
                    else
                    {
                        ChunkTable = track.Mdia.Minf.Stbl.Co64.ChunkOffsets;
                        EntryCount = track.Mdia.Minf.Stbl.Co64.EntryCount;
                    }
                }

                public ChunkEntry Current { get; private set; }

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                    ChunkTable = null;
                    SamplesToChunks = null;
                    SampleSizes = null;
                }

                public bool MoveNext()
                {
                    if (chunkIndex >= EntryCount) return false;

                    var cEntry = ChunkTable[(int)chunkIndex];

                    (int totalChunkSize, uint frameIndex, int[] frameSizes) = GetChunkFrames(cEntry.EntryIndex);
                    Current = new ChunkEntry
                    {
                        FirstFrameIndex = frameIndex,
                        FrameSizes = frameSizes,
                        ChunkIndex = cEntry.EntryIndex,
                        ChunkSize = totalChunkSize,
                        ChunkOffset = cEntry.ChunkOffset
                    };
                    chunkIndex++;
                    return true;
                }

                public void Reset()
                {
                    chunkIndex = 0;
                }

                private (int totalChunkSize, uint frameIndex, int[] frameSizes) GetChunkFrames(uint chunkIndex)
                {
                    (uint firstFrame, uint numFrames) = NumSamplesInChunk(chunkIndex);

                    int[] frameSizes = new int[numFrames];
                    int totalChunkSize = 0;

                    for (uint i = 0; i < numFrames; i++)
                    {
                        if (i + firstFrame >= SampleSizes.Count)
                        {
                            //This handels a case whwere the last Stsc entry was not written correctly.
                            int[] correctFrameSizes = new int[i];
                            Array.Copy(frameSizes, 0, correctFrameSizes, 0, i);
                            return (totalChunkSize, firstFrame, correctFrameSizes);
                        }

                        frameSizes[i] = SampleSizes[(int)(i + firstFrame)];
                        totalChunkSize += frameSizes[i];
                    }

                    return (totalChunkSize, firstFrame, frameSizes);
                }

                private (uint firstFrame, uint numFrames) NumSamplesInChunk(uint chunk)
                {
                    //Mp4 uses one-based counting
                    chunk++;

                    uint firstFrame = 0, numFrames = 0;

                    for (int i = 0; i < SamplesToChunks.Count; i++)
                    {
                        if (i + 1 == SamplesToChunks.Count || chunk < SamplesToChunks[i + 1].FirstChunk)
                        {
                            firstFrame += (chunk - SamplesToChunks[i].FirstChunk) * SamplesToChunks[i].SamplesPerChunk;
                            numFrames = SamplesToChunks[i].SamplesPerChunk;
                            break;
                        }
                        firstFrame += (SamplesToChunks[i + 1].FirstChunk - SamplesToChunks[i].FirstChunk) * SamplesToChunks[i].SamplesPerChunk;
                    }

                    return (firstFrame, numFrames);
                }
            }
        }
    }
}
