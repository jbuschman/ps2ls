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
        public Dictionary<Int32, Asset> Assets { get; private set; }

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

                foreach(Asset asset in Assets.Values)
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

        private Pack(String path)
        {
            Path = path;
            Assets = new Dictionary<Int32, Asset>();
        }

        public static Pack LoadBinary(string path)
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
                    pack.Assets.Add(file.Name.GetHashCode(), file);
                }
            }
            while (nextChunkAbsoluteOffset != 0);

            return pack;
        }

        public Boolean ExtractAllAssetsToDirectory(String directory)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }

            foreach (KeyValuePair<Int32, Asset> asset in Assets)
            {
                byte[] buffer = new byte[(int)asset.Value.Size];

                fileStream.Seek(asset.Value.AbsoluteOffset, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, (int)asset.Value.Size);

                FileStream file = new FileStream(directory + @"\" + asset.Value.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, (int)asset.Value.Size);
                file.Close();
            }

            fileStream.Close();

            return true;
        }

        public Boolean ExtractAssetsByNameToDirectory(IEnumerable<String> names, String directory)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }


            foreach(String name in names)
            {
                Asset asset = null;
                
                if(false == Assets.TryGetValue(name.GetHashCode(), out asset))
                {
                    // could not find file, skip.
                    continue;
                }

                byte[] buffer = new byte[(int)asset.Size];

                fileStream.Seek(asset.AbsoluteOffset, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, (int)asset.Size);

                FileStream file = new FileStream(directory + asset.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, (int)asset.Size);
                file.Close();
            }

            fileStream.Close();

            return true;
        }

        public Boolean ExtractAssetByNameToDirectory(String name, String directory) 
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }

            Asset asset = null;

            if (false == Assets.TryGetValue(name.GetHashCode(), out asset))
            {
                fileStream.Close();

                return false;
            }

            byte[] buffer = new byte[(int)asset.Size];

            fileStream.Seek(asset.AbsoluteOffset, SeekOrigin.Begin);
            fileStream.Read(buffer, 0, (int)asset.Size);

            FileStream file = new FileStream(directory + @"\" + asset.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
            file.Write(buffer, 0, (int)asset.Size);
            file.Close();

            fileStream.Close();

            return true;
        }

        public MemoryStream CreateAssetMemoryStreamByName(String name)
        {
            Asset asset = null;

            if (false == Assets.TryGetValue(name.GetHashCode(), out asset))
            {
                return null;
            }

            FileStream file = null;

            try
            {
                file = File.Open(asset.Pack.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return null;
            }

            byte[] buffer = new byte[asset.Size];

            file.Seek(asset.AbsoluteOffset, SeekOrigin.Begin);
            file.Read(buffer, 0, (Int32)asset.Size);

            MemoryStream memoryStream = new MemoryStream(buffer);

            return memoryStream;
        }

        public Boolean CreateTemporaryFileAndOpen(String name)
        {
            String tempPath = System.IO.Path.GetTempPath();

            if (ExtractAssetByNameToDirectory(name, tempPath))
            {
                Process.Start(tempPath + @"\" + name);

                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}