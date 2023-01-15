using System;
using System.Collections.Generic;

namespace AAXClean.FrameFilters.Audio
{
	public abstract class MultipartFilterBase<TInput, TCallback> : FrameFinalBase<TInput>
		where TInput : FrameEntry
		where TCallback : NewSplitCallback, new()
	{
		protected readonly bool InputStereo;
		protected readonly SampleRate InputSampleRate;

		private readonly IEnumerator<Chapter> SplitChapters;
		private uint StartSample;
		private long EndSample = -1;
		private long LastChunkIndex = -1;
		private long CurrentSample = 0;


		public MultipartFilterBase(ChapterInfo splitChapters, SampleRate inputSampleRate, bool inputStereo)
		{
			if (splitChapters is null || splitChapters.Count == 0)
				throw new ArgumentException($"{nameof(splitChapters)} must contain at least one chapter.");

			InputSampleRate = inputSampleRate;
			InputStereo = inputStereo;
			SplitChapters = splitChapters.GetEnumerator();
		}

		protected abstract void CloseCurrentWriter();
		protected abstract void WriteFrameToFile(TInput audioFrame, bool newChunk);
		protected abstract void CreateNewWriter(TCallback callback);

		protected sealed override void Flush()
		{
			CloseCurrentWriter();
		}
		protected override void PerformFiltering(TInput input)
		{
			if (input.Chunk is null)
			{
				//This is the final flushed entry
				WriteFrameToFile(input, false);
			}
			else if (CurrentSample > EndSample)
			{
				CloseCurrentWriter();

				if (GetNextChapter())
				{
					TCallback callback = new() { Chapter = SplitChapters.Current };
					CreateNewWriter(callback);
					WriteFrameToFile(input, true);
					LastChunkIndex = input.Chunk.ChunkIndex;
				}
			}
			else if (CurrentSample >= StartSample)
			{
				bool newChunk = input.Chunk.ChunkIndex > LastChunkIndex;
				if (newChunk)
				{
					LastChunkIndex = input.Chunk.ChunkIndex;
				}
				WriteFrameToFile(input, newChunk);
			}
			CurrentSample += input.SamplesInFrame;
		}

		private bool GetNextChapter()
		{
			if (!SplitChapters.MoveNext())
				return false;

			StartSample = (uint)Math.Round(SplitChapters.Current.StartOffset.TotalSeconds * (int)InputSampleRate);
			//Depending on time precision, the final EndFrame may be less than the last audio frame in the source file
			EndSample = (uint)Math.Round(SplitChapters.Current.EndOffset.TotalSeconds * (int)InputSampleRate);
			return true;
		}

		protected override void Dispose(bool disposing)
		{
			SplitChapters?.Dispose();
			base.Dispose(disposing);
		}
	}
}
