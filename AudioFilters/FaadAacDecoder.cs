using System;
using System.Runtime.InteropServices;

namespace AAXClean.AudioFilters
{
    sealed internal class FaadAacDecoder : AacDecoder
    {

        private FaadHandle Handle;
        public FaadAacDecoder(byte[] asc) : base(asc)
        {
            Handle = libFaad2.NeAACDecOpen();

            SetAACDecConfig();

            if (libFaad2.NeAACDecInit2(Handle, asc, asc.Length, out _, out _) != 0)
                throw new Exception($"Error initializing {nameof(libFaad2)}");
        }

        public override byte[] DecodeBytes(byte[] aacFrame)
        {
            IntPtr unmanagedBuff = DecodeUnmanaged(aacFrame);

            if (unmanagedBuff == IntPtr.Zero) return Array.Empty<byte>();

            byte[] waveSample = new byte[AAC_FRAME_SIZE * Channels];
            Marshal.Copy(unmanagedBuff, waveSample, 0, waveSample.Length);
            return waveSample;

        }

        public override short[] DecodeShort(byte[] aacFrame)
        {
            IntPtr unmanagedBuff = DecodeUnmanaged(aacFrame);

            if (unmanagedBuff == IntPtr.Zero) return Array.Empty<short>();

            short[] waveSample = new short[AAC_FRAME_SIZE * Channels / 2];
            Marshal.Copy(unmanagedBuff, waveSample, 0, waveSample.Length);
            return waveSample;
        }

        protected override IntPtr DecodeUnmanaged(byte[] aacFrame)
        {
            var pDecodeBuff = libFaad2.NeAACDecDecode(Handle, out libFaad2.NeAACDecFrameInfo info, aacFrame, aacFrame.Length);

            if (info.error != 0)
            {
                var error = libFaad2.NeAACDecGetErrorMessage(info.error);
                var message = Marshal.PtrToStringAnsi(error);

                throw new Exception($"{nameof(libFaad2.NeAACDecDecode)} failed with Error #{info.error}: {message}.");
            }

            return info.samples == 0 ? IntPtr.Zero : pDecodeBuff;
        }

        private bool SetAACDecConfig()
        {
            IntPtr pdecoderConfig = libFaad2.NeAACDecGetCurrentConfiguration(Handle);

            var decoderConfig = Marshal.PtrToStructure<libFaad2.NeAACDecConfiguration>(pdecoderConfig);
            decoderConfig.outputFormat = libFaad2.OutputFormat.FAAD_FMT_16BIT;
            Marshal.StructureToPtr(decoderConfig, pdecoderConfig, true);

            return libFaad2.NeAACDecSetConfiguration(Handle, pdecoderConfig) == 0;
        }

        public override void Dispose()
        {
            Handle?.Close();
            Handle?.Dispose();
        }

        private class FaadHandle : SafeHandle
        {
            public FaadHandle() : base(IntPtr.Zero, true) { }
            public override bool IsInvalid => !IsClosed && handle != IntPtr.Zero;
            protected override bool ReleaseHandle()
            {
                libFaad2.NeAACDecClose(this);
                return true;
            }
        }

        private static class libFaad2
        {
            const string libPath = "libfaad2_dll.dll";

            #region Enums
            internal enum OutputFormat : byte
            {
                FAAD_FMT_16BIT = 1,
                FAAD_FMT_24BIT = 2,
                FAAD_FMT_32BIT = 3,
                FAAD_FMT_FLOAT = 4,
                FAAD_FMT_DOUBLE = 5
            }

            internal enum ObjectType : byte
            {
                MAIN = 1,
                LC = 2,
                SSR = 3,
                LTP = 4,
                HE_AAC = 5,
                ER_LC = 17,
                ER_LTP = 5,
                LD = 5,
            }

            #endregion

            #region Structures

            [StructLayout(LayoutKind.Sequential)]
            public struct mp4AudioSpecificConfig
            {
                /* Audio Specific Info */
                byte objectTypeIndex;
                byte samplingFrequencyIndex;
                uint samplingFrequency;
                byte channelsConfiguration;

                /* GA Specific Info */
                byte frameLengthFlag;
                byte dependsOnCoreCoder;
                ushort coreCoderDelay;
                byte extensionFlag;
                byte aacSectionDataResilienceFlag;
                byte aacScalefactorDataResilienceFlag;
                byte aacSpectralDataResilienceFlag;
                byte epConfig;

                sbyte sbr_present_flag;
                sbyte forceUpSampling;
                sbyte downSampledSBR;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NeAACDecFrameInfo
            {
                public int bytesconsumed;
                public int samples;
                public byte channels;
                public byte error;
                public int samplerate;
                public byte sbr;
                public ObjectType object_type;
                public byte header_type;
                public byte num_front_channels;
                public byte num_side_channels;
                public byte num_back_channels;
                public byte num_lfe_channels;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
                public byte[] channel_position;
                public byte ps;
            }
            [StructLayout(LayoutKind.Sequential)]
            public struct NeAACDecConfiguration
            {
                public ObjectType defObjectType;
                public uint defSampleRate;
                public OutputFormat outputFormat;
                public bool downMatrix;
                public bool useOldADTSFormat;
                public bool dontUpsample;
            }

            #endregion

            #region Methods

            [DllImport(libPath)]
            public static extern FaadHandle NeAACDecOpen();

            [DllImport(libPath)]
            public static extern void NeAACDecClose(FaadHandle hpDecoder);

            [DllImport(libPath)]
            public static extern byte NeAACDecInit2(FaadHandle hpDecoder, byte[] pBuffer, int SizeOfDecoderSpecificInfo, out int samplerate, out int channels);

            [DllImport(libPath)]
            public static extern IntPtr NeAACDecGetCurrentConfiguration(FaadHandle hpDecoder);

            [DllImport(libPath)]
            public static extern byte NeAACDecSetConfiguration(FaadHandle hpDecoder, IntPtr config);

            [DllImport(libPath)]
            public static extern byte NeAACDecAudioSpecificConfig(byte[] pBuffer, int buffer_size, out mp4AudioSpecificConfig mp4ASC);

            [DllImport(libPath)]
            public static extern IntPtr NeAACDecDecode(FaadHandle hpDecoder, out NeAACDecFrameInfo hInfo, byte[] buffer, int buffer_size);


            [DllImport(libPath, CharSet = CharSet.Auto)]
            public static extern IntPtr NeAACDecGetErrorMessage(byte errcode);

            #endregion
        }
    }
}
