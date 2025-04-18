using System;
using System.Collections.Concurrent;

#nullable enable
namespace AAXClean.Widevine;

public enum KeyType
{
	/// <summary>
	/// Exactly one key of this type must appear.
	/// </summary>
	Signing = 1,
	/// <summary>
	/// Content key.
	/// </summary>
	Content = 2,
	/// <summary>
	/// Key control block for license renewals. No key.
	/// </summary>
	KeyControl = 3,
	/// <summary>
	/// wrapped keys for auxiliary crypto operations.
	/// </summary>
	OperatorSession = 4,
	/// <summary>
	/// Entitlement keys.
	/// </summary>
	Entitlement = 5,
	/// <summary>
	/// Partner-specific content key.
	/// </summary>
	OemContent = 6,
}
public class WidevineKey
{
	public Guid Kid { get; }
	public KeyType Type { get; }
	public byte[] Key { get; }
	internal WidevineKey(Guid kid, License.Types.KeyContainer.Types.KeyType type, byte[] key)
	{
		Kid = kid;
		Type = (KeyType)type;
		Key = key;
	}
	public override string ToString() => $"{Convert.ToHexString(Kid.ToByteArray()).ToLower()}:{Convert.ToHexString(Key).ToLower()}";
}

public class Cdm
{
	public static void dostucc(byte[] data)
	{
		Provisioning.ProvisioningRequest.Parser.ParseFrom(data);
	}
	public static Guid WidevineContentProtection { get; } = new("edef8ba9-79d6-4ace-a3c8-27dcd51d21ed");
	const int MAX_NUM_OF_SESSIONS = 16;
	public Device Device { get; }

	internal ConcurrentDictionary<Guid, Session> Sessions { get; } = new(-1, MAX_NUM_OF_SESSIONS);

	public Cdm(Device device)
	{
		Device = device;
	}

	public ISession OpenSession()
	{
		if (Sessions.Count == MAX_NUM_OF_SESSIONS)
			throw new Exception("Too Many Sessions");

		var session = new Session(Sessions.Count + 1, this);

		var ddd = Sessions.TryAdd(session.Id, session);
		return session;
	}
}
