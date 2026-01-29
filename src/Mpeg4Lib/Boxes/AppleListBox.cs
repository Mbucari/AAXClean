using System;
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

	private AppleListBox(IBox parent) : base(new BoxHeader(8, "ilst"), parent) { }

	public static AppleListBox CreateEmpty(IBox parent)
	{
		var ilist = new AppleListBox(parent);
		parent.Children.Add(ilist);
		return ilist;
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

	public bool RemoveTag(string name)
		=> GetTagBox(name) is { } tag && RemoveTag(tag);

	public bool RemoveFreeformTag(string domain, string name)
		=> GetFreeformTagBox(domain, name) is { } tag && RemoveTag(tag);

	public bool RemoveTag(AppleTagBox tag)
	{
		if (Children.Remove(tag))
		{
			tag.Dispose();
			return true;
		}
		else if (tag is FreeformTagBox ffTag)
		{
			if (ffTag.Mean?.ReverseDnsDomain is { } domain && ffTag.Name?.Name is { } name && GetFreeformTagBox(domain, name) is { } f && Children.Remove(f))
			{
				f.Dispose();
				return true;
			}
		}
		else if (GetTagBox(tag.Header.Type) is { } t && Children.Remove(t))
		{
			t.Dispose();
			return true;
		}
		return false;
	}

	public void EditOrAddTag(string name, string? data)
	{
		EditOrAddTag(name, data is null ? null : Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
	}

	public void EditOrAddTag(string name, byte[]? data)
	{
		EditOrAddTag(name, data, AppleDataType.ContainsData);
	}	

	public void EditOrAddTag<TData>(string name, TData? data) where TData : IAppleData<TData>
	{
		byte[]? bytes;
		if (data is not null)
		{
			bytes = new byte[TData.SizeInBytes];
			data.Write(bytes);
		}
		else
		{
			bytes = null;
		}

		EditOrAddTag(name, bytes, AppleDataType.ContainsData);
	}

	public void EditOrAddTag(string name, byte[]? data, AppleDataType type)
	{
		if (GetTagBox(name) is { } tagBox)
		{
			EditExistingTag(tagBox, data, type);
		}
		else if (data is not null)
		{
			AddTag(name, data, type);
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

	public void EditOrAddFreeformTag(string domain, string name, string? data)
	{
		EditOrAddFreeformTag(domain, name, data is null ? null : Encoding.UTF8.GetBytes(data), AppleDataType.Utf_8);
	}

	public void EditOrAddFreeformTag(string domain, string name, byte[]? data)
	{
		EditOrAddFreeformTag(domain, name, data, AppleDataType.ContainsData);
	}

	public void EditOrAddFreeformTag(string domain, string name, byte[]? data, AppleDataType type)
	{
		if (GetFreeformTagBox(domain, name) is { } tagBox)
		{
			EditExistingTag(tagBox, data, type);
		}
		else if (data is not null)
		{
			AddFreeformTag(domain, name, data, type);
		}
	}

	private void EditExistingTag(AppleTagBox tagBox, byte[]? data, AppleDataType type)
	{
		if (data is null)
		{
			RemoveTag(tagBox);
		}
		else
		{
			AppleDataBox dataBox = tagBox.GetChildOrThrow<AppleDataBox>();
			dataBox.Data = dataBox.DataType == type ? data
				: throw new InvalidDataException($"Existing tag data type {dataBox.DataType} differs from new edited type {type}");
		}
	}

	public string? GetTagString(string name)
		=> GetTagString(GetTagBox(name));

	public string? GetFreeformTagString(string domain, string name)
		=> GetTagString(GetFreeformTagBox(domain, name));

	private static string? GetTagString(AppleTagBox? tagBox)
		=> tagBox?.Data is not { } dataBox ? null
		: dataBox.DataType switch
		{
			AppleDataType.Utf_8 => Encoding.UTF8.GetString(dataBox.Data),
			AppleDataType.Utf_16 => Encoding.Unicode.GetString(dataBox.Data),
			_ => throw new InvalidDataException($"Apple data type {dataBox.DataType} is not a string type"),
		};

	public TData? GetTagData<TData>(string name) where TData : IAppleData<TData>
		=> GetTagBox(name)?.Data is not { } dataBox ? default
		: dataBox.DataType is not AppleDataType.ContainsData ? throw new InvalidDataException($"Apple data type {dataBox.DataType} is not compatible with {nameof(IAppleData<TData>)}")
		: dataBox.Data.Length != TData.SizeInBytes ? throw new InvalidDataException($"Tag data size ({dataBox.Data.Length}) differs from {nameof(IAppleData<TData>)} size ({TData.SizeInBytes})")
		: TData.Create(dataBox.Data);

	public AppleTagBox? GetTagBox(string name)
		=> Tags.FirstOrDefault(t => t.Header.Type == name);

	public AppleTagBox? GetFreeformTagBox(string domain, string name)
		=> Tags.OfType<FreeformTagBox>().FirstOrDefault(t => t.Mean?.ReverseDnsDomain == domain && t.Name?.Name == name);


	[Obsolete("Use GetTagBox().Data.Data instead")]
	public byte[]? GetTagBytes(string name) => GetTagBox(name)?.Data.Data;

	[Obsolete("Use GetFreeformTagBox().Data.Data instead")]
	public byte[]? GetFreeformTagBytes(string domain, string name) => GetFreeformTagBox(domain, name)?.Data.Data;
}
