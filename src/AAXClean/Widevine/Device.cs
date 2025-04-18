using System;
using System.IO;
using System.Security.Cryptography;

namespace AAXClean.Widevine;

public enum DeviceTypes : byte
{
	Unknown = 0,
	Chrome = 1,
	Android = 2
}

public class Device
{
	public DeviceTypes Type { get; }
	public int FileVersion { get; }
	public int SecurityLevel { get; }
	public int Flags { get; }

	public RSA CdmKey { get; }
	internal ClientIdentification ClientId { get; }

	public Device(string cdmFile)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(cdmFile, nameof(cdmFile));

		if (!File.Exists(cdmFile))
			throw new FileNotFoundException("Could not locate CDM file", cdmFile);

		Span<byte> fileData = File.ReadAllBytes(cdmFile);

		if (fileData.Length < 7 || fileData[0] != 'W' || fileData[1] != 'V' || fileData[2] != 'D')
			throw new InvalidDataException();

		FileVersion = fileData[3];
		Type = (DeviceTypes)fileData[4];
		SecurityLevel = fileData[5];
		Flags = fileData[6];

		if (FileVersion != 2)
			throw new InvalidDataException($"Unknown CDM File Verison: '{FileVersion}'");
		if (Type != DeviceTypes.Android)
			throw new InvalidDataException($"Unknown CDM Type: '{Type}'");
		if (SecurityLevel != 3)
			throw new InvalidDataException($"Unknown CDM Security Level: '{SecurityLevel}'");

		var privateKeyLength = (fileData[7] << 8) | fileData[8];

		if (privateKeyLength <= 0 || fileData.Length < 9 + privateKeyLength + 2)
			throw new InvalidDataException($"Invalid private key length: '{privateKeyLength}'");

		var clientIdLength = (fileData[9 + privateKeyLength] << 8) | fileData[10 + privateKeyLength];

		if (clientIdLength <= 0 || fileData.Length < 11 + privateKeyLength + clientIdLength)
			throw new InvalidDataException($"Invalid client id length: '{clientIdLength}'");

		ClientId = ClientIdentification.Parser.ParseFrom(fileData.Slice(11 + privateKeyLength));
		CdmKey = RSA.Create();
		CdmKey.ImportRSAPrivateKey(fileData.Slice(9, privateKeyLength), out _);
	}

	public byte[] SignMessage(byte[] message)
	{
		using var sha1 = SHA1.Create();
		var digestion = sha1.ComputeHash(message);
		return CdmKey.SignHash(digestion, HashAlgorithmName.SHA1, RSASignaturePadding.Pss);
	}

	public bool VerifyMessage(byte[] message, byte[] signature)
	{
		using var sha1 = SHA1.Create();
		var digestion = sha1.ComputeHash(message);
		return CdmKey.VerifyHash(digestion, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pss);
	}

	public byte[] DecryptSessionKey(byte[] sessionKey)
		=> CdmKey.Decrypt(sessionKey, RSAEncryptionPadding.OaepSHA1);
}
