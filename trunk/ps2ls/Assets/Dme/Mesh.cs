using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ps2ls.Graphics.Materials;
using OpenTK;

namespace ps2ls.Assets.Dme
{
    public class Mesh
    {
        public class VertexStream : MemoryStream
        {
            public VertexStream(int bytesPerVertex, byte[] data)
            {
                BytesPerVertex = bytesPerVertex;
                Data = data;
            }

            public int BytesPerVertex { get; private set; }
            public byte[] Data { get; private set; }

            public void ReadHalf2s(ICollection<Vector2> half2s, int offset, int startVertexIndex, int vertexCount)
            {
                Vector2 half2;

                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                {
                    half2.X = Half.FromBytes(Data, (vertexIndex * BytesPerVertex) + offset + 0).ToSingle();
                    half2.Y = Half.FromBytes(Data, (vertexIndex * BytesPerVertex) + offset + 2).ToSingle();
                    half2s.Add(half2);
                }
            }

            public void ReadFloat1s(ICollection<float> float1s, int offset, int startVertexIndex, int vertexCount)
            {
                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                    float1s.Add(BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset));
            }

            public void ReadFloat2s(ICollection<Vector2> float2s, int offset, int startVertexIndex, int vertexCount)
            {
                Vector2 float2;

                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                {
                    float2.X = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 0);
                    float2.Y = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 4);
                    float2s.Add(float2);
                }
            }

            public void ReadFloat3s(ICollection<Vector3> float3s, int offset, int startVertexIndex, int vertexCount)
            {
                Vector3 float3;

                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                {
                    float3.X = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 0);
                    float3.Y = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 4);
                    float3.Z = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 8);
                    float3s.Add(float3);
                }
            }

            public void ReadFloat4s(ICollection<Vector4> float4s, int offset, int startVertexIndex, int vertexCount)
            {
                Vector4 float4;

                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                {
                    float4.X = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 0);
                    float4.Y = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 4);
                    float4.Z = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 8);
                    float4.W = BitConverter.ToSingle(Data, (vertexIndex * BytesPerVertex) + offset + 12);
                    float4s.Add(float4);
                }
            }

            public void ReadUByte4Ns(ICollection<Vector3> ubyte4ns, int offset, int startVertexIndex, int vertexCount)
            {
                Vector3 ubyte4n;

                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                {
                    ubyte4n.X = (float)(Data[vertexIndex * BytesPerVertex + offset + 0] - 127) / 128.0f;
                    ubyte4n.Y = (float)(Data[vertexIndex * BytesPerVertex + offset + 1] - 127) / 128.0f;
                    ubyte4n.Z = (float)(Data[vertexIndex * BytesPerVertex + offset + 2] - 127) / 128.0f;
                    ubyte4ns.Add(ubyte4n);
                }
            }

            public void ReadUByte4Ns(ICollection<Vector4> ubyte4ns, int offset, int startVertexIndex, int vertexCount)
            {
                Vector4 ubyte4n;

                for (int vertexIndex = startVertexIndex; vertexIndex < startVertexIndex + vertexCount; ++vertexIndex)
                {
                    ubyte4n.X = (float)(Data[vertexIndex * BytesPerVertex + offset + 0] - 127) / 128.0f;
                    ubyte4n.Y = (float)(Data[vertexIndex * BytesPerVertex + offset + 1] - 127) / 128.0f;
                    ubyte4n.Z = (float)(Data[vertexIndex * BytesPerVertex + offset + 2] - 127) / 128.0f;
                    ubyte4n.W = (float)(Data[vertexIndex * BytesPerVertex + offset + 3] - 127) / 128.0f;
                    ubyte4ns.Add(ubyte4n);
                }
            }
        }

        public Model Model { get; private set; }
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
        public int Texture { get; private set; }

        private Mesh(Model model)
        {
            this.Model = model;
        }

        public static Mesh LoadFromStream(Model model, Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            uint bytesPerVertex = 0;
            uint vertexStreamCount = 0;

            Mesh mesh = new Mesh(model);
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
                byte[] buffer = binaryReader.ReadBytes((int)mesh.VertexCount * (int)bytesPerVertex);
                VertexStream vertexStream = new VertexStream((int)bytesPerVertex, buffer);

                if (vertexStream != null)
                    mesh.VertexStreams[j] = vertexStream;
            }

            // read indices
            mesh.IndexData = binaryReader.ReadBytes((int)mesh.IndexCount * (int)mesh.IndexSize);

            MaterialDefinition materialDefinition = MaterialDefinitionLibrary.Instance.GetMaterialDefinitionFromHash(model.Materials[(int)mesh.MaterialIndex].MaterialDefinitionHash);
            string effectName = materialDefinition.DrawStyles[0].Effect;

            return mesh;
        }

        public VertexLayout GetVertexLayout(int drawStyleIndex)
        {
            MaterialDefinition materialDefinition = MaterialDefinitionLibrary.Instance.MaterialDefinitions[Model.Materials[(int)MaterialIndex].MaterialDefinitionHash];
            if (materialDefinition == null)
                return null;

            return MaterialDefinitionLibrary.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];
        }

        public bool GetPositions(out List<Vector3> positions, int usageIndex)
        {
            positions = null;

            VertexLayout vertexLayout = GetVertexLayout(0);
            if (vertexLayout == null)
                return false;

            VertexLayout.Entry.DataTypes dataType;
            int streamOffset;
            int streamIndex;

            bool positionExists = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Position, usageIndex, out dataType, out streamIndex, out streamOffset);
            if (!positionExists)
                return false;

            positions = new List<Vector3>((int)VertexCount);
            Mesh.VertexStream vertexStream = VertexStreams[streamIndex];

            switch (dataType)
            {
                case VertexLayout.Entry.DataTypes.Float3:
                    {
                        vertexStream.ReadFloat3s(positions, streamOffset, 0, (int)VertexCount);
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public bool GetTexCoords(out Vector2[] texCoords, int usageIndex)
        {
            texCoords = null;

            VertexLayout vertexLayout = GetVertexLayout(0);
            if (vertexLayout == null)
                return false;

            VertexLayout.Entry.DataTypes dataType;
            int streamOffset;
            int streamIndex;

            bool texcoordExists = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Texcoord, usageIndex, out dataType, out streamIndex, out streamOffset);
            if (!texcoordExists)
                return false;

            texCoords = new Vector2[VertexCount];
            Mesh.VertexStream vertexStream = VertexStreams[streamIndex];

            switch (dataType)
            {
                case VertexLayout.Entry.DataTypes.Float2:
                    {
                        vertexStream.ReadFloat2s(texCoords, streamOffset, 0, (int)VertexCount);
                    }
                    break;
                case VertexLayout.Entry.DataTypes.float16_2:
                    {
                        vertexStream.ReadHalf2s(texCoords, streamOffset, 0, (int)VertexCount);
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public bool GetNormals(out Vector3[] normals, int usageIndex)
        {
            normals = null;

            VertexLayout vertexLayout = GetVertexLayout(0);
            if (vertexLayout == null)
                return false;

            VertexLayout.Entry.DataTypes dataType;
            int streamOffset;
            int streamIndex;

            //todo: if normal does not exist, we can determine it from the tangent and binormal, if it exists
            bool normalExists = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Normal, usageIndex, out dataType, out streamIndex, out streamOffset);
            if (!normalExists)
                return false;

            normals = new Vector3[VertexCount];
            Mesh.VertexStream vertexStream = VertexStreams[streamIndex];

            switch (dataType)
            {
                case VertexLayout.Entry.DataTypes.Float3:
                    {
                        vertexStream.ReadFloat3s(normals, streamOffset, 0, (int)VertexCount);
                    }
                    break;
                case VertexLayout.Entry.DataTypes.ubyte4n:
                    {
                        vertexStream.ReadUByte4Ns(normals, streamOffset, 0, (int)VertexCount);
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public bool GetIndices(out uint[] indices)
        {
            indices = new uint[IndexCount];

            switch (IndexSize)
            {
                case 2:
                    for (int i = 0; i < IndexCount; ++i)
                        indices[i] = BitConverter.ToUInt16(IndexData, i * 2);
                    break;
                case 4:
                    for (int i = 0; i < IndexCount; ++i)
                        indices[i] = BitConverter.ToUInt32(IndexData, i * 4);
                    break;
            }

            return true;
        }

        public uint TriangleCount
        {
            get { return IndexCount / 3; }
        }
    }
}
