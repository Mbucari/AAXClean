using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AAXClean.FrameFilters.Audio
{
	public abstract class MultipartFilterBase<TInput, TCallback> : FrameFinalBase<TInput>
		where TInput : FrameEntry
		where TCallback : NewSplitCallback, new()
	{
		protected readonly int SampleRate;
		protected readonly int Channels;
		private readonly IEnumerator<Chapter> SplitChapters;
		private uint StartFrame;
		private long EndFrame = -1;
		private long LastChunkIndex = -1;

		private static readonly int[] asc_samplerates = { 96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350 };

		public MultipartFilterBase(byte[] audioSpecificConfig, ChapterInfo splitChapters)
		{
			if (splitChapters is null || splitChapters.Count == 0)
				throw new ArgumentException($"{nameof(splitChapters)} must contain at least one chapter.");

			SampleRate = asc_samplerates[(audioSpecificConfig[0] & 7) << 1 | audioSpecificConfig[1] >> 7];
			Channels = (audioSpecificConfig[1] >> 3) & 7;
			SplitChapters = splitChapters.GetEnumerator();
		}

		protected abstract void CloseCurrentWriter();
		protected abstract void WriteFrameToFile(FrameEntry audioFrame, bool newChunk);
		protected abstract void CreateNewWriter(TCallback callback);
		public override async Task CompleteAsync()
		{
			await base.CompleteAsync();
			CloseCurrentWriter();
		}
		protected override void PerformFiltering(TInput input)
		{
			if (input.FrameIndex > EndFrame)
			{
				CloseCurrentWriter();

				if (GetNextChapter(input.FrameDelta))
				{
					TCallback callback = new() { Chapter = SplitChapters.Current };
					CreateNewWriter(callback);
					WriteFrameToFile(input, true);
					LastChunkIndex = input.Chunk.ChunkIndex;
				}
			}
			else if (input.FrameIndex >= StartFrame)
			{
				bool newChunk = input.Chunk.ChunkIndex > LastChunkIndex;
				if (newChunk)
				{
					LastChunkIndex = input.Chunk.ChunkIndex;
				}
				WriteFrameToFile(input, newChunk);
			}
		}

		private bool GetNextChapter(uint frameDelta)
		{
			if (!SplitChapters.MoveNext())
				return false;

			StartFrame = (uint)(SplitChapters.Current.StartOffset.TotalSeconds * SampleRate / frameDelta);
			//Depending on time precision, the final EndFrame may be less than the last audio frame in the source file
			EndFrame = (uint)(SplitChapters.Current.EndOffset.TotalSeconds * SampleRate / frameDelta);
			return true;
		}

		protected override void Dispose(bool disposing)
		{
			SplitChapters?.Dispose();
			base.Dispose(disposing);
		}
	}
}
