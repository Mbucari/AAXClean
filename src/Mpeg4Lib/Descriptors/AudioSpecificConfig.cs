using Mpeg4Lib.Util;
using System;
using System.IO;

namespace Mpeg4Lib.Descriptors;

//ISO/IEC 14496-1 (MPEG-4 Systems) Section 1.6 (pp 52)
//Only supports audio object IDs 1,2,3,4,6,7,17,19,20,21,22,23, and 42 (USAC)
//and only parses data through the dependsOnCoreCoder flag in GASpecificConfig (Subpart 4)
public class AudioSpecificConfig : BaseDescriptor
{
    private static readonly int[] ASC_SampleRates = [96000, 88200, 64000, 48000, 44100, 32000, 24000, 22050, 16000, 12000, 11025, 8000, 7350];

    public static readonly int MinSampleRate = ASC_SampleRates[^1];
    public static readonly int MaxSampleRate = ASC_SampleRates[0];

    private static readonly byte[] SupportedObjectTypes = [1, 2, 3, 4, 6, 7, 17, 19, 20, 21, 22, 23, 42];
    private const byte AOT_ESCAPE = 31;

    public override uint RenderSize => base.RenderSize + (uint)AscBlob.Length;
    public byte[] AscBlob
    {
        get => GetAscBlob();
        set => LoadAscBlob(value);
    }

    private BitReader bitReader;
    public AudioSpecificConfig(Stream file) : base(0x5, file)
    {
        var ascBlob = file.ReadBlock(Size);
        LoadAscBlob(ascBlob);
    }

    private void LoadAscBlob(byte[] ascBlob)
    {
        bitReader = new BitReader(ascBlob);
        AudioObjectType = (int)bitReader.Read(5);
        if (AudioObjectType == AOT_ESCAPE)
            AudioObjectType = (int)bitReader.Read(6) + 32;

        if (Array.IndexOf(SupportedObjectTypes, (byte)AudioObjectType) < 0)
            throw new NotSupportedException($"{nameof(AudioObjectType)} of {AudioObjectType} is unsupported");

        var samplingFrequencyIndex = bitReader.Read(4);
        if (samplingFrequencyIndex <= 12)
            SamplingFrequency = ASC_SampleRates[samplingFrequencyIndex];
        else
            throw new NotSupportedException($"Sampling frequency index of {samplingFrequencyIndex} is not supported.");

        ChannelConfiguration = (int)bitReader.Read(4);
        FrameLengthFlag = bitReader.Read(1) != 0;
        DependsOnCoreCoder = bitReader.Read(1) != 0;
    }

    public override void Render(Stream file)
    {
        file.Write(AscBlob);
    }

    private byte[] GetAscBlob()
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(AudioObjectType, 0, nameof(AudioObjectType));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(AudioObjectType, 32 + 63, nameof(AudioObjectType));

        var sampleIndex = Array.IndexOf(ASC_SampleRates, SamplingFrequency);
        if (sampleIndex < 0)
            throw new ArgumentException($"Unsupported SamplingFrequency of {SamplingFrequency}. Supported values are [{string.Join(", ", SamplingFrequency)}]", nameof(SamplingFrequency));

        ArgumentOutOfRangeException.ThrowIfLessThan(ChannelConfiguration, 1, nameof(ChannelConfiguration));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(ChannelConfiguration, 7, nameof(ChannelConfiguration));

        var writer = new BitWriter();
        if (AudioObjectType < AOT_ESCAPE)
            writer.Write((uint)AudioObjectType, 5);
        else
        {
            writer.Write(AOT_ESCAPE, 5);
            writer.Write((uint)AudioObjectType - 32, 6);
        }

        writer.Write((uint)sampleIndex, 4);
        writer.Write((uint)ChannelConfiguration, 4);
        writer.Write(FrameLengthFlag ? 1u : 0, 1);
        writer.Write(DependsOnCoreCoder ? 1u : 0, 1);

        //Copy everything after DependsOnCoreCoder from the original ASC
        var startPos = bitReader.Position;
        bitReader.CopyTo(writer);
        bitReader.Position = startPos;
        return writer.ToByteArray();
    }

    public int AudioObjectType { get; set; }
    public int SamplingFrequency { get; set; }
    public int ChannelConfiguration { get; set; }

    //GASpecificConfig in ISO/IEC 14496-3 Subpart 4 4.4.1 (pp 487)
    public bool FrameLengthFlag { get; set; }
    public bool DependsOnCoreCoder { get; set; }
}
