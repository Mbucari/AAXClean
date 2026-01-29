using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mpeg4Lib;

public class Mpeg4File : IDisposable
{
	public Stream InputStream { get; }
	public FtypBox Ftyp { get; set; }
	public MoovBox Moov { get; }
	public MdatBox Mdat { get; }
	public MetadataItems MetadataItems => lazyMetadataItems.Value;

	private readonly Lazy<MetadataItems> lazyMetadataItems;
	public virtual TimeSpan Duration => TimeSpan.FromSeconds((double)Moov.AudioTrack.Mdia.Mdhd.Duration / TimeScale);
	public int MaxBitrate => (int)(AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.MaxBitrate ?? 0);
	public AudioSampleEntry AudioSampleEntry { get; }
	public List<IBox> TopLevelBoxes { get; }

	private int m_Disposed;
	protected bool Disposed => m_Disposed != 0;

	public Mpeg4File(Stream file) : this(file, file.Length) { }

	public Mpeg4File(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read)
		: this(File.Open(fileName, FileMode.Open, access, share)) { }

	public Mpeg4File(Stream file, long fileSize)
	{
		InputStream = file.CanSeek ? file : new TrackedReadStream(file, fileSize);

		TopLevelBoxes = Mpeg4Util.LoadTopLevelBoxes(InputStream);
		Ftyp = TopLevelBoxes.OfType<FtypBox>().Single();
		Moov = TopLevelBoxes.OfType<MoovBox>().Single();
		Mdat = TopLevelBoxes.OfType<MdatBox>().Single();

		lazyMetadataItems = new Lazy<MetadataItems>(() => new MetadataItems(Moov.ILst ?? Moov.CreateEmptyMetadata()));
		AudioSampleEntry = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry
			?? throw new InvalidOperationException("The audio track's AudioSampleEntry is null");
	}

	private int? m_timescale = null;
	private int? m_audioChannels = null;
	private int? m_averageBitrate = null;

	public int TimeScale => m_timescale ??=
		AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.AudioSpecificConfig.SamplingFrequency ??
		AudioSampleEntry.Dec3?.SampleRate ??
		AudioSampleEntry.Dac4?.SampleRate ??
		(int)Moov.AudioTrack.Mdia.Mdhd.Timescale;

	public int AudioChannels => m_audioChannels ??=
		AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.AudioSpecificConfig.ChannelConfiguration ??
		AudioSampleEntry.Dec3?.NumberOfChannels ??
		AudioSampleEntry.Dac4?.NumberOfChannels ??
		AudioSampleEntry.ChannelCount;

	public int AverageBitrate => m_averageBitrate ??=
		(int)(AudioSampleEntry.Esds?.ES_Descriptor.DecoderConfig.AverageBitrate ??
		AudioSampleEntry.Dec3?.AverageBitrate ??
		AudioSampleEntry?.Dac4?.AverageBitrate ??
		CalculateBitrate());

	protected virtual uint CalculateBitrate()
	{
		var totalSize = Moov.AudioTrack.Mdia.Minf.Stbl.Stsz?.TotalSize;
		return !totalSize.HasValue || totalSize.Value == 0 ? 0 : (uint)Math.Round(totalSize.Value * 8 / Duration.TotalSeconds, 0);
	}

