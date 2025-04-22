using System.IO;

namespace Mpeg4Lib.Descriptors;

public class DescriptorHeader
{
	public long FilePosition { get; internal set; }
	public int TotalBoxSize { get; }
	public byte TagID { get; private set; }
	public int HeaderSize { get; private set; }

	public DescriptorHeader(Stream file)
	{
		FilePosition = file.Position;
		TagID = (byte)file.ReadByte();

		long start = file.Position;
		var originalInternalSize = ExpandableClass.DecodeSize(file);
		var originalSizeOfSize = (int)(file.Position - start);
		HeaderSize = originalSizeOfSize + 1;
		TotalBoxSize = HeaderSize + originalInternalSize;
	}

	public DescriptorHeader(byte tagId)
	{
		TagID = tagId;
		HeaderSize = 1;
	}

	/// <summary>
	/// Get the size of the expandable class encoded length field.
	/// Uses the original descriptor's length field size as a minimum
	/// to preserve box sizes when rendering.
	/// </summary>
	/// <returns>The size, in bytes, or the expandable class's "size" field</returns>
	public int GetEncodedSizeLength(int internalSize)
	{
		var minimumEncodeSize = HeaderSize - 1;
		return ExpandableClass.GetSizeByteCount(internalSize, minimumEncodeSize);
	}
}
