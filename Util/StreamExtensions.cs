using AAXClean.Boxes;
using System;
using System.IO;

namespace AAXClean.Util
{
    internal static class StreamExtensions
    {
        public static void WriteHeader(this Stream stream, BoxHeader header)
        {
            if (header.Version == 1)
            {
                stream.WriteUInt32BE(1);
                stream.WriteType(header.Type);
                stream.WriteInt64BE(header.TotalBoxSize);
            }
            else
            {
                stream.WriteUInt32BE((uint)header.TotalBoxSize);
                stream.WriteType(header.Type);
            }
        }
        public static void WriteType(this Stream stream, string type)
        {
            if (type?.Length != 4)
                throw new Exception("Type must be 4 chas long.");

            stream.WriteDWord(new byte[] { (byte)type[0], (byte)type[1], (byte)type[2], (byte)type[3] });
        }
        public static void WriteInt16BE(this Stream stream, short value)
        {
            stream.WriteUInt16BE((ushort)value);
        }
        public static void WriteUInt16BE(this Stream stream, ushort value)
        {
            stream.WriteWord(new byte[] { (byte)(value >> 8), (byte)value });
        }
        public static void WriteInt32BE(this Stream stream, int value)
        {
            stream.WriteUInt32BE((uint)value);
        }
        public static void WriteUInt32BE(this Stream stream, uint value)
        {
            stream.WriteDWord(new byte[] { (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
        }
        public static void WriteInt64BE(this Stream stream, long value)
        {
            stream.WriteUInt64BE((ulong)value);
        }
        public static void WriteUInt64BE(this Stream stream, ulong value)
        {
            stream.WriteQWord(new byte[] { (byte)(value >> 56), (byte)(value >> 48), (byte)(value >> 40), (byte)(value >> 32), (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value });
        }
        public static void WriteWord(this Stream stream, byte[] word)
        {
            if (word.Length != 2) throw new Exception("Word must be 2 bytes.");
            stream.Write(word);
        }
        public static void WriteDWord(this Stream stream, byte[] dWord)
        {
            if (dWord.Length != 4) throw new Exception("Word must be 4 bytes.");
            stream.Write(dWord);
        }
        public static void WriteQWord(this Stream stream, byte[] qWord)
        {
            if (qWord.Length != 8) throw new Exception("Word must be 8 bytes.");
            stream.Write(qWord);
        }
        public static byte[] ReadBlock(this Stream stream, int length)
        {
            if (length < 0)
                throw new ArgumentException("Length must be non-negative", nameof(length));

            if (length == 0)
                return Array.Empty<byte>();


            byte[] buffer = new byte[length];
            int read = 0, needed = length;
            int count;
            do
            {
                count = stream.Read(buffer, read, needed);
                read += count;
                needed -= count;
            } while (needed > 0 && count != 0);

            return buffer;
        }
        public static ushort ReadUInt16BE(this Stream stream)
        {
            return (ushort)stream.ReadInt16BE();
        }
        public static short ReadInt16BE(this Stream stream)
        {
            byte[] qword = stream.ReadWord();

            return (short)(qword[0] << 8 | qword[1]);
        }
        public static uint ReadUInt32BE(this Stream stream)
        {
            return (uint)stream.ReadInt32BE();
        }
        public static int ReadInt32BE(this Stream stream)
        {
            byte[] qword = stream.ReadDWord();

            return qword[0] << 24 | qword[1] << 16 | qword[2] << 8 | qword[3];
        }
        public static ulong ReadUInt64BE(this Stream stream)
        {
            byte[] qword = stream.ReadQWord();

            return (ulong)qword[0] << 56 | (ulong)qword[1] << 48 | (ulong)qword[2] << 40 | (ulong)qword[3] << 32 | (ulong)qword[4] << 24 | (ulong)qword[5] << 16 | (ulong)qword[6] << 8 | qword[7];
        }
        public static long ReadInt64BE(this Stream stream)
        {
            return (long)stream.ReadUInt64BE();
        }
        public static byte[] ReadWord(this Stream stream)
        {
            byte[] qword = stream.ReadBlock(2);
            if (qword.Length != 2)
                throw new Exception("Too short");
            return qword;
        }
        public static byte[] ReadDWord(this Stream stream)
        {
            byte[] qword = stream.ReadBlock(4);
            if (qword.Length != 4)
                throw new Exception("Too short");
            return qword;
        }
        public static byte[] ReadQWord(this Stream stream)
        {
            byte[] qword = stream.ReadBlock(8);
            if (qword.Length != 8)
                throw new Exception("Too short");
            return qword;
        }
        public static string ReadType(this Stream stream)
        {
            byte[] qword = stream.ReadDWord();

            return new string(new char[] { (char)qword[0], (char)qword[1], (char)qword[2], (char)qword[3] });
        }
    }
}