	public async Task SaveAsync(bool keepMoovInFront = true, ProgressTracker? progressTracker = null, CancellationToken cancellationToken = default)
	{
		if (!InputStream.CanRead || !InputStream.CanWrite || !InputStream.CanSeek)
			throw new InvalidOperationException($"{nameof(InputStream)} must be readable, writable and seekable to save");

		//Remove Free boxes and work with net size change
		foreach (var box in Moov.GetFreeBoxes())
			box.Parent?.Children.Remove(box);

		InputStream.Position = 0;
		bool moovInFront = Moov.Header.FilePosition < Mdat.Header.FilePosition;
		if (moovInFront)
		{
			var totalSizeChange = Ftyp.RenderSize + Moov.RenderSize - Mdat.Header.FilePosition;
			if (totalSizeChange == 0)
			{
				Ftyp.Save(InputStream);
				Moov.Save(InputStream);
			}
			else if (FreeBox.MinSize + totalSizeChange <= 0)
			{
				//Ftyp + Moov shrank more than FreeBox.MinSize
				//We can accomodate the change with a Free box
				Ftyp.Save(InputStream);
				Moov.Save(InputStream);
				FreeBox.Create(-totalSizeChange, null).Save(InputStream);
			}
			else
			{
				//Ftyp + Moov either grew, or they shrank less than FreeBox.MinSize
				if (keepMoovInFront)
				{
					//Shift Mdat by totalSizeChange to fit the new Moov exactly.
					totalSizeChange = Moov.ShiftChunkOffsetsWithMoovInFront(totalSizeChange);
					await Mdat.ShiftMdatAsync(InputStream, totalSizeChange, progressTracker, cancellationToken);
					InputStream.Position = 0;
					Ftyp.Save(InputStream);
					Moov.Save(InputStream);
					InputStream.SetLength(Mdat.Header.FilePosition + Mdat.Header.TotalBoxSize);
				}
				else
				{
					//Replace the moov with a free box and write the moov at the end
					var freeBoxSize = Mdat.Header.FilePosition - Ftyp.RenderSize;
					if (freeBoxSize < 8)
					{
						//The only way this can happen is if the ftyp grew so much that it now exceeds the
						//original ftyp + moov box sizes, which should never happen since ftyp is supposed
						//to be on the order of a few dozen kilobytes in size.
						throw new InvalidOperationException("Not enough space to write ftyp box before mdat box");
					}

					Ftyp.Save(InputStream);
					FreeBox.Create(freeBoxSize, null).Save(InputStream);
					InputStream.Position = Mdat.Header.FilePosition + Mdat.Header.TotalBoxSize;
					Moov.Save(InputStream);
				}
			}
		}
		else
		{
			//Moov is at the end of the file
			bool rewriteMoovAtEnd = true;
			long ftypSizeChange = Ftyp.RenderSize - Ftyp.Header.TotalBoxSize;
			if (ftypSizeChange == 0)
			{
				Ftyp.Save(InputStream);
			}
			else if (FreeBox.MinSize + ftypSizeChange <= 0)
			{
				//Ftyp shrank more than FreeBox.MinSize
				//We can accomodate the change with a Free box
				Ftyp.Save(InputStream);
				FreeBox.Create(-ftypSizeChange, null).Save(InputStream);
			}
			else
			{
				//Ftyp either grew or it shrank less than FreeBox.MinSize. We have to shift the mdat.
				if (keepMoovInFront)
				{
					//The Moov is at the end, but since we're shifting the entire mdat anway,
					//we might as well rewrite the moov at the beginning place
					long shiftVector = ftypSizeChange + Moov.RenderSize;
					shiftVector = Moov.ShiftChunkOffsetsWithMoovInFront(shiftVector);
					await Mdat.ShiftMdatAsync(InputStream, shiftVector, progressTracker, cancellationToken);
					InputStream.Position = 0;
					Ftyp.Save(InputStream);
					Moov.Save(InputStream);
					InputStream.SetLength(Mdat.Header.FilePosition + Mdat.Header.TotalBoxSize);
					rewriteMoovAtEnd = false;
				}
				else
				{
					//Shift mdat to accomodate ftyp size change
					//Since Moov is being re-written at the end, we don't need to worry about the stco/co64 
					//conversion changing the size of the moov box. Moov gets rewritten at the end anyway.
					Moov.ShiftChunkOffsets(ftypSizeChange);
					await Mdat.ShiftMdatAsync(InputStream, ftypSizeChange, progressTracker, cancellationToken);
					InputStream.Position = 0;
					Ftyp.Save(InputStream);
				}
			}

			if (rewriteMoovAtEnd)
			{
				//Go to the end of the mdat and re-write the moov.
				InputStream.Position = Mdat.Header.FilePosition + Mdat.Header.TotalBoxSize;
				Moov.Save(InputStream);
				InputStream.SetLength(InputStream.Position);
			}
		}

		await InputStream.FlushAsync(cancellationToken);
	}

	public static async Task RelocateMoovToBeginningAsync(string mp4FilePath, ProgressTracker? progressTracker = null, CancellationToken cancellationToken = default)
	{
		List<IBox> boxes;

		using (FileStream fileStream = File.OpenRead(mp4FilePath))
			boxes = Mpeg4Util.LoadTopLevelBoxes(fileStream);

		try
		{
			long ftypeSize = boxes.OfType<FtypBox>().Single().RenderSize;
			MoovBox moov = boxes.OfType<MoovBox>().Single();

			if (progressTracker is not null)
			{
				progressTracker.TotalDuration = TimeSpan.FromSeconds(moov.Mvhd.Duration / (double)moov.Mvhd.Timescale);
				if (moov.Header.FilePosition == ftypeSize)
				{
					//Moov is already at the beginning, immidately following ftyp.
					progressTracker.MovedBytes = progressTracker.TotalSize = 1;
					return;
				}
			}

			var mdat = boxes.OfType<MdatBox>().Single();

			//Figure out how much mdat must be shifted to make room for moov at the beginning.
			long toShift = ftypeSize + moov.RenderSize - mdat.Header.FilePosition;
			long shifted = moov.ShiftChunkOffsetsWithMoovInFront(toShift);

			using FileStream mpegFile = new(mp4FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			await mdat.ShiftMdatAsync(mpegFile, shifted, progressTracker, cancellationToken);
			mpegFile.Position = ftypeSize;
			moov.Save(mpegFile);
			mpegFile.SetLength(mpegFile.Position + mdat.Header.TotalBoxSize);
		}
		finally
		{
			foreach (IBox box in boxes)
				box.Dispose();
		}
	}

	~Mpeg4File()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing && Interlocked.CompareExchange(ref m_Disposed, 1, 0) == 0)
		{
			InputStream.Dispose();
			foreach (var box in TopLevelBoxes)
				box.Dispose();
		}
	}
}
