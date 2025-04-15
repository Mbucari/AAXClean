using System;
using System.IO;

namespace AAXClean
{
	/// <summary>
	/// A write-only stream that tracks the stream position based on the number of bytes written. 
	/// </summary>
	internal class TrackedWriteStream : Stream
	{
		private readonly Stream BaseStream;
		private long WritePosition = 0;

		public TrackedWriteStream(Stream baseStream)
		{
			BaseStream = baseStream;
		}

		public override bool CanRead => false;
		public override bool CanSeek => BaseStream.CanSeek;
		public override long Length => WritePosition;
		public override bool CanWrite => BaseStream.CanWrite;
		public override long Position
		{
			get => CanSeek ? BaseStream.Position : WritePosition;
			set
			{
				if (!CanSeek)
					throw new NotSupportedException();
				BaseStream.Position = value;
			}
		}

		public override void Flush()
			=> BaseStream.Flush();

		public override int Read(byte[] buffer, int offset, int count)
			=> throw new NotSupportedException();

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!CanSeek)
				throw new NotSupportedException();

			return BaseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
			=> throw new NotSupportedException();

		public override void Write(byte[] buffer, int offset, int count)
		{
			BaseStream.Write(buffer, offset, count);
			WritePosition += count;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				BaseStream.Dispose();

			base.Dispose(disposing);
		}
	}
}
