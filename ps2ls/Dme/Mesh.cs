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
            Indices = new Int32[indexCount];
        }

        public Vertex[] Vertices { get; private set; }
        public Int32[] Indices { get; private set; }
    }
}
