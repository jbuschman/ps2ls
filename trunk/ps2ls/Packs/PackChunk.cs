using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace ps2ls
{
    public class PackChunk
    {
        private PackChunk()
        {
            NextChunkAbsoluteOffset = 0;
            FileCount = 0;
        }

        public static PackChunk LoadBinary(Pack pack, Stream stream)
        {
            PackChunk chunk = new PackChunk();
            BinaryReaderBigEndian binaryReader = new BinaryReaderBigEndian(stream);

            chunk.NextChunkAbsoluteOffset = binaryReader.ReadUInt32();
            chunk.FileCount = binaryReader.ReadUInt32();

            for (Int32 i = 0; i < chunk.FileCount; ++i)
            {
                PackFile file = PackFile.LoadBinary(pack, stream);
                pack.Files.Add(file.Name.GetHashCode(), file);
            }

            return chunk;
        }

        public UInt32 NextChunkAbsoluteOffset { get; private set; }
        public UInt32 FileCount { get; private set; }
    }
}
