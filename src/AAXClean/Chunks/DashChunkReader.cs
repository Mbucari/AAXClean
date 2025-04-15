using AAXClean.Chunks;
using AAXClean.FrameFilters;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Chunks;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace AAXClean;
public class DashChunkReader : IChunkReader
{
    private const uint AAC_SAMPLES_PER_FRAME = 1024;
    public Action<ConversionProgressEventArgs>? OnProggressUpdateDelegate { get; set; }

    private List<TrakBox> Tracks { get; } = new();
    private List<FrameFilterBase<FrameEntry>> FirstFilters { get; } = new();

    private Stream InputStream { get; }
    private TimeSpan StartTime { get; }
    private TimeSpan EndTime { get; }
    private DashFile Dash { get; }


    private uint[]? LastFrameProcessed;
    private uint[]? TimeScales;
    private uint[]? StartFrames;
    private uint[]? EndFrames;

    Dictionary<uint, int> TrackIdToIndex = new();

    public DashChunkReader(DashFile dash, Stream inputStream, TimeSpan startTime, TimeSpan endTime)
    {
        InputStream = inputStream;
        StartTime = startTime;
        EndTime = endTime;
        Dash = dash;
    }

    public void AddTrack(TrakBox trak, FrameFilterBase<FrameEntry> filter)
    {
        if (TrackIdToIndex.ContainsKey(trak.Tkhd.TrackID))
            throw new InvalidOperationException($"A track with ID = {trak.Tkhd.TrackID} has already been added to the chunk reader.");

        TrackIdToIndex.Add(trak.Tkhd.TrackID, TrackIdToIndex.Count);
        Tracks.Add(trak);
        FirstFilters.Add(filter);
    }

    private void Initialize()
    {
        LastFrameProcessed = new uint[Tracks.Count];
        TimeScales = Tracks.Select(t => t.Mdia.Mdhd.Timescale).ToArray();

        StartFrames = TimeScales.Select(ts => (uint)Math.Round(StartTime.TotalSeconds / AAC_SAMPLES_PER_FRAME * ts)).ToArray();
        EndFrames = TimeScales.Select(ts => (uint)Math.Round(Math.Min(EndTime.TotalSeconds / AAC_SAMPLES_PER_FRAME * ts, uint.MaxValue))).ToArray();
    }

    public async Task RunAsync(CancellationTokenSource cancellationSource)
    {
        Initialize();
        if (LastFrameProcessed is null || TimeScales is null || StartFrames is null || EndFrames is null)
            throw new InvalidOperationException($"Must call {nameof(Initialize)}() before calling {nameof(RunAsync)}()");

        //All filters share the came cancellation source.
        foreach (var filter in FirstFilters)
            filter.SetCancellationToken(cancellationSource.Token);

        beginProcess = DateTime.Now;
        nextUpdate = beginProcess;
        try
        {

            foreach (DashTrackChunk tc in EnumerateChunks(LastFrameProcessed))
            {
                Memory<byte> chunkData = new byte[tc.Entry.ChunkSize];
                _ = await InputStream.ReadAsync(chunkData, cancellationSource.Token);
                await DispatchChunk(AAC_SAMPLES_PER_FRAME, tc, chunkData, StartFrames, LastFrameProcessed, TimeScales);
            }
        }
        catch (OperationCanceledException) { }
        catch
        {
            cancellationSource.Cancel();
            throw;
        }
        finally
        {
            OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, EndTime, (EndTime - StartTime) / (DateTime.Now - beginProcess)));

