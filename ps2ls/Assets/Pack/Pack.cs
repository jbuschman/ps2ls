using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using ps2ls.IO;

namespace ps2ls.Assets.Pack
{
    public class Pack
    {
        [DescriptionAttribute("The path on disk to this pack file.")]
        [ReadOnlyAttribute(true)]
        public string Path { get; private set; }

        [BrowsableAttribute(false)]
        public List<Asset> Assets { get; private set; }

        [DescriptionAttribute("The number of assets contained in this pack file.")]
        [ReadOnlyAttribute(true)]
        public Int32 AssetCount { get { return Assets.Count; } }

        [DescriptionAttribute("The total size in bytes of all assets contained in this pack file.")]
        [ReadOnlyAttribute(true)]
        public UInt32 AssetSize
        {
            get
            {
                UInt32 assetSize = 0;

                foreach(Asset asset in Assets)
                {
                    assetSize += asset.Size;
                }

                return assetSize;
            }
        }

        [BrowsableAttribute(false)]
        public String Name
        {
            get { return System.IO.Path.GetFileName(Path); }
        }

        public Dictionary<Int32, Asset> assetLookupCache = new Dictionary<Int32, Asset>();

        private Pack(String path)
        {
            Path = path;
            Assets = new List<Asset>();
        }

        public static Pack LoadPackFromFile(string path)
        {
            Pack pack = new Pack(path);
            FileStream fileStream = null;

            try
            {
                fileStream = File.OpenRead(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return null;
            }

            BinaryReaderBigEndian binaryReader = new BinaryReaderBigEndian(fileStream);
            UInt32 nextChunkAbsoluteOffset = 0;
            UInt32 fileCount = 0;

            do
            {
                fileStream.Seek(nextChunkAbsoluteOffset, SeekOrigin.Begin);

                nextChunkAbsoluteOffset = binaryReader.ReadUInt32();
                fileCount = binaryReader.ReadUInt32();

                for (UInt32 i = 0; i < fileCount; ++i)
                {
                    Asset file = Asset.LoadBinary(pack, binaryReader.BaseStream);
                    pack.assetLookupCache.Add(file.Name.GetHashCode(), file);
                    pack.Assets.Add(file);
                }
            }
            while (nextChunkAbsoluteOffset != 0);

            return pack;
        }


        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// This will the asset object for the given asset name, or null if it doesn't exist in this pack
        /// </summary>
        public Asset GetAssetByName(string name)
        {
            // TODO: Use TryGetValue
            return assetLookupCache[name.GetHashCode()];
        }

        /// <summary>
        /// This will the asset object for the given asset name, or null if it doesn't exist in this pack
        /// It will also pop up a message box if there is a failure.
        /// REVIEW: Would this be better in AssetManager?
        /// </summary>
        public static Dictionary<Pack, IList<Asset>> GetAssetListSortedByPack(IEnumerable<Asset> assets)
        {
            Dictionary<Pack, IList<Asset>> sortedAssetList = new Dictionary<Pack, IList<Asset>>();
            foreach (Asset asset in assets)
            {
                // TODO: Use TryGetValue
                if (!sortedAssetList.Keys.Contains(asset.Pack))
                {
                    sortedAssetList[asset.Pack] = new List<Asset>();
                }
                sortedAssetList[asset.Pack].Add(asset);
            }
            return sortedAssetList;
        }

        /// <summary>
        /// This will return a FileStream that represents the pack, or null if there is a failure.
        /// It will also pop up a message box if there is a failure.
        /// </summary>
        private FileStream createPackFileStream()
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return fileStream;
        }

        /// <summary>
        /// The given asset will be extracted from the given file stream (that should represent the asset's pack) a buffer representing it will be returned
        /// </summary>
        private static byte[] createAssetByteBufferFromFileStream(Asset asset, FileStream packStream)
        {
            byte[] buffer = new byte[(int)asset.Size];

            packStream.Seek(asset.AbsoluteOffset, SeekOrigin.Begin);
            packStream.Read(buffer, 0, (int)asset.Size);

            return buffer;
        }

        /// <summary>
        /// Creates a memory stream that represents the given asset
        /// </summary>
        public MemoryStream CreateAssetMemoryStream(Asset asset)
        {
            System.Diagnostics.Debug.Assert(asset.Pack == this);

            FileStream fileStream = createPackFileStream();
            if (fileStream == null)
            {
                return null;
            }

            byte[] buffer = createAssetByteBufferFromFileStream(asset, fileStream);

            MemoryStream memoryStream = new MemoryStream(buffer);

            return memoryStream;
        }

        #region Extraction Functions
        /// <summary>
        /// Extract all assets from this pack to the given directory
        /// </summary>
        public Boolean ExtractAllAssetsToDirectory(String directory)
        {
            return ExtractAssetsToDirectory(Assets, directory);
        }

        /// <summary>
        /// Extract the given assets from this pack to the given directory
        /// </summary>
        public Boolean ExtractAssetsToDirectory(IEnumerable<Asset> assets, String directory)
        {
            FileStream fileStream = createPackFileStream();
            if (fileStream == null)
            {
                return false;
            }

            foreach (Asset asset in assets)
            {
                System.Diagnostics.Debug.Assert(asset.Pack == this);

                byte[] buffer = createAssetByteBufferFromFileStream(asset, fileStream);

                string filePath = directory + @"\" + asset.Name;
                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, buffer.Length);
                file.Close();

            }

            fileStream.Close();

            return true;
        }

        #endregion //Extraction Functions

    }
}