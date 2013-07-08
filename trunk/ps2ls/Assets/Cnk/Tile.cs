using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Assets.Cnk
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

        public Int32 X { get; private set; }
        public Int32 Y { get; private set; }
        public Int32 Unknown0 { get; private set; }
        public Int32 Unknown1 { get; private set; }
        public Int32 EcoCount { get; private set; }
        public Int32 Index { get; private set; }
        public Int32 Unknown2 { get; private set; }
        public UInt32 ImageSize { get; private set; }
        public byte[] ImageData { get; private set; }
        public UInt32 LayerTextureCount { get; private set; }
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

            for (Int32 i = 0; i < tile.EcoCount; ++i)
            {
                UInt32 ev = binaryReader.ReadUInt32();
                UInt32 fc = binaryReader.ReadUInt32();

                for (Int32 j = 0; j < fc; ++j)
                {
                    UInt32 fv = binaryReader.ReadUInt32();
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

                DevIL.ImageExporter asd = new DevIL.ImageExporter();
                DevIL.ImageImporter asd2 = new DevIL.ImageImporter();
                DevIL.Image image = asd2.LoadImageFromStream(memoryStream);
                memoryStream.Close();

                asd.SaveImage(image, @"C:\Users\Colin\Desktop\" + tile.Index + ".dds");
            }

            tile.LayerTextureCount = binaryReader.ReadUInt32();
            tile.LayerTextures = binaryReader.ReadBytes((int)tile.LayerTextureCount);

            return LoadError.None;
        }
    }
}
