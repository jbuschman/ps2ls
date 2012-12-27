using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Files.Dme
{
    public class Vertex
    {
        public List<Byte> Data;

        public Vector3 Position;
        public Vector3 Normal;
        public List<Vector2> UVs;

        public Vertex()
        {
            UVs = new List<Vector2>();
            Data = new List<byte>();
        }
    }
}
