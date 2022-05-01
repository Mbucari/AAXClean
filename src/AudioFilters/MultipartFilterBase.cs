using System;
using System.Collections.Generic;

namespace AAXClean.AudioFilters
{
	public abstract class MultipartFilterBase : AudioFilterBase
	{
#pragma warning disable IDE0051 // Remove unused private members
		private new bool Closed { get; set; }
#pragma warning restore IDE0051 // Remove unused private members
		protected abstract Action<NewSplitCallback> NewFileCallback { get; }

		private readonly int SampleRate;
		private readonly IEnumerator<Chapter> SplitChapters;
		private uint StartFrame;
		private long EndFrame = -1;
		private long LastChunkIndex = -1;

		private const int AAC_TIME_DOMAIN_SAMPLES = 1024;
		private static readonly int[] asc_samplerates = { 96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350 };

		public MultipartFilterBase(byte[] audioSpecificConfig, ChapterInfo splitChapters)
		{
			if (splitChapters is null || splitChapters.Count == 0)
				throw new ArgumentException($"{nameof(splitChapters)} must contain at least one chapter.");

			SampleRate = asc_samplerates[(audioSpecificConfig[0] & 7) << 1 | audioSpecificConfig[1] >> 7];
			SplitChapters = splitChapters.GetEnumerator();
		}

		protected abstract void CloseCurrentWriter();
		protected abstract void WriteFrameToFile(Span<byte> audioFrame, bool newChunk);
		protected abstract void CreateNewWriter(NewSplitCallback callback);

		public override void Close()
		{
			CloseCurrentWriter();
		}

		public override bool FilterFrame(uint chunkIndex, uint frameIndex, Span<byte> audioFrame)
		{
			if (frameIndex > EndFrame)
			{
				CloseCurrentWriter();

				if (!GetNextChapter())
					return false;

				NewSplitCallback callback = new NewSplitCallback { Chapter = SplitChapters.Current };
				CreateNewWriter(callback);
				WriteFrameToFile(audioFrame, true);
				LastChunkIndex = chunkIndex;
			}
			else if (frameIndex >= StartFrame)
			{
				bool newChunk = false;
				if (chunkIndex > LastChunkIndex)
				{
					newChunk = true;
					LastChunkIndex = chunkIndex;
				}
				WriteFrameToFile(audioFrame, newChunk);
			}

			return true;
		}

		private bool GetNextChapter()
		{
			if (!SplitChapters.MoveNext())
				return false;

			StartFrame = (uint)(SplitChapters.Current.StartOffset.TotalSeconds * SampleRate / AAC_TIME_DOMAIN_SAMPLES);
			EndFrame = (uint)(SplitChapters.Current.EndOffset.TotalSeconds * SampleRate / AAC_TIME_DOMAIN_SAMPLES);
			return true;
		}

		protected override void Dispose(bool disposing)
		{
			SplitChapters?.Dispose();
			base.Dispose(disposing);
		}
	}
}
