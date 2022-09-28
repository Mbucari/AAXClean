using System;
using System.IO;

namespace AAXClean
{
	/// <summary>
	/// A read-only stream that tracks the stream position based on the number of bytes read. 
	/// </summary>
	internal class TrackedReadStream : Stream
	{
		private readonly Stream BaseStream;
		private readonly long BaseStreamLength;
		private long ReadPosition = 0;

		public TrackedReadStream(Stream baseStream, long streamLength)
		{
			BaseStream = baseStream;
			BaseStreamLength = streamLength;
		}

		public override bool CanRead => BaseStream.CanRead;
		public override bool CanSeek => BaseStream.CanSeek;
		public override long Length => BaseStreamLength;
		public override bool CanWrite => false;
		public override long Position
		{
			get => CanSeek ? BaseStream.Position : ReadPosition;
			set
			{
				if (!CanSeek)
					throw new NotSupportedException();
				BaseStream.Position = ReadPosition = value;
			}
		}

		public override void Flush()
			=> throw new NotSupportedException();

		public override int Read(byte[] buffer, int offset, int count)
		{
			int totalRead = 0, btsRead;

			do
			{
				btsRead = BaseStream.Read(buffer, offset + totalRead, count - totalRead);
				totalRead += btsRead;
			} while (totalRead < count && btsRead > 0);

			ReadPosition += totalRead;

			return totalRead;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				BaseStream.Dispose();

			base.Dispose(disposing);
		}

		public override void Close()
			=> BaseStream.Close();

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!CanSeek)
				throw new NotSupportedException();

			return ReadPosition = BaseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
			=> throw new NotSupportedException();

		public override void Write(byte[] buffer, int offset, int count)
			=> throw new NotSupportedException();
	}
}
