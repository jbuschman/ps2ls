using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK;

namespace ps2ls.Dme
{
    public class Model
    {
        public struct ExportOptions
        {
            public Boolean InvertX;
            public Boolean InvertY;
            public Boolean InvertZ;
            public Boolean SwapXY;
            public Boolean SwapXZ;
            public Boolean SwapYZ;
            public Boolean ExportNormals;
            public Single Scale;
        }

        public static Model LoadFromStream(String name, Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            byte[] magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'D' ||
                magic[1] != 'M' ||
                magic[2] != 'O' ||
                magic[3] != 'D')
            {
                return null;
            }

            UInt32 dmodVersion = binaryReader.ReadUInt32();

            // only handle version 4 for now
            if (dmodVersion != 3 && dmodVersion != 4)
            {
                return null;
            }

            UInt32 modelHeaderOffset = binaryReader.ReadUInt32();

            magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'D' ||
                magic[1] != 'M' ||
                magic[2] != 'A' ||
                magic[3] != 'T')
            {
                return null;
            }

            UInt32 dmatVersion = binaryReader.ReadUInt32();
            UInt32 dmatLength = binaryReader.ReadUInt32();

            Model model = new Model();

            model.Name = name;

            char[] buffer = binaryReader.ReadChars((Int32)dmatLength);
            List<String> materialNames = new List<string>();
            Int32 startIndex = 0;

            for (Int32 i = 0; i < buffer.Count(); ++i)
            {
                if (buffer[i] == '\0')
                {
                    Int32 length = i - startIndex;

                    String materialName = new String(buffer, startIndex, length);
                    startIndex = i + 1;

                    materialNames.Add(materialName);
                }
            }

            UInt32 unknown0 = binaryReader.ReadUInt32();
            UInt32 unknown1 = binaryReader.ReadUInt32();
            UInt32 unknownBlockLength = binaryReader.ReadUInt32();

            binaryReader.BaseStream.Seek(modelHeaderOffset, SeekOrigin.Begin);
            binaryReader.BaseStream.Seek(12, SeekOrigin.Current);   //unknown

            //bounding box
            model.Min.X = binaryReader.ReadSingle();
            model.Min.Y = binaryReader.ReadSingle();
            model.Min.Z = binaryReader.ReadSingle();

            model.Max.X = binaryReader.ReadSingle();
            model.Max.Y = binaryReader.ReadSingle();
            model.Max.Z = binaryReader.ReadSingle();

            UInt32 meshCount = binaryReader.ReadUInt32();

            model.Meshes = new Mesh[meshCount];

            for (Int32 i = 0; i < meshCount; ++i)
            {
                UInt32 indexCount = 0;
                UInt32 vertexCount = 0;
                UInt32 bytesPerVertex = 0;
                UInt32 vertexBlockCount = 1;

                UInt32 unknown2 = 0;

                //mesh header
                if (dmodVersion == 3)
                {
                    binaryReader.BaseStream.Seek(16, SeekOrigin.Current);   //unknown
                    bytesPerVertex = binaryReader.ReadUInt32();
                    vertexCount = binaryReader.ReadUInt32();
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);   //unknown
                    indexCount = binaryReader.ReadUInt32();
                }
                else if (dmodVersion == 4)
                {
                    binaryReader.BaseStream.Seek(16, SeekOrigin.Current);   //unknown

                    vertexBlockCount = binaryReader.ReadUInt32();

                    unknown2 = binaryReader.ReadUInt32();

                    indexCount = binaryReader.ReadUInt32();
                    vertexCount = binaryReader.ReadUInt32();
                }
                else
                {
                    return null;
                }

                Mesh mesh = new Mesh((Int32)vertexCount, (Int32)indexCount);

                // read vertex data
                for (Int32 j = 0; j < vertexBlockCount; ++j)
                {
                    if (dmodVersion == 4)
                    {
                        bytesPerVertex = binaryReader.ReadUInt32();
                    }

                    for (Int32 k = 0; k < vertexCount; ++k)
                    {
                        mesh.Vertices[k].Data.AddRange(binaryReader.ReadBytes((Int32)bytesPerVertex));
                    }
                }

                // interpret vertex data
                for (Int32 j = 0; j < vertexCount; ++j)
                {
                    Vertex vertex = mesh.Vertices[j];
                    byte[] data = vertex.Data.ToArray();

                    vertex.Position.X = BitConverter.ToSingle(data, 0);
                    vertex.Position.Y = BitConverter.ToSingle(data, 4);
                    vertex.Position.Z = BitConverter.ToSingle(data, 8);
                }

                // read indices
                for (Int32 j = 0; j < indexCount; ++j)
                {
                    UInt16 index = binaryReader.ReadUInt16();

                    mesh.Indices[j] = index;
                }

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

                //mesh.CreateBuffers();

                model.Meshes[i] = mesh;
            }

            return model;
        }

        public void ExportOBJToDirectoryWithOptions(string directory, ExportOptions options)
        {
        }

        public void ExportPLYToDirectoryWithOptions(string directory, ExportOptions options)
        {
        }

        public Mesh[] Meshes { get; private set; }
        public Vector3 Min;
        public Vector3 Max;
        public String Name;
    }
}
