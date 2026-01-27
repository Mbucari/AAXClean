using Mpeg4Lib.Util;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mpeg4Lib.Boxes
{
	public class MdatBox : Box
	{
		public MdatBox(BoxHeader header) : base(header, null) { }

		/// <summary>
		/// Shifts the position of the mdat atom in the stream. When Completed, <see cref="Stream.Position"/> is at the end of the mdat atom.
		/// </summary>
		/// <param name="file">A <see cref="Stream"/> that <see cref="Stream.CanRead"/>, <see cref="Stream.CanWrite"/>, <see cref="Stream.CanSeek"/> </param>
		/// <param name="shiftVector">The size and direction of the shift</param>
		public async Task ShiftMdatAsync(Stream file, long shiftVector, ProgressTracker? progressTracker = null, CancellationToken cancellationToken = default)
		{
			await Mpeg4Util.ShiftDataBlock(file, Header.FilePosition, Header.TotalBoxSize, shiftVector, progressTracker, cancellationToken).ConfigureAwait(false);
			Header.FilePosition += shiftVector;
		}

		protected override void Render(Stream file)
		{
			throw new NotSupportedException();
		}
	}
}
