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
                UInt32 indexCount = 0;
                UInt32 vertexCount = 0;
                UInt32 bytesPerVertex = 0;
                UInt32 vertexBlockCount = 1;
                UInt32 materialIndex = 0;
                UInt32 meshUnknown1 = 0;
                UInt32 meshUnknown2 = 0;
                UInt32 meshUnknown3 = 0;
                UInt32 meshUnknown4 = 0;
                Int32 totalBytesPerVertex = 0;
                UInt32 indexSize = 0;

                //switch on mesh header
                if (dmodVersion == 3)
                {
                    materialIndex = binaryReader.ReadUInt32();
                    meshUnknown1 = binaryReader.ReadUInt32();
                    meshUnknown2 = binaryReader.ReadUInt32();
                    meshUnknown3 = binaryReader.ReadUInt32();
                    bytesPerVertex = binaryReader.ReadUInt32();
                    vertexCount = binaryReader.ReadUInt32();
                    indexSize = binaryReader.ReadUInt32();
                    indexCount = binaryReader.ReadUInt32();
                }
                else if (dmodVersion == 4)
                {
                    materialIndex = binaryReader.ReadUInt32();
                    meshUnknown1 = binaryReader.ReadUInt32();
                    meshUnknown2 = binaryReader.ReadUInt32();
                    meshUnknown3 = binaryReader.ReadUInt32();
                    vertexBlockCount = binaryReader.ReadUInt32();
                    indexSize = binaryReader.ReadUInt32();
                    indexCount = binaryReader.ReadUInt32();
                    vertexCount = binaryReader.ReadUInt32();
                }
                else
                {
                    return null;
                }

                totalBytesPerVertex += (Int32)bytesPerVertex;

                Mesh mesh = new Mesh((Int32)vertexCount, (Int32)indexCount);

                mesh.VertexBlockCount = vertexBlockCount;
                mesh.MaterialIndex = materialIndex;
                mesh.Unknown1 = meshUnknown1;
                mesh.Unknown2 = meshUnknown2;
                mesh.Unknown3 = meshUnknown3;
                mesh.Unknown4 = meshUnknown4;

                // read vertex data
                for (Int32 j = 0; j < vertexBlockCount; ++j)
                {
                    if (dmodVersion == 4)
                    {
                        bytesPerVertex = binaryReader.ReadUInt32();

                        totalBytesPerVertex += (Int32)bytesPerVertex;
                    }

                    for (Int32 k = 0; k < vertexCount; ++k)
                    {
                        mesh.Vertices[k].Data.AddRange(binaryReader.ReadBytes((Int32)bytesPerVertex));
                    }
                }

                mesh.BytesPerVertex = (Int32)totalBytesPerVertex;

                //TODO: fix hash function, lookups are failing
                //MaterialDefinition materialDefinition = MaterialDefinitionManager.Instance.MaterialDefinitions[model.Materials[(Int32)mesh.MaterialIndex].MaterialDefinitionHash];
                MaterialDefinition materialDefinition = MaterialDefinitionManager.Instance.MaterialDefinitions[Jenkins.OneAtATime("OcclusionModel")];
                VertexLayout vertexLayout = MaterialDefinitionManager.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                Int32 positionOffset = vertexLayout.GetOffsetFromDataUsageAndIndex(VertexLayout.Entry.DataUsages.Position, 0);

                // interpret vertex data
                for (Int32 j = 0; j < vertexCount; ++j)
                {
                    Vertex vertex = mesh.Vertices[j];
                    byte[] data = vertex.Data.ToArray();

                    //position
                    vertex.Position.X = BitConverter.ToSingle(data, positionOffset + 0);
                    vertex.Position.Y = BitConverter.ToSingle(data, positionOffset + 4);
                    vertex.Position.Z = BitConverter.ToSingle(data, positionOffset + 8);
                }

                // read indices
                for (Int32 j = 0; j < indexCount; ++j)
                {
                    UInt16 index = binaryReader.ReadUInt16();

                    mesh.Indices[j] = index;
                }

                //TODO: remove once we read these in from file
                // calculate normals
                for (Int32 j = 0; j < indexCount;)
                {
                    UInt16[] faceIndices = { mesh.Indices[j++], mesh.Indices[j++], mesh.Indices[j++] };

                    Vector3 vertex0 = mesh.Vertices[faceIndices[0]].Position;
                    Vector3 vertex1 = mesh.Vertices[faceIndices[1]].Position;
                    Vector3 vertex2 = mesh.Vertices[faceIndices[2]].Position;

                    Vector3 normal = Vector3.Cross((vertex1 - vertex0), (vertex2 - vertex0));

                    mesh.Vertices[faceIndices[0]].Normal += normal;
                    mesh.Vertices[faceIndices[1]].Normal += normal;
                    mesh.Vertices[faceIndices[2]].Normal += normal;
                }

                // normalize normals
                for (Int32 j = 0; j < vertexCount; ++j)
                {
                    mesh.Vertices[j].Normal.Normalize();
                }

                model.Meshes[i] = mesh;
            }

            return model;
        }

        public UInt32 Version = 0;
        public String Name = String.Empty;
        public UInt32 Unknown0 = 0;
        public UInt32 Unknown1 = 0;
        public UInt32 Unknown2 = 0;
        public Vector3 Min = Vector3.Zero;
        public Vector3 Max = Vector3.Zero;
        public List<Material> Materials = new List<Material>();
        public Mesh[] Meshes { get; private set; }

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
    }
}
