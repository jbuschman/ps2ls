using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using ps2ls.IO;

namespace ps2ls.Files.Pack
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
            ".cnk3",
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
            ".txt",
            ".xml",
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
            CNK3,
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
            TXT,
            XML,
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
            Type = Types.Unknown;
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
                    break;
                }
            }

            fileInfo.Extension = Path.GetExtension(fileInfo.Name).Trim(new char[] { '.' }).ToUpper();

            return fileInfo;
        }

        public override string ToString()
        {
            return Name;
        }

        public static System.Drawing.Image GetImageFromType(PackFile.Types type)
        {
            switch (type)
            {
                case PackFile.Types.DME:
                    return Properties.Resources.tree;
                case PackFile.Types.DDS:
                    return Properties.Resources.image;
                case PackFile.Types.TXT:
                    return Properties.Resources.document_tex;
                case PackFile.Types.XML:
                    return Properties.Resources.document_xaml;
                case PackFile.Types.FSB:
                    return Properties.Resources.music;
            }

            return Properties.Resources.question;
        }

        [BrowsableAttribute(false)]
        public Pack Pack { get; private set; }

        public String Name { get; private set; }
        public UInt32 Length { get; private set; }
        public UInt32 AbsoluteOffset { get; private set; }
        public UInt32 Crc32 { get; private set; }

        public String Extension { get; private set; }

        public PackFile.Types Type { get; private set; }

        public class NameComparer : Comparer<PackFile>
        {
            public override int Compare(PackFile x, PackFile y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }
        public class LengthComparer : Comparer<PackFile>
        {
            public override int Compare(PackFile x, PackFile y)
            {
                if (x.Length > y.Length)
                    return -1;
                if (x.Length < y.Length)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
