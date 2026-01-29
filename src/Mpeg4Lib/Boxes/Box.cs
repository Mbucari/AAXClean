using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	public abstract class Box : IBox
	{
		public IBox? Parent { get; }
		public BoxHeader Header { get; }
		public List<IBox> Children { get; } = new List<IBox>();
		public virtual long RenderSize => 8 + Children.Sum(b => b.RenderSize);

		protected long RemainingBoxLength(Stream file) => Header.FilePosition + Header.TotalBoxSize - file.Position;

		public Box(BoxHeader header, IBox? parent)
		{
			Header = header;
			Parent = parent;
		}
		protected abstract void Render(Stream file);
		public T? GetChild<T>() where T : IBox
		{
			IEnumerable<T> children = GetChildren<T>();

			return children.Count() switch
			{
				0 => default,
				1 => children.First(),
				_ => throw new InvalidOperationException($"{GetType().Name} has {children.Count()} children of type {typeof(T)}. Call {nameof(GetChildren)} instead."),
			};
		}

		public T GetChildOrThrow<T>() where T : IBox
			=> GetChild<T>() ?? throw new InvalidDataException($"{Header.Type} does not contain a child of type {typeof(T)}");

		public IEnumerable<T> GetChildren<T>() where T : IBox
		{
			return Children.OfType<T>();
		}

		protected void LoadChildren(Stream file)
		{
			long endPos = Header.FilePosition + Header.TotalBoxSize;

			while (file.Position < endPos)
			{
				IBox child = BoxFactory.CreateBox(file, this);

				if (child.Header.TotalBoxSize == 0)
					break;
				Children.Add(child);
				if (child.Header.FilePosition + child.Header.TotalBoxSize != file.Position)
					break;
			}
		}

		public List<FreeBox> GetFreeBoxes()
		{
			List<FreeBox> freeBoxes = GetChildren<FreeBox>().ToList();

			foreach (var child in Children)
				freeBoxes.AddRange(child.GetFreeBoxes());

			return freeBoxes;
		}

		public void Save(Stream file)
		{
			Header.FilePosition = file.Position;
			file.WriteHeader(Header, RenderSize);

			Render(file);

			foreach (var child in Children)
			{
				child.Save(file);
			}
		}

		#region IDisposable
		protected bool Disposed = false;
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		~Box()
		{
			Dispose(disposing: false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !Disposed)
			{
				foreach (IBox child in Children)
					child?.Dispose();

				Children.Clear();
			}

			Disposed = true;
		}
		#endregion
	}
}
