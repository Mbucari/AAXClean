using Mpeg4Lib.Descriptors;
using System.IO;

namespace Mpeg4Lib.Boxes;

//This box contains a single Elementary Stream Descriptor (ES_Descriptor)
//https://developer.apple.com/library/archive/documentation/QuickTime/QTFF/QTFFChap3/qtff3.html#//apple_ref/doc/uid/TP40000939-CH205-124774
public class EsdsBox : FullBox
{
	public override long RenderSize => base.RenderSize + ES_Descriptor.RenderSize;
	public ES_Descriptor ES_Descriptor { get; }

	public EsdsBox(Stream file, BoxHeader header, IBox? parent) : base(file, header, parent)
	{
		var descroptor = DescriptorFactory.CreateDescriptor(file);
		ES_Descriptor = descroptor as ES_Descriptor ?? throw new InvalidDataException($"{descroptor.GetType()} is not an {nameof(ES_Descriptor)}");
	}

	private EsdsBox(ES_Descriptor es_Descriptor, IBox parent)
		: base([0, 0, 0, 0], new BoxHeader(8, "esds"), parent)
	{
		ES_Descriptor = es_Descriptor;
	}

	public static EsdsBox CreateEmpty(IBox parent)
	{
		var esds = new EsdsBox(ES_Descriptor.CreateAudio(), parent);
		parent.Children.Add(esds);
		return esds;
	}

	protected override void Render(Stream file)
	{
		base.Render(file);
		ES_Descriptor.Save(file);
	}
}
