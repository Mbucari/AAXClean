using AAXClean.Boxes;
using System.IO;

namespace AAXClean.Chunks
{
    internal abstract class MdatChunk
    {
        public BoxHeader Header { get; }
        public long Position { get; }
        public MdatChunk(Stream file, BoxHeader header)
        {
            Header = header;
            Position = file.Position - header.HeaderSize;
        }
        public virtual MdatChunk GetNext(Stream file)
        {
            return MdatFactory.CreateEntry(file);
        }
    }
}
