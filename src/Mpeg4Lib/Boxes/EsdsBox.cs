using Mpeg4Lib.Descriptors;
using System.IO;

namespace Mpeg4Lib.Boxes
{

	//This box contains a single Elementary Stream Descriptor (ES_Descriptor)
	//https://developer.apple.com/library/archive/documentation/QuickTime/QTFF/QTFFChap3/qtff3.html#//apple_ref/doc/uid/TP40000939-CH205-124774
	public class EsdsBox : FullBox
	{
		public override long RenderSize => base.RenderSize + ES_Descriptor.RenderSize;
		public ES_Descriptor ES_Descriptor { get; }

		public EsdsBox(Stream file, BoxHeader header, Box parent) : base(file, header, parent)
		{

			ES_Descriptor = DescriptorFactory.CreateDescriptor(file) as ES_Descriptor;
		}

		protected override void Render(Stream file)
		{
			base.Render(file);
			ES_Descriptor.Save(file);
		}
	}
}
