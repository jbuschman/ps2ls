using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ps2ls.IO;
using System.Runtime.InteropServices;
using lzhamNET;
using DevIL;

namespace ps2ls.Assets.Cnk
{
    public class Cnk0
    {
        public class Tile
        {
            public enum LoadError
            {
                None,
                NullStream
            }

            private Tile()
            {
            }

            public int X { get; private set; }
            public int Y { get; private set; }
            public int Unknown0 { get; private set; }
            public int Unknown1 { get; private set; }
            public int EcoCount { get; private set; }
            public int Index { get; private set; }
            public int Unknown2 { get; private set; }
            public uint ImageSize { get; private set; }
            public byte[] ImageData { get; private set; }
            public uint LayerTextureCount { get; private set; }
            public byte[] LayerTextures { get; private set; }

            public static LoadError LoadFromStream(Stream stream, out Tile tile)
            {
                if (stream == null)
                {
                    tile = null;
                    return LoadError.NullStream;
                }

                BinaryReader binaryReader = new BinaryReader(stream, Encoding.Default, true);

                tile = new Tile();
                tile.X = binaryReader.ReadInt32();
                tile.Y = binaryReader.ReadInt32();
                tile.Unknown0 = binaryReader.ReadInt32();
                tile.Unknown1 = binaryReader.ReadInt32();
                tile.EcoCount = binaryReader.ReadInt32();

                for (int i = 0; i < tile.EcoCount; ++i)
                {
                    uint ev = binaryReader.ReadUInt32();
                    uint fc = binaryReader.ReadUInt32();

                    for (int j = 0; j < fc; ++j)
                    {
                        uint fv = binaryReader.ReadUInt32();
                        stream.Seek(fv * 8, SeekOrigin.Current);
                    }
                }

                tile.Index = binaryReader.ReadInt32();
                tile.Unknown2 = binaryReader.ReadInt32();

                if (tile.Unknown2 > 0)
                {
                    tile.ImageSize = binaryReader.ReadUInt32();
                    tile.ImageData = binaryReader.ReadBytes((int)tile.ImageSize);

                    MemoryStream memoryStream = new MemoryStream(tile.ImageData);

                    ImageExporter asd = new DevIL.ImageExporter();
                    ImageImporter asd2 = new DevIL.ImageImporter();
                    Image image = asd2.LoadImageFromStream(memoryStream);
                    memoryStream.Close();

                    asd.SaveImage(image, @"C:\Users\Colin\Desktop\" + tile.Index + ".dds");
                }

                tile.LayerTextureCount = binaryReader.ReadUInt32();
                tile.LayerTextures = binaryReader.ReadBytes((int)tile.LayerTextureCount);

                return LoadError.None;
            }
        }

        public class RenderBatch
        {
            public enum LoadError
            {
                None
            }

            private RenderBatch() { }

            public uint IndexOffset { get; private set; }
            public uint IndexCount { get; private set; }
            public uint VertexOffset { get; private set; }
            public uint VertexCount { get; private set; }

            public static LoadError LoadFromStream(Stream stream, out RenderBatch renderBatch)
            {
                BinaryReader binaryReader = new BinaryReader(stream, Encoding.Default, true);

                renderBatch = new RenderBatch();
                renderBatch.IndexOffset = binaryReader.ReadUInt32();
                renderBatch.IndexCount = binaryReader.ReadUInt32();
                renderBatch.VertexOffset = binaryReader.ReadUInt32();
                renderBatch.VertexCount = binaryReader.ReadUInt32();

                return LoadError.None;
            }
        }

        public class Vertex
        {
            public enum LoadError
            {
                None
            }

            private Vertex() { }

            public short X { get; private set; }
            public short Y { get; private set; }
            public short FarHeight { get; private set; }
            public short NearHeight { get; private set; }
            public byte[] C0 { get; private set; }
            public byte[] C1 { get; private set; }

