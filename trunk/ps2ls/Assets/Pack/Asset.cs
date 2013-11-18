using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using ps2ls.IO;

namespace ps2ls.Assets.Pack
{
    public class Asset
    {
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

        private Asset(Pack pack)
        {
            Pack = pack;
            Name = String.Empty;
            Size = 0;
            AbsoluteOffset = 0;
            Type = Types.Unknown;
        }

        static Asset()
        {
            createTypeImages();
        }

        public static Asset LoadBinary(Pack pack, Stream stream)
        {
            BinaryReaderBigEndian reader = new BinaryReaderBigEndian(stream);

            Asset asset = new Asset(pack);

            uint count = reader.ReadUInt32();
            asset.Name = new String(reader.ReadChars((int)count));
            asset.AbsoluteOffset = reader.ReadUInt32();
            asset.Size = reader.ReadUInt32();
            asset.Crc32 = reader.ReadInt32();

            //determine asset type from file extension
            string extension = Path.GetExtension(asset.Name).Substring(1);
            Types assetType = Types.Unknown;
            GetTypeFromExtension(extension, ref assetType);
            asset.Type = assetType;

            return asset;
        }

        public static void GetTypeFromExtension(string extension, ref Asset.Types type)
        {
            try
            {
                type = (Asset.Types)Enum.Parse(typeof(Types), extension, true);
            }
            catch (System.ArgumentException exception)
            {
                // This extension isn't mapped in the enum
                System.Diagnostics.Debug.Write(exception.ToString());
                type = Types.Unknown;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private static Dictionary<Types, System.Drawing.Image> typeImages;

        public static System.Drawing.Image GetImageFromType(Asset.Types type)
        {
            return typeImages[type];
        }

        private static void createTypeImages()
        {
            if (typeImages != null)
                return;

            typeImages = new Dictionary<Types, System.Drawing.Image>();

            foreach (Types type in Enum.GetValues(typeof(Types)))
            {
                switch (type)
                {
                    case Asset.Types.DME:
                        typeImages[type] = Properties.Resources.tree;
                        break;
                    case Asset.Types.DDS:
                        typeImages[type] = Properties.Resources.image;
                        break;
                    case Asset.Types.TXT:
                        typeImages[type] = Properties.Resources.document_tex;
                        break;
                    case Asset.Types.XML:
                        typeImages[type] = Properties.Resources.document_xaml;
                        break;
                    case Asset.Types.FSB:
                        typeImages[type] = Properties.Resources.music;
                        break;
                    default:
                        typeImages[type] = Properties.Resources.question;
                        break;
                }
            }
        }

        [BrowsableAttribute(false)]
        public Pack Pack { get; private set; }

        public string Name { get; private set; }
        public uint Size { get; private set; }
        public uint AbsoluteOffset { get; private set; }
        public int Crc32 { get; private set; }

        public Asset.Types Type { get; private set; }

        public class NameComparer : Comparer<Asset>
        {
            public override int Compare(Asset x, Asset y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }
        public class SizeComparer : Comparer<Asset>
        {
            public override int Compare(Asset x, Asset y)
            {
                if (x.Size > y.Size)
                    return -1;
                if (x.Size < y.Size)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
