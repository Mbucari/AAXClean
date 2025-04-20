using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Descriptors;

public abstract class BaseDescriptor
{
	public List<BaseDescriptor> Children { get; } = new List<BaseDescriptor>();

	public virtual uint RenderSize => 1 + (uint)originalSizeOfSize + (uint)Children.Sum(b => b.RenderSize);

	protected long FilePosition { get; }
	public byte TagID { get; }
	public int Size { get; }
	//Store the original size of the expandable class size.
	//Use it as a minimum when re-encoding to preserve box sizes.
	private readonly int originalSizeOfSize;
	public BaseDescriptor(byte tagID, Stream file)
	{
		FilePosition = file.Position - 1;
		TagID = tagID;

		long start = file.Position;
		Size = ExpandableClass.DecodeSize(file);
		originalSizeOfSize = (int)(file.Position - start);
	}

	public abstract void Render(Stream file);

	public T? GetChild<T>() where T : BaseDescriptor
	{
		IEnumerable<T> children = GetChildren<T>();

		return children.Count() switch
		{
			0 => null,
			1 => children.First(),
			_ => throw new InvalidOperationException($"{GetType().Name} has {children.Count()} children of type {typeof(T)}. Call {nameof(GetChildren)} instead."),
		};
	}

	public T GetChildOrThrow<T>() where T : BaseDescriptor
	=> GetChild<T>() ?? throw new InvalidDataException($"Descriptor does not contain a child of type {typeof(T)}");

	public IEnumerable<T> GetChildren<T>() where T : BaseDescriptor
	{
		return Children.OfType<T>();
	}

	protected void LoadChildren(Stream file)
	{
		long endPos = FilePosition + 1 /* TagID */ + originalSizeOfSize + Size;

		while (file.Position < endPos)
		{
			BaseDescriptor child = DescriptorFactory.CreateDescriptor(file);

			if (child.Size == 0)
				break;
			Children.Add(child);
		}
	}

	public void Save(Stream file)
	{
		file.WriteByte(TagID);
		var sizeSize = ExpandableClass.GetSizeByteCount(Size, originalSizeOfSize);
		ExpandableClass.EncodeSize(file, (int)RenderSize - sizeSize - 1, originalSizeOfSize);
		Render(file);

		foreach (BaseDescriptor child in Children)
		{
			child.Save(file);
		}
	}
}
