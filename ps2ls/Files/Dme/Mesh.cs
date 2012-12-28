using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace ps2ls.Files.Dme
{
    public class Mesh
    {
        public Mesh(Int32 vertexCount, Int32 indexCount)
        {
            VertexCount = vertexCount;
            IndexCount = indexCount;
            Vertices = new Vertex[vertexCount];
            Indices = new UInt16[indexCount];

            for (Int32 i = 0; i < vertexCount; ++i)
            {
                Vertices[i] = new Vertex();
            }
        }

        public Vertex[] Vertices { get; private set; }
        public UInt16[] Indices { get; private set; }

        public UInt32 Index { get; set; }
        public UInt32 Unknown1 { get; set; }
        public UInt32 Unknown2 { get; set; }
        public UInt32 Unknown3 { get; set; }
        public UInt32 Unknown4 { get; set; }
        public Int32 VertexCount { get; set; }
        public Int32 IndexCount { get; private set; }
        public Int32 BytesPerVertex { get; set; }
        public UInt32 VertexBlockCount { get; set; }
    }
}
