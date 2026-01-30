using System.IO;
using System.Text;

namespace Mpeg4Lib.ID3;

public class UnknownFrame : Frame
{
	public override int Size => Blob.Length;
	public byte[] Blob { get; }
	public UnknownFrame(Stream file, Header header, Frame parent) : base(header, parent)
	{
		Blob = new byte[header.Size];
		file.ReadExactly(Blob);
	}
	public override void Render(Stream file) => file.Write(Blob);
	public string DataText => Blob.Length == 0 ? "" : (Blob[0] == 0 ? Encoding.ASCII : Encoding.Unicode).GetString(Blob, 1, Blob.Length - 1);
}
