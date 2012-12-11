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
        }

        public void CreateBuffers()
        {
            GL.GenBuffers(1, out vertexBufferHandle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * Vertices.Length), Vertices.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.GenBuffers(1, out indexBufferHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBufferHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(UInt16) * Indices.Length), Indices.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public Vertex[] Vertices { get; private set; }
        public UInt16[] Indices { get; private set; }

        private Int32 vertexBufferHandle;
        public Int32 VertexBufferHandle { get { return vertexBufferHandle; } }

        private Int32 indexBufferHandle;
        public Int32 IndexBufferHandle { get { return vertexBufferHandle; } }
    }
}
