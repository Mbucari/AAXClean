using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mpeg4Lib.Boxes
{
	public abstract class Box : IDisposable
	{
		public Box Parent { get; }
		public BoxHeader Header { get; }
		public List<Box> Children { get; } = new List<Box>();
		public virtual long RenderSize => 8 + Children.Sum(b => b.RenderSize);

		public Box(BoxHeader header, Box parent)
		{
			Header = header;
			Parent = parent;
		}
		protected abstract void Render(Stream file);
		public T GetChild<T>() where T : Box
		{
			IEnumerable<T> children = GetChildren<T>();

			return children.Count() switch
			{
				0 => null,
				1 => children.First(),
				_ => throw new Exception($"{GetType().Name} has {children.Count()} children of type {typeof(T)}. Call {nameof(GetChildren)} instead."),
			};
		}

		public IEnumerable<T> GetChildren<T>() where T : Box
		{
			return Children.Where(c => c is T).Cast<T>();
		}

		protected void LoadChildren(Stream file)
		{
			long endPos = Header.FilePosition + Header.TotalBoxSize;

			while (file.Position < endPos)
			{
				Box child = BoxFactory.CreateBox(file, this);

				if (child.Header.TotalBoxSize == 0)
					break;
				Children.Add(child);
				if (child.Header.FilePosition + child.Header.TotalBoxSize != file.Position)
					break;
			}
		}

		public void Save(Stream file)
		{
			file.WriteHeader(Header, RenderSize);

			Render(file);

			foreach (Box child in Children)
			{
				child.Save(file);
			}
		}

		private bool _disposed = false;
		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			if (disposing)
			{
				foreach (Box child in Children)
					child?.Dispose();

				Children.Clear();
			}

			_disposed = true;
		}
	}
}
