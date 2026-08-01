using Mpeg4Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Audio
{
	public abstract class MultipartFilterBase<TInput, TCallback> : FrameFinalBase<TInput>
		where TInput : FrameEntry
		where TCallback : INewSplitCallback<TCallback>
	{
		protected readonly bool InputStereo;
		protected readonly SampleRate InputSampleRate;

		private readonly IEnumerator<Chapter> splitChapters;
		private long startSample;
		private long endSample = -1;
		private long lastChunkIndex = -1;
		private long currentSample;
		private bool writerOpen;

		private readonly SyncPrerollQueue prerollQueue = new();

		/// <summary>
		/// When true, each new part begins at the most recent sync frame at or before the
		/// chapter boundary instead of at the boundary frame itself, so the part starts with
		/// an independently decodable frame. <see cref="OnPartOpened"/> reports the resulting
		/// presentation offset so the writer can trim playback to the exact chapter window.
		/// </summary>
		protected virtual bool StartPartAtSyncFrame => false;

		/// <summary>Whether this frame is a valid decode entry point. Default: all frames are.</summary>
		protected virtual bool IsSyncFrame(TInput frame) => frame.IsSyncSample ?? true;

		/// <summary>
		/// Called after <see cref="CreateNewWriter"/> with the exact presentation window of the
		/// new part: <paramref name="editMediaTime"/> is the offset (in media timescale units)
		/// from the part's first written frame to the chapter start, and
		/// <paramref name="presentedSamples"/> is the chapter's exact duration in media
		/// timescale units.
		/// </summary>
		protected virtual void OnPartOpened(long editMediaTime, long presentedSamples) { }

		public MultipartFilterBase(ChapterInfo splitChapters, SampleRate inputSampleRate, bool inputStereo, long mediaTimeOffset = 0)
		{
			if (splitChapters is null || splitChapters.Count == 0)
				throw new ArgumentException($"{nameof(splitChapters)} must contain at least one chapter.");

			InputSampleRate = inputSampleRate;
			InputStereo = inputStereo;
			this.mediaTimeOffset = mediaTimeOffset;
			startSample = currentSample = timeToSample(splitChapters.StartOffset);
			this.splitChapters = splitChapters.GetEnumerator();
		}

		protected abstract void CloseCurrentWriter();
		protected abstract void WriteFrameToFile(TInput audioFrame, bool newChunk);
		protected abstract void CreateNewWriter(TCallback callback);

		protected sealed override Task FlushAsync()
		{
			CloseCurrentWriter();
			return Task.CompletedTask;
		}

		protected override Task PerformFilteringAsync(TInput input)
		{
			if (input.Chunk is null)
			{
				//This is the final flushed entry
				WriteFrameToFile(input, false);
				return Task.CompletedTask;
			}

			//Exact media position when the reader provides it; the accumulator otherwise.
			currentSample = input.StartSample ?? currentSample;

			if (currentSample > endSample)
			{
				CloseCurrentWriter();
				writerOpen = false;

				if (!GetNextChapter())
				{
					//No more chapters: nothing past this point is written, and the sentinels
					//keep both the re-fire and the deferred open below permanently false.
					startSample = endSample = long.MaxValue;
				}
			}

			if (!writerOpen)
			{
				//The chapter window may begin after the current frame (the reader dispatches
				//early: sync-frame lookback, or an edit-list input whose window starts
				//mid-media). Open the part only at the first frame that overlaps the window,
				//so its media is the contiguous run from the preroll's sync frame — opening
				//eagerly would write the sync frame, then drop the pre-window frames after
				//it, leaving a hole in the part's bitstream.
				if (currentSample + input.SamplesInFrame > startSample)
				{
					CreateNewWriter(TCallback.Create(splitChapters.Current));
					writerOpen = true;

					//The preroll queue holds the frames since (and including) the most
					//recent sync frame, all of which start at or before the chapter
					//boundary. Starting the part there gives decoders a valid entry
					//point; the current frame follows them.
					var partFrames = new List<(TInput frame, long start)>();
					if (StartPartAtSyncFrame)
						foreach ((FrameEntry frame, long start) in prerollQueue.Frames)
							partFrames.Add(((TInput)frame, start));
					partFrames.Add((input, currentSample));

					OnPartOpened(editMediaTime: Math.Max(0, startSample - partFrames[0].start),
						presentedSamples: endSample - startSample);

					bool first = true;
					foreach ((TInput frame, long _) in partFrames)
					{
						bool newChunk = first || frame.Chunk!.ChunkIndex > lastChunkIndex;
						lastChunkIndex = frame.Chunk!.ChunkIndex;
						WriteFrameToFile(frame, newChunk);
						first = false;
					}
				}
			}
			else if (currentSample >= startSample)
			{
				bool newChunk = input.Chunk.ChunkIndex > lastChunkIndex;
				if (newChunk)
				{
					lastChunkIndex = input.Chunk.ChunkIndex;
				}
				WriteFrameToFile(input, newChunk);
			}

			prerollQueue.Push(input, currentSample, IsSyncFrame(input));

			currentSample += input.SamplesInFrame;

			return Task.CompletedTask;
		}

		private bool GetNextChapter()
		{
			if (!splitChapters.MoveNext())
				return false;

			startSample = timeToSample(splitChapters.Current.StartOffset);
			//Depending on time precision, the final EndFrame may be less than the last audio frame in the source file
			endSample = timeToSample(splitChapters.Current.EndOffset);
			return true;
		}

		//Chapter offsets are presentation times; frame positions are media times. The offset
		//is the input edit list's media_time (0 without one).
		private readonly long mediaTimeOffset;
		private long timeToSample(TimeSpan time) => (long)Math.Round(time.TotalSeconds * (int)InputSampleRate) + mediaTimeOffset;

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				splitChapters?.Dispose();
			base.Dispose(disposing);
		}
	}
}
