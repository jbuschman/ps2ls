using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ps2ls.Graphics.Materials;
using ps2ls.Cryptography;
using OpenTK;

namespace ps2ls.Assets.Dme
{
    public class Mesh
    {
        public class VertexStream
        {
            public static VertexStream LoadFromStream(Stream stream, int vertexCount, int bytesPerVertex)
            {
                BinaryReader binaryReader = new BinaryReader(stream);

                VertexStream vertexStream = new VertexStream();
                vertexStream.BytesPerVertex = bytesPerVertex;
                vertexStream.Data = binaryReader.ReadBytes(vertexCount * bytesPerVertex);

                return vertexStream;
            }

            public int BytesPerVertex { get; private set; }
            public byte[] Data { get; private set; }
        }

        public VertexStream[] VertexStreams { get; private set; }
        public byte[] IndexData { get; private set; }
        public uint MaterialIndex { get; private set; }
        public uint Unknown1 { get; private set; }
        public uint Unknown2 { get; private set; }
        public uint Unknown3 { get; private set; }
        public uint Unknown4 { get; private set; }
        public uint VertexCount { get; private set; }
        public uint IndexCount { get; private set; }
        public uint IndexSize { get; private set; }

        private Mesh()
        {
        }

        public static Mesh LoadFromStream(Stream stream, ICollection<Dma.Material> materials)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            uint bytesPerVertex = 0;
            uint vertexStreamCount = 0;

            Mesh mesh = new Mesh();
            mesh.MaterialIndex = binaryReader.ReadUInt32();
            mesh.Unknown1 = binaryReader.ReadUInt32();
            mesh.Unknown2 = binaryReader.ReadUInt32();
            mesh.Unknown3 = binaryReader.ReadUInt32();
            vertexStreamCount = binaryReader.ReadUInt32();
            mesh.IndexSize = binaryReader.ReadUInt32();
            mesh.IndexCount = binaryReader.ReadUInt32();
            mesh.VertexCount = binaryReader.ReadUInt32();

            mesh.VertexStreams = new VertexStream[(int)vertexStreamCount];

            // read vertex streams
            for (int j = 0; j < vertexStreamCount; ++j)
            {
                bytesPerVertex = binaryReader.ReadUInt32();

                VertexStream vertexStream = VertexStream.LoadFromStream(binaryReader.BaseStream, (int)mesh.VertexCount, (int)bytesPerVertex);

                if (vertexStream != null)
                    mesh.VertexStreams[j] = vertexStream;
            }

            // read indices
            mesh.IndexData = binaryReader.ReadBytes((int)mesh.IndexCount * (int)mesh.IndexSize);

            return mesh;
        }
    }
}
