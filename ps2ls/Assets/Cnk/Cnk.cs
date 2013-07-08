using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ps2ls.IO;
using System.Runtime.InteropServices;
using lzhamNET;

namespace ps2ls.Assets.Cnk
{
    public class Cnk0
    {
        public Tile[] Tiles { get; private set; }

        public const int VERSION = 1;

        public enum LoadError
        {
            None,
            NullStream,
            BadHeader,
            VersionMismatch,
            BadTile
        }

        private Cnk0()
        {
        }

        public static LoadError LoadFromStream(Stream stream, out Cnk0 cnk0)
        {
            if (stream == null)
            {
                cnk0 = null;
                return LoadError.NullStream;
            }

            BinaryReader binaryReader = new BinaryReader(stream);

            //header
            char[] magic = binaryReader.ReadChars(4);

            if (magic[0] != 'C' ||
                magic[1] != 'N' ||
                magic[2] != 'K' ||
                magic[3] != '0')
            {
            }

            int version = binaryReader.ReadInt32();

            if (version != VERSION)
            {
                cnk0 = null;
                return LoadError.VersionMismatch;
            }

            uint uncompressedSize = binaryReader.ReadUInt32();
            uint compressedSize = binaryReader.ReadUInt32();

            byte[] inputBuffer = binaryReader.ReadBytes((int)compressedSize);

            binaryReader.Close();

            byte[] outputBuffer = new byte[uncompressedSize];

            LZHAM.ZStream zStream = new LZHAM.ZStream();
            LZHAM.ZInflateInit2(zStream, 20);
            zStream.AvailableInputBytes = compressedSize;
            zStream.AvailableOutputBytes = uncompressedSize;

            GCHandle inputBufferGCHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
            GCHandle outputBufferGCHandle = GCHandle.Alloc(outputBuffer, GCHandleType.Pinned);

            zStream.NextInputByte = inputBufferGCHandle.AddrOfPinnedObject();
            zStream.NextOutputByte = outputBufferGCHandle.AddrOfPinnedObject();

            LZHAM.ZInflate(zStream, (int)LZHAM.ZFlush.Finish);
            LZHAM.ZInflateEnd(zStream);

            MemoryStream memoryStream = new MemoryStream(outputBuffer);
            binaryReader = new BinaryReader(memoryStream);

            cnk0 = new Cnk0();

            UInt32 tileCount = binaryReader.ReadUInt32();

            cnk0.Tiles = new Tile[tileCount];

            for (UInt32 i = 0; i < tileCount; ++i)
            {
                Tile tile;

                if (Tile.LoadFromStream(memoryStream, out tile) != Tile.LoadError.None)
                {
                    cnk0 = null;
                    return LoadError.BadTile;
                }

                cnk0.Tiles[i] = tile;
            }

            binaryReader.Close();

            return LoadError.None;
        }
    }
}
