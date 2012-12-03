using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ps2ls.Dme
{
    public class Model
    {
        private Model()
        {
            Meshes = new List<Mesh>();
        }

        public static Model LoadFromFile(String path)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception) { return null; }

            BinaryReaderBigEndian binaryReader = new BinaryReaderBigEndian(fileStream);

            byte[] magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'D' ||
                magic[1] != 'M' ||
                magic[2] != 'O' ||
                magic[3] != 'D')
            {
                return null;
            }

            Model model = new Model();
            Int32 dmodVersion = binaryReader.ReadInt32();

            // only handle version 4 for now
            if (dmodVersion != 4)
            {
                return null;
            }

            Int32 modelHeaderOffset = binaryReader.ReadInt32();

            magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'D' ||
                magic[1] != 'M' ||
                magic[2] != 'A' ||
                magic[3] != 'T')
            {
                return null;
            }

            Int32 dmatVersion = binaryReader.ReadInt32();

            if (dmatVersion != 1)
            {
                return null;
            }



            return model;
        }

        public List<Mesh> Meshes { get; private set; }
    }
}
