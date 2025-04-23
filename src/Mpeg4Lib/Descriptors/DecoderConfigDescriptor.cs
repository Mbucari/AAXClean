using Mpeg4Lib.Util;
using System.IO;

namespace Mpeg4Lib.Descriptors;

public class DecoderConfigDescriptor : BaseDescriptor
{
	public byte ObjectTypeIndication { get; }
	private readonly byte[] Blob;
	public uint MaxBitrate { get; set; }
	public uint AverageBitrate { get; set; }
	public AudioSpecificConfig AudioSpecificConfig => GetChildOrThrow<AudioSpecificConfig>();
	public override int InternalSize => base.InternalSize + 13;

	public DecoderConfigDescriptor(Stream file, DescriptorHeader header) : base(file, header)
	{
		ObjectTypeIndication = (byte)file.ReadByte();
		Blob = file.ReadBlock(4);
		MaxBitrate = file.ReadUInt32BE();
		AverageBitrate = file.ReadUInt32BE();

		//Currently only supported child is DecoderSpecificInfo, and the only
		//supported DecoderSpecificInfo is AudioSpecificConfig.
		//Any additional children will be loaded as UnknownDescriptor
		LoadChildren(file);
	}

	private DecoderConfigDescriptor(byte objectTypeIndication, byte[] blob) : base(4)
	{
		ObjectTypeIndication = objectTypeIndication;
		Blob = blob;
	}

	public static DecoderConfigDescriptor CreateAudio()
	{
		var descriptor = new DecoderConfigDescriptor(0x40, [0x15, 0x00, 0x00, 0x00]);
		var asc = AudioSpecificConfig.CreateEmpty();
		descriptor.Children.Add(asc);
		return descriptor;
	}

	public override void Render(Stream file)
	{
		file.WriteByte(ObjectTypeIndication);
		file.Write(Blob);
		file.WriteUInt32BE(MaxBitrate);
		file.WriteUInt32BE(AverageBitrate);
	}
}
