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
    internal class AacToMp3Filter : IFrameFilter
    {
        
        private const int MAX_BUFFER_SZ = 1024 * 1024;
        private AacDecoder decoder;
        private BlockingCollection<byte[]> waveFrameQueue;
        private LameMP3FileWriter lameMp3Encoder;
        private Task encoderLoopTask;
        private WaveFormat waveFormat;
        private Stream OutputStream;

        public AacToMp3Filter(Stream mp3Output, byte[] audioSpecificConfig, ushort sampleSize, LameConfig lameConfig)
        {
            if (sampleSize != AacDecoder.BITS_PER_SAMPLE)
                throw new ArgumentException($"{nameof(AacToMp3Filter)} only supports 16-bit aac streams.");

            OutputStream = mp3Output;
            decoder = new Aac2Decoder(audioSpecificConfig);

            waveFormat = new WaveFormat(decoder.SampleRate, sampleSize, decoder.Channels);

            lameMp3Encoder = new LameMP3FileWriter(OutputStream, waveFormat, lameConfig);

            int waveFrameSize = 1024 /* Decoded AAC frame size*/ * waveFormat.BlockAlign;
            int maxCachedFrames = MAX_BUFFER_SZ / waveFrameSize;
            waveFrameQueue = new BlockingCollection<byte[]>(maxCachedFrames);

            encoderLoopTask = new Task(EncoderLoop);
            encoderLoopTask.Start();
        }

        public static ID3TagData GetDefaultMp3Tags(AppleTags appleTags)
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
                    byte[] waveFrame = waveFrameQueue.Take();

                    lameMp3Encoder.Write(waveFrame);
                }
            }
            catch (InvalidOperationException)
            {
                lameMp3Encoder.Close();
            }
        }

        public bool FilterFrame(uint chunkIndex, uint frameIndex, byte[] aacFrame)
        {
            waveFrameQueue.Add(decoder.DecodeBytes(aacFrame));
            return true;
        }

        public void Close()
        {
            waveFrameQueue.CompleteAdding();
            encoderLoopTask.Wait();
            lameMp3Encoder.Close();
            OutputStream.Close();
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
