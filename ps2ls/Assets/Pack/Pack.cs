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
        public string Path { get; private set; }
        public int AssetCount { get { return Assets.Count; } }
        public uint Size
        {
            get
            {
                uint size = 0;

                foreach(Asset asset in Assets.Values)
                    size += asset.Size;

                return size;
            }
        }

        public string Name
        {
            get { return System.IO.Path.GetFileName(Path); }
        }

        public int Checksum
        {
            get
            {
                int checksum = 0;

                foreach (Asset asset in Assets.Values)
                    checksum += asset.Crc32;

                return checksum;
            }
        }

        public Dictionary<int, Asset> Assets = new Dictionary<int, Asset>();

        private Pack(string path)
        {
            Path = path;
        }

        public static Pack Load(string path)
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
            uint nextChunkAbsoluteOffset = 0;
            uint fileCount = 0;

            do
            {
                fileStream.Seek(nextChunkAbsoluteOffset, SeekOrigin.Begin);

                nextChunkAbsoluteOffset = binaryReader.ReadUInt32();
                fileCount = binaryReader.ReadUInt32();

                for (uint i = 0; i < fileCount; ++i)
                {
                    Asset asset = Asset.LoadBinary(pack, binaryReader.BaseStream);
                    pack.Assets.Add(asset.Name.GetHashCode(), asset);
                }
            }
            while (nextChunkAbsoluteOffset != 0);

            return pack;
        }

        public bool ExtractAllAssetsToDirectory(string directory)
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

            foreach (Asset asset in Assets.Values)
            {
                byte[] buffer = new byte[(int)asset.Size];

                fileStream.Seek(asset.AbsoluteOffset, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, (int)asset.Size);

                FileStream file = new FileStream(directory + @"\" + asset.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, (int)asset.Size);
                file.Close();
            }

            fileStream.Close();

            return true;
        }

        public bool ExtractAssetsToDirectory(IEnumerable<string> names, string directory)
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

            foreach(string name in names)
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

                FileStream file = new FileStream(directory + @"\" + asset.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, (int)asset.Size);
                file.Close();
            }

            fileStream.Close();

            return true;
        }

        public bool ExtractAssetToDirectory(string name, string directory) 
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

        public MemoryStream CreateAssetMemoryStreamByName(string name)
        {
            Asset asset = null;

            if (!Assets.TryGetValue(name.GetHashCode(), out asset))
                return null;

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
            file.Read(buffer, 0, (int)asset.Size);

            MemoryStream memoryStream = new MemoryStream(buffer);

            return memoryStream;
        }

        public bool CreateTemporaryFileAndOpen(string name)
        {
            string tempPath = System.IO.Path.GetTempPath();

            if (ExtractAssetToDirectory(name, tempPath))
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