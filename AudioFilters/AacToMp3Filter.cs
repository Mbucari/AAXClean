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
        private const int BITS_PER_SAMPLE = 16;
        private const int MAX_BUFFER_SZ = 1024 * 1024;
        private FaadHandle FaadHandle;
        private BlockingCollection<byte[]> waveSampleQueue;
        private LameMP3FileWriter lameMp3Encoder;
        private Task encoderLoopTask;

        public AacToMp3Filter(Stream mp3Output, byte[] audioSpecificConfig, ushort sampleSize, int avgKbps, AppleTags appleTags = null)
        {
            if (BITS_PER_SAMPLE != sampleSize)
                throw new ArgumentException($"{nameof(AacToMp3Filter)} only supports 16-bit aac streams.");

            FaadHandle = libFaad2.NeAACDecOpen();

            SetAACDecConfig();

            if (libFaad2.NeAACDecInit2(FaadHandle, audioSpecificConfig, audioSpecificConfig.Length, out int sr, out int ch) != 0)
                throw new Exception($"Error initializing {nameof(libFaad2)}");


            var waveFormat = new WaveFormat(sr, BITS_PER_SAMPLE, ch);

            var lameConfig = new NAudio.Lame.LameConfig
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
            var pDecodeBuff = libFaad2.NeAACDecDecode(FaadHandle, out libFaad2.NeAACDecFrameInfo info, aacSample, aacSample.Length);

            if (info.error != 0)
            {
                var error = libFaad2.NeAACDecGetErrorMessage(info.error);
                var message = Marshal.PtrToStringAnsi(error);

                throw new Exception($"{nameof(libFaad2.NeAACDecDecode)} failed with Error #{info.error}: {message}.");
            }
            else if (info.samples > 0)
            {
                byte[] waveSample = new byte[info.samples * BITS_PER_SAMPLE / 8];
                Marshal.Copy(pDecodeBuff, waveSample, 0, waveSample.Length);
                
                waveSampleQueue.Add(waveSample);
            }
        }

        private bool SetAACDecConfig()
        {
            IntPtr pdecoderConfig = libFaad2.NeAACDecGetCurrentConfiguration(FaadHandle);

            var decoderConfig = Marshal.PtrToStructure<libFaad2.NeAACDecConfiguration>(pdecoderConfig);
            decoderConfig.outputFormat = libFaad2.OutputFormat.FAAD_FMT_16BIT;
            Marshal.StructureToPtr(decoderConfig, pdecoderConfig, true);

            return libFaad2.NeAACDecSetConfiguration(FaadHandle, pdecoderConfig) == 0;
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
                FaadHandle?.Dispose();
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