            public static LoadError LoadFromStream(Stream stream, out Vertex vertex)
            {
                BinaryReader binaryReader = new BinaryReader(stream, Encoding.Default, true);

                vertex = new Vertex();
                vertex.X = binaryReader.ReadInt16();
                vertex.Y = binaryReader.ReadInt16();
                vertex.FarHeight = binaryReader.ReadInt16();
                vertex.NearHeight = binaryReader.ReadInt16();
                vertex.C0 = binaryReader.ReadBytes(4);
                vertex.C1 = binaryReader.ReadBytes(4);

                return LoadError.None;
            }

            public override string ToString()
            {
                return string.Format("{0}, {1}, {2}", X, Y, NearHeight);
            }
        }

        public Tile[] Tiles { get; private set; }
        public UInt16[] Indices { get; private set; }
        public Vertex[] Vertices { get; private set; }
        public RenderBatch[] RenderBatches { get; private set; }

        public const uint VERSION = 1;

        public enum LoadError
        {
            None,
            NullStream,
            BadHeader,
            VersionMismatch,
            BadTile,
            BadVertex,
            BadRenderBatch
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
                cnk0 = null;
                return LoadError.BadHeader;
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

            Lzham.ZStream zStream = new Lzham.ZStream();
            Lzham.ZInflateInit2(zStream, 20);
            zStream.AvailableInputBytes = compressedSize;
            zStream.AvailableOutputBytes = uncompressedSize;

            GCHandle inputBufferGCHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
            GCHandle outputBufferGCHandle = GCHandle.Alloc(outputBuffer, GCHandleType.Pinned);

            zStream.NextInputByte = inputBufferGCHandle.AddrOfPinnedObject();
            zStream.NextOutputByte = outputBufferGCHandle.AddrOfPinnedObject();

            Lzham.ZInflate(zStream, (int)Lzham.ZFlush.Finish);
            Lzham.ZInflateEnd(zStream);

            MemoryStream memoryStream = new MemoryStream(outputBuffer);
            binaryReader = new BinaryReader(memoryStream);

            cnk0 = new Cnk0();

            //tiles
            uint tileCount = binaryReader.ReadUInt32();
            cnk0.Tiles = new Tile[tileCount];

            for (uint i = 0; i < tileCount; ++i)
            {
                Tile tile;

                if (Tile.LoadFromStream(memoryStream, out tile) != Tile.LoadError.None)
                {
                    cnk0 = null;
                    return LoadError.BadTile;
                }

                cnk0.Tiles[i] = tile;
            }

            //unknown block
            uint unknown0 = binaryReader.ReadUInt32();
            uint unknown1 = binaryReader.ReadUInt32();
            binaryReader.BaseStream.Seek(unknown1 * 4, SeekOrigin.Current);

            //indices
            uint indexCount = binaryReader.ReadUInt32();
            cnk0.Indices = new ushort[indexCount];

            for (int i = 0; i < indexCount; ++i)
                cnk0.Indices[i] = binaryReader.ReadUInt16();

            //vertices
            uint vertexCount = binaryReader.ReadUInt32();
            cnk0.Vertices = new Vertex[vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            {
                Vertex vertex;

                if (Vertex.LoadFromStream(binaryReader.BaseStream, out vertex) != Vertex.LoadError.None)
                {
                    cnk0 = null;
                    return LoadError.BadVertex;
                }

                cnk0.Vertices[i] = vertex;
            }

            //render batches
            uint renderBatchCount = binaryReader.ReadUInt32();
            cnk0.RenderBatches = new RenderBatch[renderBatchCount];

            for (uint i = 0; i < renderBatchCount; ++i)
            {
                RenderBatch renderBatch;
                if (RenderBatch.LoadFromStream(binaryReader.BaseStream, out renderBatch) != RenderBatch.LoadError.None)
                {
                    cnk0 = null;
                    return LoadError.BadRenderBatch;
                }

                cnk0.RenderBatches[i] = renderBatch;
            }

            binaryReader.Close();

            return LoadError.None;
        }
    }
}