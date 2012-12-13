using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace ps2ls
{
    public class PackFile
    {
        private static String[] typeStrings =
        {
            ".adr",
            ".agr",
            ".cdt",
            ".cnk0",
            ".cnk1",
            ".cnk2",
            ".crc",
            ".dds",
            ".dma",
            ".dme",
            ".dmv",
            ".dsk",
            ".eco",
            ".fsb",
            ".fxo",
            ".gfx",
            ".lst",
            ".nsa",
            ".zone"
        };

        public enum Types
        {
            ADR,
            AGR,
            CDT,
            CNK0,
            CNK1,
            CNK2,
            CRC,
            DDS,
            DMA,
            DME,
            DMV,
            DSK,
            ECO,
            FSB,
            FXO,
            GFX,
            LST,
            NSA,
            ZONE,
            Unknown
        };

        private PackFile(Pack pack)
        {
            Pack = pack;
            Name = String.Empty;
            Length = 0;
            AbsoluteOffset = 0;
            Extension = String.Empty;
        }

        public static PackFile LoadBinary(Pack pack, Stream stream)
        {
            BinaryReaderBigEndian reader = new BinaryReaderBigEndian(stream);

            PackFile fileInfo = new PackFile(pack);

            UInt32 count = reader.ReadUInt32();
            fileInfo.Name = new String(reader.ReadChars((Int32)count));
            fileInfo.AbsoluteOffset = reader.ReadUInt32();
            fileInfo.Length = reader.ReadUInt32();
            fileInfo.Crc32 = reader.ReadUInt32();

            String extension = Path.GetExtension(fileInfo.Name);

            for (Int32 i = 0; i < typeStrings.Length; ++i)
            {
                if (extension.ToLower() == typeStrings[i])
                {
                    fileInfo.Type = (PackFile.Types)(i);
                }
            }

            fileInfo.Extension = Path.GetExtension(fileInfo.Name).Trim(new char[] { '.' }).ToUpper();

            return fileInfo;
        }

        [BrowsableAttribute(false)]
        public Pack Pack { get; private set; }

        public String Name { get; private set; }
        public UInt32 Length { get; private set; }
        public UInt32 AbsoluteOffset { get; private set; }
        public UInt32 Crc32 { get; private set; }

        public String Extension { get; private set; }

        public PackFile.Types Type { get; private set; }

    }
}
