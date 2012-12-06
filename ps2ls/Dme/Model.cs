using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenTK.Math;

namespace ps2ls.Dme
{
    public class Model
    {
        public static Model LoadFromStream(Stream stream)
        {
            BinaryReaderBigEndian binaryReader = new BinaryReaderBigEndian(stream);

            byte[] magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'D' ||
                magic[1] != 'M' ||
                magic[2] != 'O' ||
                magic[3] != 'D')
            {
                return null;
            }

            Int32 dmodVersion = binaryReader.ReadInt32();

            // only handle version 4 for now
            if (dmodVersion != 3 && dmodVersion != 4)
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
            Int32 dmatLength = binaryReader.ReadInt32();

            Model model = new Model();

            char[] buffer = binaryReader.ReadChars(dmatLength);
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

            Int32 unknown0 = binaryReader.ReadInt32();
            Int32 unknown1 = binaryReader.ReadInt32();
            Int32 unknownBlockLength = binaryReader.ReadInt32();

            binaryReader.BaseStream.Seek(modelHeaderOffset, SeekOrigin.Begin);
            binaryReader.BaseStream.Seek(12, SeekOrigin.Current);   //unknown

            //bounding box
            model.AABB.Min.X = binaryReader.ReadSingle();
            model.AABB.Min.Y = binaryReader.ReadSingle();
            model.AABB.Min.Z = binaryReader.ReadSingle();

            model.AABB.Max.X = binaryReader.ReadSingle();
            model.AABB.Max.Y = binaryReader.ReadSingle();
            model.AABB.Max.Z = binaryReader.ReadSingle();

            Int32 meshCount = binaryReader.ReadInt32();

            model.Meshes = new Mesh[meshCount];

            for (Int32 i = 0; i < meshCount; ++i)
            {
                Int32 indexCount = 0;
                Int32 vertexCount = 0;
                Int32 bytesPerVertex = 0;

                //mesh header
                if (dmodVersion == 3)
                {
                    binaryReader.BaseStream.Seek(16, SeekOrigin.Current);   //unknown
                    bytesPerVertex = binaryReader.ReadInt32();
                    vertexCount = binaryReader.ReadInt32();
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);    //unknown
                }
                else if(dmodVersion == 4)
                {
                    binaryReader.BaseStream.Seek(24, SeekOrigin.Current);   //unknown
                    indexCount = binaryReader.ReadInt32();
                    vertexCount = binaryReader.ReadInt32();
                    bytesPerVertex = binaryReader.ReadInt32();
                }

                Mesh mesh = new Mesh(vertexCount, indexCount);

                //primary vertex block
                for (Int32 j = 0; j < vertexCount; ++j)
                {
                    Vertex vertex;
                    vertex.Position.X = binaryReader.ReadSingle();
                    vertex.Position.Y = binaryReader.ReadSingle();
                    vertex.Position.Z = binaryReader.ReadSingle();

                    if (vertexCount > 12)
                    {
                        binaryReader.BaseStream.Seek(vertexCount - 12, SeekOrigin.Current); //unknown
                    }
                }

                //secondary vertex block
                if (dmodVersion == 4)
                {
                    bytesPerVertex = binaryReader.ReadInt32();

                    for (Int32 j = 0; j < vertexCount; ++j)
                    {
                        binaryReader.BaseStream.Seek(bytesPerVertex, SeekOrigin.Current);
                    }
                }

                //indices
                for (Int32 j = 0; j < indexCount; ++j)
                {
                    UInt16 index = binaryReader.ReadUInt16();

                    mesh.Indices[j] = index;
                }

                model.Meshes[i] = mesh;
            }

            return model;
        }

        public Mesh[] Meshes { get; private set; }
        public AABB AABB { get; private set; }
    }
}
