using System;
using System.Buffers.Binary;

namespace Mpeg4Lib;

public interface IAppleData<TData> where TData : IAppleData<TData>
{
	public static abstract int SizeInBytes { get; }
	public static abstract TData Create(ReadOnlySpan<byte> source);
	public void Write(Span<byte> destination);
}

public record TrackNumber(ushort Track, ushort TotalTracks) : IAppleData<TrackNumber>
{
	public static int SizeInBytes => 8;

	public static TrackNumber Create(ReadOnlySpan<byte> source)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(source.Length, SizeInBytes, nameof(source));
		return new TrackNumber(BinaryPrimitives.ReadUInt16BigEndian(source[2..4]), BinaryPrimitives.ReadUInt16BigEndian(source[4..6]));
	}

	public void Write(Span<byte> destination)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, SizeInBytes, nameof(destination));
		BinaryPrimitives.WriteUInt16BigEndian(destination[2..4], Track);
		BinaryPrimitives.WriteUInt16BigEndian(destination[4..6], TotalTracks);
	}

	public static implicit operator TrackNumber((int trackNum, int totalTracks) tn)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(tn.trackNum, nameof(tn.trackNum));
		ArgumentOutOfRangeException.ThrowIfNegative(tn.totalTracks, nameof(tn.totalTracks));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(tn.trackNum, ushort.MaxValue, nameof(tn.trackNum));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(tn.totalTracks, ushort.MaxValue, nameof(tn.totalTracks));
		return new TrackNumber((ushort)tn.trackNum, (ushort)tn.totalTracks);
	}
}

public record DiskNumber(ushort Disk, ushort TotalDisks) : IAppleData<DiskNumber>
{
	public static int SizeInBytes => 6;
	public static DiskNumber Create(ReadOnlySpan<byte> source)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(source.Length, SizeInBytes, nameof(source));
		return new DiskNumber(BinaryPrimitives.ReadUInt16BigEndian(source[2..4]), BinaryPrimitives.ReadUInt16BigEndian(source[4..6]));
	}

	public void Write(Span<byte> destination)
	{
		ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, SizeInBytes, nameof(destination));
		BinaryPrimitives.WriteUInt16BigEndian(destination[2..4], Disk);
		BinaryPrimitives.WriteUInt16BigEndian(destination[4..6], TotalDisks);
	}

	public static implicit operator DiskNumber((int diskNum, int totalDisks) tn)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(tn.diskNum, nameof(tn.diskNum));
		ArgumentOutOfRangeException.ThrowIfNegative(tn.totalDisks, nameof(tn.totalDisks));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(tn.diskNum, ushort.MaxValue, nameof(tn.diskNum));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(tn.totalDisks, ushort.MaxValue, nameof(tn.totalDisks));
		return new DiskNumber((ushort)tn.diskNum, (ushort)tn.totalDisks);
	}
}
