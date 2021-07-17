using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    internal class AacToMp3Filter : ISampleFilter
    {
        
        private const int MAX_BUFFER_SZ = 1024 * 1024;
        private AacDecoder decoder;
        private BlockingCollection<byte[]> waveSampleQueue;
        private LameMP3FileWriter lameMp3Encoder;
        private Task encoderLoopTask;

        public AacToMp3Filter(Stream mp3Output, byte[] audioSpecificConfig, ushort sampleSize, int avgKbps, AppleTags appleTags = null)
        {
            if (sampleSize != AacDecoder.BITS_PER_SAMPLE)
                throw new ArgumentException($"{nameof(AacToMp3Filter)} only supports 16-bit aac streams.");

            decoder = new Aac2Decoder(audioSpecificConfig);

            var waveFormat = new WaveFormat(decoder.SampleRate, sampleSize, decoder.Channels);

            var lameConfig = new LameConfig
            {
                ABRRateKbps = avgKbps,
                Mode = MPEGMode.Mono,
                VBR = VBRMode.ABR,
            };

            lameConfig.ID3 = PopulateTags(appleTags);
            lameMp3Encoder = new LameMP3FileWriter(mp3Output, waveFormat, lameConfig);

            int waveSampleSize = 1024 /* Decoded AAC frame size*/ * waveFormat.BlockAlign;
            int maxCachedSamples = MAX_BUFFER_SZ / waveSampleSize;
            waveSampleQueue = new BlockingCollection<byte[]>(maxCachedSamples);

            encoderLoopTask = new Task(EncoderLoop);
            encoderLoopTask.Start();

        }

        private static ID3TagData PopulateTags(AppleTags appleTags)
        {
            if (appleTags is null) return new();

            var tags = new ID3TagData();

            tags.Album = appleTags.Album;
            tags.AlbumArt = appleTags.Cover;
            tags.AlbumArtist = appleTags.AlbumArtists;
            tags.Comment = appleTags.Comment;
            tags.Genre = appleTags.Generes;
            tags.Title = appleTags.Title;
            tags.Year = appleTags.ReleaseDate;

            return tags;
        }

        private void EncoderLoop()
        {
            try
            {
                while (true)
                {
                    byte[] waveFrame = waveSampleQueue.Take();

                    lameMp3Encoder.Write(waveFrame);
                }
            }
            catch (InvalidOperationException)
            {
                lameMp3Encoder.Close();
            }
        }

        public void FilterSample(uint chunkIndex, uint frameIndex, byte[] aacSample)
        {

            waveSampleQueue.Add(decoder.DecodeBytes(aacSample));
        }

        public void Close()
        {
            waveSampleQueue.CompleteAdding();
            encoderLoopTask.Wait();
        }

        private bool _disposed = false;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Close();
                decoder?.Dispose();
            }

            _disposed = true;
        }

        private class WaveFormat : NAudio.Wave.WaveFormat
        {
            public WaveFormat(int sampleRate, int bitsPerSample, int channels)
            {
                this.sampleRate = sampleRate;
                this.channels = (short)channels;
                this.bitsPerSample = (short)(bitsPerSample);
                blockAlign = (short)(channels * this.bitsPerSample / 8);
                averageBytesPerSecond = blockAlign * sampleRate;
                waveFormatTag = WaveFormatEncoding.Pcm;
            }
        }
    }
}
