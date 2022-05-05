using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Descriptors
{
	public abstract class BaseDescriptor
	{
		public List<BaseDescriptor> Children { get; } = new List<BaseDescriptor>();

		public virtual uint RenderSize => 1 + (uint)SizeBytes + (uint)Children.Sum(b => b.RenderSize);

		protected long FilePosition { get; }
		public byte TagID { get; }
		public int Size { get; }
		private readonly int SizeBytes;
		public BaseDescriptor(byte tagID, Stream file)
		{
			FilePosition = file.Position - 1;
			TagID = tagID;

			long start = file.Position;
			Size = DecodeSize(file);
			SizeBytes = (int)(file.Position - start);
		}

		public abstract void Render(Stream file);

		public T GetChild<T>() where T : BaseDescriptor
		{
			IEnumerable<T> children = GetChildren<T>();

			return children.Count() switch
			{
				0 => null,
				1 => children.First(),
				_ => throw new Exception($"{GetType().Name} has {children.Count()} children of type {typeof(T)}. Call {nameof(GetChildren)} instead."),
			};
		}

		public IEnumerable<T> GetChildren<T>() where T : BaseDescriptor
		{
			return Children.Where(c => c is T).Cast<T>();
		}

		protected void LoadChildren(Stream file)
		{
			long endPos = FilePosition + 1 /* TagID */ + SizeBytes + Size;

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
			EncodeSize(file, Size, SizeBytes);

			Render(file);

			foreach (BaseDescriptor child in Children)
			{
				child.Save(file);
			}
		}

		//Variable size coding for expandable classes (0x808080xx) defined in ISO/IEC 14496-1 Section 8.3.3 Expandable classes
		private static int DecodeSize(Stream file)
		{
			byte b;
			int sizeOfInstance = 0;

			do
			{
				b = (byte)file.ReadByte();
				sizeOfInstance = (sizeOfInstance << 7) | (b & 0x7f);
			} while (b >> 7 == 1);

			return sizeOfInstance;
		}
		private static void EncodeSize(Stream file, int length, int numBytes)
		{
			for (int i = numBytes - 1; i > 0; i--)
			{
				byte b = (byte)(0x80 | (length >> (i * 7)) & 0x7b);
				file.WriteByte(b);
			}

			file.WriteByte((byte)length);
		}
	}
}
