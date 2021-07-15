using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean
{
    internal static class libLame3
    {
        public enum QualityPreset : int
        {
            LQP_NOPRESET = -1,
            LQP_NORMAL_QUALITY = 0,
            LQP_LOW_QUALITY = 1,
            LQP_HIGH_QUALITY = 2,
            LQP_VOICE_QUALITY = 3,
            LQP_R3MIX = 4,
            LQP_VERYHIGH_QUALITY = 5,
            LQP_STANDARD = 6,
            LQP_FAST_STANDARD = 7,
            LQP_EXTREME = 8,
            LQP_FAST_EXTREME = 9,
            LQP_INSANE = 10,
            LQP_ABR = 11,
            LQP_CBR = 12,
            LQP_MEDIUM = 13,
            LQP_FAST_MEDIUM = 14,
            LQP_PHONE = 1000,
            LQP_SW = 2000,
            LQP_AM = 3000,
            LQP_FM = 4000,
            LQP_VOICE = 5000,
            LQP_RADIO = 6000,
            LQP_TAPE = 7000,
            LQP_HIFI = 8000,
            LQP_CD = 9000,
            LQP_STUDIO = 10000,
        }
        public enum Mp3Mode : int
        {
            BE_MP3_MODE_STEREO = 0,
            BE_MP3_MODE_JSTEREO = 1,
            BE_MP3_MODE_DUALCHANNEL = 2,
            BE_MP3_MODE_MONO = 3,
        }
        public enum VBRMETHOD : int
        {
            VBR_METHOD_NONE = -1,
            VBR_METHOD_DEFAULT = 0,
            VBR_METHOD_OLD = 1,
            VBR_METHOD_NEW = 2,
            VBR_METHOD_MTRH = 3,
            VBR_METHOD_ABR = 4
        }

        [StructLayout(LayoutKind.Sequential, Size =331)]
        public struct LHV1
        {
            public int dwConfig;

            public int dwStructVersion;
            public int dwStructSize;
            /// <summary>
            /// SAMPLERATE OF INPUT FILE
            /// </summary>
            public uint dwSampleRate;
            /// <summary>
            /// DOWNSAMPLERATE, 0=ENCODER DECIDES
            /// </summary>
            public uint dwReSampleRate;
            public Mp3Mode nMode;
            /// <summary>
            /// CBR bitrate, VBR min bitrate
            /// </summary>
            public uint dwBitrate;
            /// <summary>
            /// CBR ignored, VBR Max bitrate
            /// </summary>
            public uint dwMaxBitrate;
            public QualityPreset nPreset;
            /// <summary>
            /// 1=MPEG-1  0=MPEG-2
            /// </summary>
            public uint dwMpegVersion;
            public uint dwPsyModel;
            public uint dwEmphasis;
            /// <summary>
            /// Set Private Bit
            /// </summary>
            public int bPrivate;
            public int bCRC;
            public int bCopyright;
            /// <summary>
            /// Set Copyright Bit
            /// </summary>
            public int bOriginal;
            /// <summary>
            /// WRITE XING VBR HEADER
            /// </summary>
            public int bWriteVBRHeader;
            /// <summary>
            /// USE VBR ENCODING
            /// </summary>
            public int bEnableVBR;
            /// <summary>
            /// VBR QUALITY 0..9
            /// </summary>
            public int nVBRQuality;
            /// <summary>
            /// Use ABR in stead of nVBRQuality
            /// </summary>
            public uint dwVbrAbr_bps;
            /// <summary>
            /// 
            /// </summary>
            public VBRMETHOD nVbrMethod;
            /// <summary>
            /// Disable Bit resorvoir
            /// </summary>
            public int bNoRes;
            /// <summary>
            /// 
            /// </summary>
            public int bStrictIso;
            /// <summary>
            /// 
            /// </summary>
            public ushort nQuality;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BE_VERSION
        {
            public byte byDLLMajorVersion;
            public byte byDLLMinorVersion;
            public byte byMajorVersion;
            public byte byMinorVersion;
            public byte byDay;
            public byte byMonth;
            public short wYear;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127)]
            public byte[] zHomepage;

            public byte byAlphaLevel;
            public byte byBetaLevel;
            public byte byMMXEnabled;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 125)]
            public byte[] btReserved;

        }

        const string libPath = "lame_enc.dll";


        [DllImport(libPath)] public static extern void beVersion(out BE_VERSION version);
        [DllImport(libPath)] public static extern uint beInitStream(ref LHV1 config, out int dwSamples, out int dwMP3Buffer, out IntPtr hbeStream);
        [DllImport(libPath)] public static extern uint beEncodeChunk(IntPtr hbeStream, int dwRead, short[] pWAVBuffer, byte[] pMP3Buffer, out int dwWrite);
    }
}
