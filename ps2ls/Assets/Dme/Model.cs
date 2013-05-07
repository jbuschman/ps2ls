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
        public UInt32 Version { get; private set; }
        public String Name { get; private set; }
        public UInt32 Unknown0 { get; private set; }
        public UInt32 Unknown1 { get; private set; }
        public UInt32 Unknown2 { get; private set; }
        public Vector3 Min { get; private set; }
        public Vector3 Max { get; private set; }
        public List<Material> Materials { get; private set; }
        public Mesh[] Meshes { get; private set; }
        public List<String> TextureStrings  { get; private set; }
        public BoneMap[] BoneMaps { get; private set; }
        public Matrix4[] Bones { get; private set; }
        public Vector3[] BonesMins { get; private set; }
        public Vector3[] BonesMaxs { get; private set; }
        public UInt32[] BoneHashes { get; private set; }

        #region Attributes
        public UInt32 VertexCount
        {
            get
            {
                UInt32 vertexCount = 0;

                for (Int32 i = 0; i < Meshes.Length; ++i)
                {
                    vertexCount += Meshes[i].VertexCount;
                }

                return vertexCount;
            }
        }
        public UInt32 IndexCount
        {
            get
            {
                UInt32 indexCount = 0;

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
            Model model = new Model();

            model.Version = binaryReader.ReadUInt32();

            if (model.Version != 4)
            {
                return null;
            }

            UInt32 modelHeaderOffset = binaryReader.ReadUInt32();

            model.Name = name;

            //textures & materials
            model.TextureStrings = new List<String>();
            model.Materials = new List<Material>();

            Dma.Dma.LoadFromStream(binaryReader.BaseStream, model.TextureStrings, model.Materials);

            //bounding box
            Vector3 min = new Vector3();
            min.X = binaryReader.ReadSingle();
            min.Y = binaryReader.ReadSingle();
            min.Z = binaryReader.ReadSingle();
            model.Min = min;
            
            Vector3 max = new Vector3();
            max.X = binaryReader.ReadSingle();
            max.Y = binaryReader.ReadSingle();
            max.Z = binaryReader.ReadSingle();
            model.Max = max;

            //meshes
            UInt32 meshCount = binaryReader.ReadUInt32();

            model.Meshes = new Mesh[meshCount];

            for (Int32 i = 0; i < meshCount; ++i)
            {
                Mesh mesh = Mesh.LoadFromStream(binaryReader.BaseStream, model.Materials);

                if (mesh != null)
                {
                    model.Meshes[i] = mesh;
                }
            }

            //bone maps
            UInt32 boneMapCount = binaryReader.ReadUInt32();

            model.BoneMaps = new BoneMap[boneMapCount];

            for (Int32 i = 0; i < boneMapCount; ++i)
            {
                BoneMap boneMap = BoneMap.LoadFromStream(binaryReader.BaseStream);
                model.BoneMaps[i] = boneMap;
            }

            //bone map entries
            UInt32 boneMapEntryCount = binaryReader.ReadUInt32();

            BoneMapEntry[] boneMapEntries = new BoneMapEntry[boneMapEntryCount];

            for (Int32 i = 0; i < boneMapEntryCount; ++i)
            {
                BoneMapEntry boneMapEntry = BoneMapEntry.LoadFromStream(binaryReader.BaseStream);

                boneMapEntries[i] = boneMapEntry;
            }

            for (Int32 i = 0; i < model.BoneMaps.Length; ++i)
            {
                UInt32 end = 0;

                if (model.BoneMaps[i].BoneCount > 0)
                {
                    for (Int32 j = 0; j < model.BoneMaps[i].BoneCount; ++j)
                    {
                        if (boneMapEntries[j].GlobalIndex + model.BoneMaps[i].Delta > end)
                        {
                            end = boneMapEntries[j].GlobalIndex + model.BoneMaps[i].Delta;
                        }
                    }
                }

                model.BoneMaps[i].BoneEnd = end;
            }

            UInt32 boneCount = binaryReader.ReadUInt32();

            model.Bones = new Matrix4[boneCount];
            model.BonesMins = new Vector3[boneCount];
            model.BonesMaxs = new Vector3[boneCount];
            model.BoneHashes = new UInt32[boneCount];

            if (boneCount > 0)
            {
                for (Int32 i = 0; i < boneCount; ++i)
                {
                    Matrix4 boneMatrix = Matrix4.Identity;

                    boneMatrix.M11 = binaryReader.ReadSingle();
                    boneMatrix.M12 = binaryReader.ReadSingle();
                    boneMatrix.M13 = binaryReader.ReadSingle();

                    boneMatrix.M21 = binaryReader.ReadSingle();
                    boneMatrix.M22 = binaryReader.ReadSingle();
                    boneMatrix.M23 = binaryReader.ReadSingle();

                    boneMatrix.M31 = binaryReader.ReadSingle();
                    boneMatrix.M32 = binaryReader.ReadSingle();
                    boneMatrix.M33 = binaryReader.ReadSingle();

                    boneMatrix.M41 = binaryReader.ReadSingle();
                    boneMatrix.M42 = binaryReader.ReadSingle();
                    boneMatrix.M43 = binaryReader.ReadSingle();

                    boneMatrix.Invert();

                    model.Bones[i] = boneMatrix;
                }
                
                //bones bounding box
                for(Int32 i = 0; i < boneCount; ++i)
                {
                    Vector3 boneMin = new Vector3();
                    boneMin.X = binaryReader.ReadSingle();
                    boneMin.Y = binaryReader.ReadSingle();
                    boneMin.Z = binaryReader.ReadSingle();
                    model.BonesMins[i] = boneMin;

                    Vector3 boneMax = new Vector3();
                    boneMax.X = binaryReader.ReadSingle();
                    boneMax.Y = binaryReader.ReadSingle();
                    boneMax.Z = binaryReader.ReadSingle();
                    model.BonesMaxs[i] = boneMax;
                }

                //bone hashes
                for (Int32 i = 0; i < boneCount; ++i)
                {
                    model.BoneHashes[i] = binaryReader.ReadUInt32();
                }
            }

            return model;
        }
    }
}