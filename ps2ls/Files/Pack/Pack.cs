using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using ps2ls.IO;

namespace ps2ls.Files.Pack
{
    public class Pack
    {
        [DescriptionAttribute("The path on disk to this pack file.")]
        [ReadOnlyAttribute(true)]
        public string Path { get; private set; }

        [BrowsableAttribute(false)]
        public Dictionary<Int32, PackFile> Files { get; private set; }

        [DescriptionAttribute("The number of files contained in this pack file.")]
        [ReadOnlyAttribute(true)]
        public Int32 FileCount { get { return Files.Count; } }

        [DescriptionAttribute("The total size in bytes of all files contained in this pack file.")]
        [ReadOnlyAttribute(true)]
        public UInt32 FileSize
        {
            get
            {
                UInt32 fileSize = 0;

                foreach(PackFile file in Files.Values)
                {
                    fileSize += file.Length;
                }

                return fileSize;
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
            Files = new Dictionary<Int32, PackFile>();
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
                    PackFile file = PackFile.LoadBinary(pack, binaryReader.BaseStream);
                    pack.Files.Add(file.Name.GetHashCode(), file);
                }
            }
            while (nextChunkAbsoluteOffset != 0);

            return pack;
        }

        public Boolean ExtractAllFilesToDirectory(String directory)
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

            foreach (KeyValuePair<Int32, PackFile> packFile in Files)
            {
                byte[] buffer = new byte[(int)packFile.Value.Length];

                fileStream.Seek(packFile.Value.AbsoluteOffset, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, (int)packFile.Value.Length);

                FileStream file = new FileStream(directory + @"\" + packFile.Value.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, (int)packFile.Value.Length);
                file.Close();
            }

            fileStream.Close();

            return true;
        }

        public Boolean ExtractFilesByNameToDirectory(IEnumerable<String> names, String directory)
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
                PackFile packFile = null;
                
                if(false == Files.TryGetValue(name.GetHashCode(), out packFile))
                {
                    // could not find file, skip.
                    continue;
                }

                byte[] buffer = new byte[(int)packFile.Length];

                fileStream.Seek(packFile.AbsoluteOffset, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, (int)packFile.Length);

                FileStream file = new FileStream(directory + packFile.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                file.Write(buffer, 0, (int)packFile.Length);
                file.Close();
            }

            fileStream.Close();

            return true;
        }

        public Boolean ExtractFileByNameToDirectory(String name, String directory) 
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

            PackFile packFile = null;

            if (false == Files.TryGetValue(name.GetHashCode(), out packFile))
            {
                fileStream.Close();

                return false;
            }

            byte[] buffer = new byte[(int)packFile.Length];

            fileStream.Seek(packFile.AbsoluteOffset, SeekOrigin.Begin);
            fileStream.Read(buffer, 0, (int)packFile.Length);

            FileStream file = new FileStream(directory + @"\" + packFile.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
            file.Write(buffer, 0, (int)packFile.Length);
            file.Close();

            fileStream.Close();

            return true;
        }

        public MemoryStream CreateMemoryStreamByName(String name)
        {
            PackFile packFile = null;

            if (false == Files.TryGetValue(name.GetHashCode(), out packFile))
            {
                return null;
            }

            FileStream file = null;

            try
            {
                file = File.Open(packFile.Pack.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return null;
            }

            byte[] buffer = new byte[packFile.Length];

            file.Seek(packFile.AbsoluteOffset, SeekOrigin.Begin);
            file.Read(buffer, 0, (Int32)packFile.Length);

            MemoryStream memoryStream = new MemoryStream(buffer);

            return memoryStream;
        }

        public Boolean CreateTemporaryFileAndOpen(String name)
        {
            String tempPath = System.IO.Path.GetTempPath();

            if (ExtractFileByNameToDirectory(name, tempPath))
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