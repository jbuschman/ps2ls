using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using OpenTK;
using ps2ls.Assets.Dma;
using ps2ls.Graphics.Materials;
using ps2ls.Cryptography;

namespace ps2ls.Assets.Dme
{
    public class Model
    {
        public UInt32 Version = 0;
        public String Name = String.Empty;
        public UInt32 Unknown0 = 0;
        public UInt32 Unknown1 = 0;
        public UInt32 Unknown2 = 0;
        public Vector3 Min = Vector3.Zero;
        public Vector3 Max = Vector3.Zero;
        public List<Material> Materials = new List<Material>();
        public Mesh[] Meshes { get; private set; }

        #region Attributes
        public Int32 VertexCount
        {
            get
            {
                Int32 vertexCount = 0;

                for (Int32 i = 0; i < Meshes.Length; ++i)
                {
                    vertexCount += Meshes[i].VertexCount;
                }

                return vertexCount;
            }
        }
        public Int32 IndexCount
        {
            get
            {
                Int32 indexCount = 0;

                for (Int32 i = 0; i < Meshes.Length; ++i)
                {
                    indexCount += Meshes[i].IndexCount;
                }

                return indexCount;
            }
        }
        #endregion

        public static Model LoadFromStream(String name, Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            //header
            byte[] magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'D' ||
                magic[1] != 'M' ||
                magic[2] != 'O' ||
                magic[3] != 'D')
            {
                return null;
            }

            UInt32 dmodVersion = binaryReader.ReadUInt32();

            if (dmodVersion != 3 && dmodVersion != 4)
            {
                return null;
            }

            UInt32 modelHeaderOffset = binaryReader.ReadUInt32();

            Model model = new Model();
            
            //materials
            Dma.Dma.LoadFromStream(binaryReader.BaseStream, model.Materials);

            //bounding box
            model.Min.X = binaryReader.ReadSingle();
            model.Min.Y = binaryReader.ReadSingle();
            model.Min.Z = binaryReader.ReadSingle();

            model.Max.X = binaryReader.ReadSingle();
            model.Max.Y = binaryReader.ReadSingle();
            model.Max.Z = binaryReader.ReadSingle();

            //meshes
            UInt32 meshCount = binaryReader.ReadUInt32();

            model.Meshes = new Mesh[meshCount];

            for (Int32 i = 0; i < meshCount; ++i)
            {
                Mesh mesh = Mesh.LoadFromStreamWithVersion(binaryReader.BaseStream, dmodVersion, model.Materials);

                if (mesh != null)
                {
                    model.Meshes[i] = mesh;
                }
            }

            return model;
        }
    }
}
