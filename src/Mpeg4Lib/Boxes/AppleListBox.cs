using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mpeg4Lib.Boxes;

public class AppleListBox : Box
{
	public AppleListBox(Stream file, BoxHeader header, IBox? parent) : base(header, parent)
	{
		long endPos = Header.FilePosition + Header.TotalBoxSize;

		while (file.Position < endPos)
		{
			BoxHeader tagBoxHeader = new BoxHeader(file);

			AppleTagBox appleTag
				= tagBoxHeader.Type == "----"
				? new FreeformTagBox(file, tagBoxHeader, this)
				: new AppleTagBox(file, tagBoxHeader, this);

			if (appleTag.Header.TotalBoxSize == 0)
				break;

			Children.Add(appleTag);
		}
	}

	public IEnumerable<AppleTagBox> Tags => GetChildren<AppleTagBox>();

	protected override void Render(Stream file) { }

	public void AddTag(string name, string data)
	{
		AppleTagBox.Create(this, name, Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
	}

	public void AddTag(string name, byte[] data, AppleDataType type)
	{
		AppleTagBox.Create(this, name, data, type);
	}

	public void EditOrAddTag(string name, string data)
	{
		EditOrAddTag(name, Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
	}

	public void EditOrAddTag(string name, byte[] data)
	{
		EditOrAddTag(name, data, AppleDataType.ContainsData);
	}

	public void EditOrAddTag(string name, byte[] data, AppleDataType type)
	{
		AppleTagBox? tag = Tags.Where(t => t.Header.Type == name).FirstOrDefault();

		if (tag is null)
		{
			AddTag(name, data, type);
		}
		else if (tag?.Data.DataType == type)
		{
			tag.Data.Data = data;
		}
	}

	public void AddFreeformTag(string domain, string name, string data)
	{
		FreeformTagBox.Create(this, domain, name, Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
	}

	public void AddFreeformTag(string domain, string name, byte[] data, AppleDataType type)
	{
		FreeformTagBox.Create(this, domain, name, data, type);
	}

	public void EditOrAddFreeformTag(string domain, string name, string data)
	{
		EditOrAddFreeformTag(domain, name, Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
	}

	public void EditOrAddFreeformTag(string domain, string name, byte[] data)
	{
		EditOrAddFreeformTag(domain, name, data, AppleDataType.ContainsData);
	}

	public void EditOrAddFreeformTag(string domain, string name, byte[] data, AppleDataType type)
	{
		FreeformTagBox? tag
			= Tags
			.OfType<FreeformTagBox>()
			.Where(t => t.Mean?.ReverseDnsDomain == domain && t.Name?.Name == name)
			.FirstOrDefault();

		if (tag is null)
		{
			AddFreeformTag(domain, name, data, type);
		}
		else if (tag?.Data.DataType == type)
		{
			tag.Data.Data = data;
		}
	}

	public string? GetTagString(string name)
	{
		byte[]? tag = GetTagBytes(name);
		if (tag is null) return null;

		return Encoding.UTF8.GetString(tag);
	}

	public byte[]? GetTagBytes(string name)
		=> Tags
		.Where(t => t.Header.Type == name)
		.FirstOrDefault()
		?.GetChild<AppleDataBox>()
		?.Data;

	public string? GetFreeformTagString(string domain, string name)
	{
		byte[]? tag = GetFreeformTagBytes(domain, name);
		if (tag is null) return null;

		return Encoding.UTF8.GetString(tag);
	}

	public byte[]? GetFreeformTagBytes(string domain, string name)
		=> Tags
		.OfType<FreeformTagBox>()
		.Where(t => t.Mean?.ReverseDnsDomain == domain && t.Name?.Name == name)
		.FirstOrDefault()
		?.GetChild<AppleDataBox>()
		?.Data;
}