            //Always call CompleteAsync() on all filters so that every
            //FilterLoop gets awaited and any exceptions are thrown.
            await Task.WhenAll(FirstFilters.Select(f => f.CompleteAsync()));
        }

    }

    private DateTime beginProcess;
    private DateTime nextUpdate;
    private static TimeSpan ProcessPosition(int trackNum, uint[] lastFrameProcessed, uint[] timeScales)
        => TimeSpan.FromSeconds(lastFrameProcessed[trackNum] * AAC_SAMPLES_PER_FRAME / timeScales[trackNum]);

    async Task DispatchChunk(uint frameDelta, DashTrackChunk chunk, Memory<byte> chunkdata, uint[] startFrames, uint[] lastFrameProcessed, uint[] timeScales)
    {
        for (int start = 0, f = 0; f < chunk.Entry.FrameSizes.Length; start += chunk.Entry.FrameSizes[f], f++)
        {
            uint frameIndex = chunk.Entry.FirstFrameIndex + (uint)f;

            if (!frameIndex.Between(startFrames[chunk.TrackNum], EndFrames![chunk.TrackNum]))
                continue;

            //Throttle update so it doesn't bog down UI
            if (DateTime.Now > nextUpdate)
            {
                TimeSpan position = ProcessPosition(chunk.TrackNum, lastFrameProcessed, timeScales);
                double speed = (position - StartTime) / (DateTime.Now - beginProcess);
                OnProggressUpdateDelegate?.Invoke(new ConversionProgressEventArgs(StartTime, EndTime, position, speed));

                nextUpdate = DateTime.Now.AddMilliseconds(100);
            }

            await FirstFilters[chunk.TrackNum].AddInputAsync(new DashFrameEntry
            {
                Chunk = chunk.Entry,
                FrameIndex = frameIndex,
                SamplesInFrame = frameDelta,
                FrameData = chunkdata.Slice(start, chunk.Entry.FrameSizes[f]),
                IV = chunk.IVs[f]
            });
        }
    }

    private IEnumerable<DashTrackChunk> EnumerateChunks(uint[] lastChunksProicessed)
    {
        if (TrackIdToIndex.TryGetValue(Dash.FirstMoof.Traf.Tfhd.TrackID, out var index))
        {
            var trackChunk = ValidateMdatSize(Dash.FirstMoof, Dash.FirstMdat);
            lastChunksProicessed[index] += (uint)trackChunk.Entry.FrameSizes.Length;
            yield return trackChunk;
        }

        var headerBlock = new byte[8];

        //TODO: Figure out if there's a way to determine the file size or number of moof/mdat pairs
        while (InputStream.Position < InputStream.Length)
        {
            var nextMoof = BoxFactory.CreateBox(InputStream, null) as MoofBox;
            var nextMdat = BoxFactory.CreateBox(InputStream, null) as MdatBox;

            if (nextMoof is not null && nextMdat is not null && TrackIdToIndex.TryGetValue(nextMoof.Traf.Tfhd.TrackID, out index))
            {
                var trackChunk = ValidateMdatSize(nextMoof, nextMdat);
                lastChunksProicessed[index] += (uint)trackChunk.Entry.FrameSizes.Length;
                yield return trackChunk;
            }
        }
    }

    private DashTrackChunk ValidateMdatSize(MoofBox moofBox, MdatBox mdatBox)
    {
        var chunkDataSize = moofBox.Traf.Trun.Samples.Sum(s => s.SampleSize ?? 0);
        if (chunkDataSize != mdatBox.Header.TotalBoxSize - mdatBox.Header.HeaderSize)
            throw new InvalidDataException("Mdat box size doesn't match sample sizes in track fragment run box");

        var ivs = moofBox.Traf.GetChild<SencBox>().IVs;
        var frameSizes = moofBox.Traf.Trun.Samples.Select(s => s.SampleSize ?? 0).ToArray();

        if (frameSizes.Length != ivs.Length)
            throw new InvalidDataException($"The number if IVs ({ivs.Length}) does not match the number of samples ({frameSizes.Length}) in fragment {moofBox.Mfhd.SequenceNumber}");

        return new DashTrackChunk()
        {
            TrackNum = TrackIdToIndex[moofBox.Traf.Tfhd.TrackID],
            IVs = ivs,
            Entry = new ChunkEntry
            {
                ChunkIndex = (uint)moofBox.Mfhd.SequenceNumber,
                ChunkOffset = InputStream.Position,
                ChunkSize = chunkDataSize,
                FirstFrameIndex = 0,
                FrameSizes = frameSizes
            },
        };
    }
}
