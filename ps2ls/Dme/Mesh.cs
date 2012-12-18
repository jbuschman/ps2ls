using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace ps2ls.Dme
{
    public class Mesh
    {
        public Mesh(Int32 vertexCount, Int32 indexCount)
        {
            Vertices = new Vertex[vertexCount];
            Indices = new UInt16[indexCount];

            for (Int32 i = 0; i < vertexCount; ++i)
            {
                Vertices[i] = new Vertex();
            }
        }

        public Vertex[] Vertices { get; private set; }
        public UInt16[] Indices { get; private set; }
    }
}
