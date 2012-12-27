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

        public Int32 VertexCount { get; private set; }
        public Int32 IndexCount { get; private set; }
        public Int32 BytesPerVertex { get; set; }
        public Int32 VertexBlockCount { get; set; }
    }
}
