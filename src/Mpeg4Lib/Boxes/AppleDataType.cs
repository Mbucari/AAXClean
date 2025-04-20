namespace Mpeg4Lib.Boxes;

public enum AppleDataType : uint
{
	/// <summary>The box contains binary data.</summary>
	ContainsData = 0x00,

	/// <summary>Without any count or NULL terminator.</summary>
	Utf_8 = 0x01,

	/// <summary>Also known as UTF-16BE.</summary>
	Utf_16 = 0x02,

	/// <summary>Variant storage of a string for sorting only.</summary>
	Utf_8_Sort = 0x04,

	/// <summary>Variant storage of a string for sorting only.</summary>
	Utf_16_Sort = 0x05,

	/// <summary>The box contains a raw JPEG image.</summary>
	JPEG = 0x0D,

	/// <summary>The box contains a raw PNG image.</summary>
	PNG = 0x0E,

	/// <summary>A big-endian signed integer in 1,2,3 or 4 bytes.</summary>
	/// <remarks>This data type is not supported in Timed Metadata Media. Use one of the fixed-size signed integer data types (that is, type codes 65, 66, or 67) instead.</remarks>
	BE_Signed_Integer = 0x15,

	/// <summary>A big-endian unsigned integer in 1,2,3 or 4 bytes; size of value determines integer size.</summary>
	/// <remarks>This data type is not supported in Timed Metadata Media. Use one of the fixed-size signed integer data types (that is, type codes 65, 66, or 67) instead.</remarks>
	BE_Unsigned_Integer = 0x16,

	/// <summary>A big-endian 32-bit floating point value (IEEE754).</summary>
	BE_Float_32 = 0x17,

	/// <summary>A big-endian 64-bit floating point value (IEEE754).</summary>
	BE_Float_64 = 0x18,

	/// <summary>Windows bitmap format graphics.</summary>
	BMP = 0x1B,

	/// <summary>An 8-bit signed integer.</summary>
	Signed_Byte = 0x41,

	/// <summary>A big-endian 16-bit signed integer.</summary>
	BE_Signed_Integer_16 = 0x42,

	/// <summary>A big-endian 32-bit signed integer.</summary>
	BE_Signed_Integer_32 = 0x43,

	/// <summary>A big-endian 64-bit signed integer.</summary>
	BE_Signed_Integer_64 = 0x4A,

	/// <summary>An 8-bit signed integer.</summary>
	Unigned_Byte = 0x4B,

	/// <summary>A big-endian 16-bit signed integer.</summary>
	BE_Unsigned_Integer_16 = 0x4C,

	/// <summary>A big-endian 32-bit signed integer.</summary>
	BE_Unsigned_Integer_32 = 0x4D,

	/// <summary>A big-endian 64-bit signed integer.</summary>
	BE_Unsigned_Integer_64 = 0x4E,
}
