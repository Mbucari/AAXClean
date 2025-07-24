using Mpeg4Lib.Util;

namespace Mpeg4Lib.Boxes.AC4SpecificBox;

public class alternative_info
{
	public ushort name_len;
	public string presentation_name;
	public byte n_targets;
	public (byte target_md_compat, byte target_device_category)[] target_ids;
	public alternative_info(BitReader reader)
	{
		name_len = (ushort)reader.Read(16);
		char[] nameBts = new char[name_len];
		for (int i = 0; i < name_len; i++)
		{
			nameBts[i] = (char)reader.Read(8);
		}
		presentation_name = new string(nameBts);
		n_targets = (byte)reader.Read(5);
		target_ids = new (byte target_md_compat, byte target_device_category)[n_targets];

		for (int i = 0; i < name_len; i++)
		{
			target_ids[i] = ((byte)reader.Read(3), (byte)reader.Read(8));
		}
	}
}