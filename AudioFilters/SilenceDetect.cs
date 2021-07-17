using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    class SilenceDetect : ISampleFilter
    {

        public List<(TimeSpan, TimeSpan)> Silences { get; }


        private const int BITS_PER_SAMPLE = 16;
        private AacDecoder decoder;
        private BlockingCollection<(uint,short[])> waveSampleQueue;
        private Task encoderLoopTask;

        private short maxAmplitude;
        private long numSamples;
        private int sampleRate;
        private int channels;
        public SilenceDetect(double db, double seconds, byte[] audioSpecificConfig, ushort sampleSize)
        {
            if (BITS_PER_SAMPLE != sampleSize)
                throw new ArgumentException($"{nameof(AacToMp3Filter)} only supports 16-bit aac streams.");

            decoder = new Aac2Decoder(audioSpecificConfig);

            Silences = new List<(TimeSpan, TimeSpan)>();

            maxAmplitude = (short)(Math.Pow(10, db / 20) * 0x7fff);
            numSamples = (long)Math.Round(sampleRate * seconds);

            waveSampleQueue = new BlockingCollection<(uint, short[])>();
            encoderLoopTask = new Task(EncoderLoop);
            encoderLoopTask.Start();
        }

        private void EncoderLoop()
        {
            long lastStart = 0;
            long numConsecutive = 0;
            try
            {
                while (true)
                {
                    (uint frameIndex, short[] waveFrame) = waveSampleQueue.Take();

                    for (int i = 0; i < waveFrame.Length; i += channels)
                    {
                        if (waveFrame[i] > maxAmplitude || (channels == 2 && waveFrame[i + 1] > maxAmplitude))
                        {
                            if (numConsecutive > numSamples)
                            {
                                var start = TimeSpan.FromSeconds((double)lastStart / sampleRate);
                                var end = TimeSpan.FromSeconds((double)(lastStart + numConsecutive) / sampleRate);

                                var duration = (end - start).TotalSeconds;
                                Silences.Add((start, end));
                            }
                            numConsecutive = 0;
                        }
                        else if (numConsecutive == 0)
                        {
                            lastStart = frameIndex;
                            numConsecutive++;
                        }
                        else
                        {
                            numConsecutive++;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                Dispose(true);
            }
        }
        public void FilterSample(uint chunkIndex, uint frameIndex, byte[] aacSample)
        {
            var waveFrame = decoder.DecodeBytes(aacSample);
            short[] waveFrameShort = new short[waveFrame.Length / 2];
            Buffer.BlockCopy(waveFrame, 0, waveFrameShort, 0, waveFrame.Length);
            waveSampleQueue.Add((frameIndex,waveFrameShort));
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
    }
}
