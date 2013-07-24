using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ps2ls.Assets.Dme
{
    public struct BoneMapEntry
    {
        public ushort BoneIndex;
        public ushort GlobalIndex;

        public static BoneMapEntry LoadFromStream(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            BoneMapEntry boneMapEntry = new BoneMapEntry();

            boneMapEntry.BoneIndex = binaryReader.ReadUInt16();
            boneMapEntry.GlobalIndex = binaryReader.ReadUInt16();

            return boneMapEntry;
        }
    }

    public struct BoneMap
    {
        public uint Unknown0;
        public uint BoneStart;
        public uint BoneCount;
        public uint Delta;
        public uint Unknown1;
        public uint BoneEnd;
        public uint VertexCount;
        public uint Unknown2;
        public uint IndexCount;

        public static BoneMap LoadFromStream(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            BoneMap boneMap = new BoneMap();

            boneMap.Unknown0 = binaryReader.ReadUInt32();
            boneMap.BoneStart = binaryReader.ReadUInt32();
            boneMap.BoneCount = binaryReader.ReadUInt32();
            boneMap.Delta = binaryReader.ReadUInt32();
            boneMap.Unknown1 = binaryReader.ReadUInt32();
            boneMap.BoneEnd = binaryReader.ReadUInt32();
            boneMap.VertexCount = binaryReader.ReadUInt32();
            boneMap.Unknown2 = binaryReader.ReadUInt32();
            boneMap.IndexCount = binaryReader.ReadUInt32();

            return boneMap;
        }
    }
}
