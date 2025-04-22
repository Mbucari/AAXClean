using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Descriptors;

public abstract class BaseDescriptor
{
	public DescriptorHeader Header { get; }
	public List<BaseDescriptor> Children { get; } = new List<BaseDescriptor>();

	public uint RenderSize => 1 + (uint)Header.GetEncodedSizeLength(InternalSize) + (uint)InternalSize;
	public virtual int InternalSize => (int)Children.Sum(c => c.RenderSize);

	public BaseDescriptor(Stream file, DescriptorHeader header)
	{
		Header = header;
	}

	protected BaseDescriptor(byte tagId)
	{
		Header = new DescriptorHeader(tagId);
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
		while (file.Position < Header.FilePosition + Header.TotalBoxSize)
		{
			BaseDescriptor child = DescriptorFactory.CreateDescriptor(file);

			if (child.InternalSize == 0)
				break;
			Children.Add(child);
		}
	}

	public void Save(Stream file)
	{
		file.WriteByte(Header.TagID);
		ExpandableClass.EncodeSize(file, InternalSize, Header.GetEncodedSizeLength(InternalSize));
		Render(file);

		foreach (BaseDescriptor child in Children)
		{
			child.Save(file);
		}
	}
}
