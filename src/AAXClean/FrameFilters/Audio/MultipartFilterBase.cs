using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Audio
{
	public abstract class MultipartFilterBase<TInput, TCallback> : FrameFinalBase<TInput>
		where TInput : FrameEntry
		where TCallback : NewSplitCallback, new()
	{
		protected readonly bool InputStereo;
		protected readonly SampleRate InputSampleRate;

		private readonly IEnumerator<Chapter> splitChapters;
		private long endSample = -1;
		private long lastChunkIndex = -1;
		private long currentSample = 0;

		public MultipartFilterBase(ChapterInfo splitChapters, SampleRate inputSampleRate, bool inputStereo)
		{
			if (splitChapters is null || splitChapters.Count == 0)
				throw new ArgumentException($"{nameof(splitChapters)} must contain at least one chapter.");

			InputSampleRate = inputSampleRate;
			InputStereo = inputStereo;
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
			}
			else if (currentSample > endSample)
			{
				CloseCurrentWriter();

				if (GetNextChapter())
				{
					TCallback callback = new() { Chapter = splitChapters.Current };
					CreateNewWriter(callback);
					WriteFrameToFile(input, true);
					lastChunkIndex = input.Chunk.ChunkIndex;
				}
			}
			else
			{
				bool newChunk = input.Chunk.ChunkIndex > lastChunkIndex;
				if (newChunk)
				{
					lastChunkIndex = input.Chunk.ChunkIndex;
				}
				WriteFrameToFile(input, newChunk);
			}
			currentSample += input.SamplesInFrame;

			return Task.CompletedTask;
		}

		private bool GetNextChapter()
		{
			if (!splitChapters.MoveNext())
				return false;

			//Depending on time precision, the final EndFrame may be less than the last audio frame in the source file
			endSample = (long)Math.Round(splitChapters.Current.EndOffset.TotalSeconds * (int)InputSampleRate);
			return true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
				splitChapters?.Dispose();
			base.Dispose(disposing);
		}
	}
}
