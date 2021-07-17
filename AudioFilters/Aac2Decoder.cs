using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.AudioFilters
{
    class Aac2Decoder : AacDecoder
    {
        private DecoderHandle Handle;
        private int decSz => AAC_FRAME_SIZE * Channels;
        public Aac2Decoder(byte[] asc) : base(asc)
        {
            var ascUnmanaged =  Marshal.AllocHGlobal(asc.Length);

            Handle = aacDecoder_Open(TRANSPORT_TYPE.TT_MP4_RAW, 1);

            Marshal.Copy(asc, 0, ascUnmanaged, asc.Length);
            var err = aacDecoder_ConfigRaw(Handle, ref ascUnmanaged, new int[] { asc.Length });
            Marshal.FreeHGlobal(ascUnmanaged);
        }
        public override byte[] DecodeBytes(byte[] aacFrame)
        {
            IntPtr unmanagedBuff = DecodeUnmanaged(aacFrame);

            byte[] buffer = new byte[decSz];

            Marshal.Copy(unmanagedBuff, buffer, 0, buffer.Length);
            Marshal.FreeHGlobal(unmanagedBuff);

            return buffer;
        }

        public override short[] DecodeShort(byte[] aacFrame)
        {
            IntPtr unmanagedBuff = DecodeUnmanaged(aacFrame);

            short[] buffer = new short[decSz / 2];

            Marshal.Copy(unmanagedBuff, buffer, 0, buffer.Length);
            Marshal.FreeHGlobal(unmanagedBuff);

            return buffer;
        }

        private IntPtr DecodeUnmanaged(byte[] aacFrame)
        {
            int inputSize = aacFrame.Length;
            int bytesValid = inputSize;

            int error = aacDecoder_Fill(Handle, ref aacFrame, ref inputSize, ref bytesValid);
            if (error != 0 || bytesValid != 0)
            {
                throw new Exception($"Error filling decoder buffer. Code {error:X}");
            }

            IntPtr unmanagedBuff = Marshal.AllocHGlobal(decSz);

            error = aacDecoder_DecodeFrame(Handle, unmanagedBuff, decSz / 2, DecoderFlags.NONE);

            if (error != 0)
            {
                throw new Exception($"Error decoding AAC frame. Code {error:X}");
            }

            return unmanagedBuff;
        }
        public override void Dispose()
        {
            Handle?.Close();
            Handle?.Dispose();
        }

        private class DecoderHandle : SafeHandle
        {
            public DecoderHandle() : base(IntPtr.Zero, true) { }
            public override bool IsInvalid => !IsClosed && handle != IntPtr.Zero;
            protected override bool ReleaseHandle()
            {
                aacDecoder_Close(this);
                return true;
            }
        }
        private const string libName = "aac2.dll";

        [DllImport(libName)]
        private static extern DecoderHandle aacDecoder_Open(TRANSPORT_TYPE tt, int nrOfLayers);

        [DllImport(libName)]
        private static extern int aacDecoder_Close(DecoderHandle self);

        [DllImport(libName)]
        private static extern int aacDecoder_ConfigRaw(DecoderHandle self, ref IntPtr ASC, int[] ASCSize);

        [DllImport(libName)]
        private static extern int aacDecoder_Fill(DecoderHandle self, ref byte[] buffer, ref int bufferSize, ref int bytesValid);

        [DllImport(libName)]
        private static extern int aacDecoder_DecodeFrame(DecoderHandle self, IntPtr buffer, int bufferSize, DecoderFlags bytesValid);

        private enum TRANSPORT_TYPE : int
        {
            /// <summary>
            /// Unknown format.
            /// </summary>
            TT_UNKNOWN = -1,
            /// <summary>
            /// "as is" access units (packet based since there is obviously no sync layer)
            /// </summary>
            TT_MP4_RAW = 0,
            /// <summary>
            /// ADIF bitstream format.
            /// </summary>
            TT_MP4_ADIF = 1,
            /// <summary>
            /// ADTS bitstream format.
            /// </summary>
            TT_MP4_ADTS = 2,
            /// <summary>
            /// Audio Mux Elements with muxConfigPresent = 1
            /// </summary>
            TT_MP4_LATM_MCP1 = 6,
            /// <summary>
            /// Audio Mux Elements with muxConfigPresent = 0, out of band StreamMuxConfig 
            /// </summary>
            TT_MP4_LATM_MCP0 = 7,
            /// <summary>
            /// Audio Sync Stream.
            /// </summary>
            TT_MP4_LOAS = 10,
            /// <summary>
            /// Digital Radio Mondial (DRM30/DRM+) bitstream format.
            /// </summary>
            TT_DRM = 12
        }
        [Flags]
        private enum DecoderFlags : int
        {
            NONE = 0,
            /// <summary>
            /// Flag for aacDecoder_DecodeFrame(): Trigger the built-in error concealment
            /// module to generate a substitute signal for one lost frame. New input data
            /// will not be considered.
            /// </summary>
            AACDEC_CONCEAL = 1,
            /// <summary>
            /// Flag for aacDecoder_DecodeFrame(): Flush all filterbanks to get all delayed
            /// audio without having new input data. Thus new input data will not be
            /// considered.
            /// </summary>
            AACDEC_FLUSH = 2,
            /// <summary>
            /// Flag for aacDecoder_DecodeFrame(): Signal an input bit stream data
            /// discontinuity. Resync any internals as necessary.
            /// </summary>
            AACDEC_INTR = 4,
            /// <summary>
            /// Flag for aacDecoder_DecodeFrame(): Clear all signal delay lines and history
            /// buffers. CAUTION: This can cause discontinuities in the output signal.
            /// </summary>
            AACDEC_CLRHIST = 8
        }
    }
}
