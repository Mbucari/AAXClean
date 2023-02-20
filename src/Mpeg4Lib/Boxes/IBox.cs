using System;
using System.Collections.Generic;
using System.IO;

namespace Mpeg4Lib.Boxes
{
	public interface IBox : IDisposable
	{
		public IBox Parent { get; }
		public BoxHeader Header { get; }
		public List<IBox> Children { get; }
		long RenderSize { get; }
		void Save(Stream file);
		List<FreeBox> GetFreeBoxes();
		T GetChild<T>() where T : IBox;
		IEnumerable<T> GetChildren<T>() where T : IBox;
	}
}
