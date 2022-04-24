using System;

namespace AAXClean.AudioFilters
{
	public abstract class AacDecoder : IDisposable
	{
		public const int BITS_PER_SAMPLE = 16;

		protected const int AAC_FRAME_SIZE = 1024 * BITS_PER_SAMPLE / 8;
		public int Channels { get; }
		public int SampleRate { get; }

		private static readonly int[] asc_samplerates = { 96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350 };
		public AacDecoder(byte[] asc)
		{
			SampleRate = asc_samplerates[(asc[0] & 7) << 1 | asc[1] >> 7];

			Channels = (asc[1] >> 3) & 7;

		}
		public abstract Span<byte> DecodeBytes(Span<byte> aacFrame);
		public abstract Span<short> DecodeShort(Span<byte> aacFrame);
		public abstract void Dispose();
	}
}
