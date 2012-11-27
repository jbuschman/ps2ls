using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace ps2ls
{
    public class Pack
    {
        [DescriptionAttribute("The path on disk to this pack file.")]
        [ReadOnlyAttribute(true)]
        public string Path { get; private set; }

        [BrowsableAttribute(false)]
        public SortedDictionary<String, PackFile> Files { get; private set; }

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

        private Pack(String path)
        {
            Path = path;
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

            pack.Files = new SortedDictionary<String, PackFile>();

            while(true)
            {
                PackChunk chunk = PackChunk.LoadBinary(pack, fileStream);

                if (chunk.NextChunkAbsoluteOffset == 0)
                {
                    break;
                }

                fileStream.Seek(chunk.NextChunkAbsoluteOffset, SeekOrigin.Begin);
            }

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

            foreach (KeyValuePair<String, PackFile> packFile in Files)
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
                
                if(false == Files.TryGetValue(name, out packFile))
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

            if (false == Files.TryGetValue(name, out packFile))
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
    }
}
