﻿using AAXClean.FrameFilters;
using AAXClean.FrameFilters.Audio;
using Mpeg4Lib.Boxes;
using Mpeg4Lib.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AAXClean
{
	public sealed class AaxFile : Mp4File
	{
		public byte[] Key { get; private set; }
		public byte[] IV { get; private set; }

		private readonly long OriginalFtypSize;
		private readonly long OriginalMoovSize;

		public AaxFile(Stream file, long fileSize, bool additionalFixups = true) : base(file, fileSize)
		{
			if (FileType != FileType.Aax && FileType != FileType.Aaxc)
				throw new ArgumentException($"This instance of {nameof(Mp4File)} is not an Aax or Aaxc file.");

			OriginalFtypSize = Ftyp.Header.TotalBoxSize;
			OriginalMoovSize = Moov.Header.TotalBoxSize;

			//This is the flag that, if set, prevents cover art from loading on android.
			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Esds.ES_Descriptor.DecoderConfig.AudioSpecificConfig.DependsOnCoreCoder = false;
			//Must change the audio type from aavd to mp4a
			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Header.ChangeAtomName("mp4a");

			//These actions will alter the mpeg-4 size and should not
			//be performed unless re-writing the entire mpeg-4 file.
			if (additionalFixups)
			{
				//Remove extra Free boxes
				List<IBox> children = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children;
				for (int i = children.Count - 1; i >= 0; i--)
				{
					if (children[i] is FreeBox)
						children.RemoveAt(i);
				}

				Ftyp = FtypBox.Create(32, null);
				Ftyp.CompatibleBrands.Clear();
				Ftyp.CompatibleBrands.Add("iso2");
				Ftyp.CompatibleBrands.Add("mp41");
				Ftyp.CompatibleBrands.Add("M4A ");
				Ftyp.CompatibleBrands.Add("M4B ");
			}

			Ftyp.MajorBrand = "isom";
			Ftyp.MajorVersion = 0x200;
		}
		public AaxFile(Stream file) : this(file, file.Length) { }
		public AaxFile(string fileName, FileAccess access = FileAccess.Read, FileShare share = FileShare.Read) : this(File.Open(fileName, FileMode.Open, access, share)) { }

		public override FrameTransformBase<FrameEntry, FrameEntry> GetAudioFrameFilter()
			=> new AavdFilter(Key, IV);

		#region Aax(c) Keys

		public void SetDecryptionKey(string activationBytes)
		{
			if (string.IsNullOrWhiteSpace(activationBytes) || activationBytes.Length != 8)
				throw new ArgumentException($"{nameof(activationBytes)} must be 4 bytes long.");

			byte[] actBytes = ByteUtil.BytesFromHexString(activationBytes);

			SetDecryptionKey(actBytes);
		}

		public void SetDecryptionKey(byte[] activationBytes)
		{
			if (activationBytes is null || activationBytes.Length != 4)
				throw new ArgumentException($"{nameof(activationBytes)} must be 4 bytes long.");
			if (FileType != FileType.Aax)
				throw new ArgumentException($"This instance of {nameof(AaxFile)} is not an {FileType.Aax} file.");

			AdrmBox adrm = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.GetChild<AdrmBox>();

			if (adrm is null)
				throw new Exception($"This instance of {nameof(AaxFile)} does not contain an adrm box.");

			//Adrm key derrivation from 
			//https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/mov.c in mov_read_adrm

			byte[] intermediate_key = Crypto.Sha1(
			   (audible_fixed_key, 0, audible_fixed_key.Length),
			   (activationBytes, 0, activationBytes.Length));

			byte[] intermediate_iv = Crypto.Sha1(
				(audible_fixed_key, 0, audible_fixed_key.Length),
				(intermediate_key, 0, intermediate_key.Length),
				(activationBytes, 0, activationBytes.Length));

			byte[] calculatedChecksum = Crypto.Sha1(
				(intermediate_key, 0, 16),
				(intermediate_iv, 0, 16));

			if (!ByteUtil.BytesEqual(calculatedChecksum, adrm.Checksum))
				throw new Exception("Calculated checksum doesn't match AAX file checksum.");

			byte[] drmBlob = ByteUtil.CloneBytes(adrm.DrmBlob);

			Crypto.DecryptInPlace(
				ByteUtil.CloneBytes(intermediate_key, 0, 16),
				ByteUtil.CloneBytes(intermediate_iv, 0, 16),
				drmBlob);

			if (!ByteUtil.BytesEqual(drmBlob, 0, activationBytes, 0, 4, true))
				throw new Exception("Supplied key doesn't match calculated key.");

			byte[] file_key = ByteUtil.CloneBytes(drmBlob, 8, 16);

			byte[] file_iv = Crypto.Sha1(
				(drmBlob, 26, 16),
				(file_key, 0, 16),
				(audible_fixed_key, 0, 16));

			Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children.Remove(adrm);

			IBox aabd = Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children.FirstOrDefault(b => b.Header.Type == "aabd");
			if (aabd is not null)
				Moov.AudioTrack.Mdia.Minf.Stbl.Stsd.AudioSampleEntry.Children.Remove(aabd);

			SetDecryptionKey(file_key, ByteUtil.CloneBytes(file_iv, 0, 16));
		}

		//Constant key
		//https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/mov.c
		private static readonly byte[] audible_fixed_key = { 0x77, 0x21, 0x4d, 0x4b, 0x19, 0x6a, 0x87, 0xcd, 0x52, 0x00, 0x45, 0xfd, 0x20, 0xa5, 0x1d, 0x67 };

		public void SetDecryptionKey(string audible_key, string audible_iv)
		{
			if (string.IsNullOrWhiteSpace(audible_key) || audible_key.Length != 32)
				throw new ArgumentException($"{nameof(audible_key)} must be 16 bytes long.");
			if (string.IsNullOrWhiteSpace(audible_iv) || audible_iv.Length != 32)
				throw new ArgumentException($"{nameof(audible_iv)} must be 16 bytes long.");

			byte[] key = ByteUtil.BytesFromHexString(audible_key);

			byte[] iv = ByteUtil.BytesFromHexString(audible_iv);

			SetDecryptionKey(key, iv);
		}

		public void SetDecryptionKey(byte[] key, byte[] iv)
		{
			if (key is null || key.Length != 16)
				throw new ArgumentException($"{nameof(key)} must be 16 bytes long.");
			if (iv is null || iv.Length != 16)
				throw new ArgumentException($"{nameof(iv)} must be 16 bytes long.");

			Key = key;
			IV = iv;
		}

		#endregion
	}
}
