using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Assets.Zone
{
    public class Actor
    {
        public struct InstanceInfo
        {
            public Vector4 Translation;
            public Quaternion Rotation;
            public Vector4 Scale;
            public uint Unknown0;
            public byte Unknown1;
            public float Unknown2;
        }

        public string AdrFileName { get; private set; }
        public float Unknown0 { get; private set; }
        public InstanceInfo[] InstanceInfos { get; private set; }

        public enum LoadError
        {
            None,
            NullStream
        }

        public static LoadError LoadFromStream(Stream stream, out Actor actor)
        {
            if (stream == null)
            {
                actor = null;
                return LoadError.NullStream;
            }

            BinaryReader binaryReader = new BinaryReader(stream, Encoding.Default, true);

            actor = new Actor();
            actor.AdrFileName = IO.Utils.ReadNullTerminatedStringFromStream(stream);
            actor.Unknown0 = binaryReader.ReadSingle();

            uint placementCounts = binaryReader.ReadUInt32();

            for(uint i = 0; i < placementCounts; ++i)
            {
                InstanceInfo placementInfo = new InstanceInfo();
                placementInfo.Translation = new Vector4();
                placementInfo.Translation.X = binaryReader.ReadSingle();
                placementInfo.Translation.Y = binaryReader.ReadSingle();
                placementInfo.Translation.Z = binaryReader.ReadSingle();
                placementInfo.Translation.W = binaryReader.ReadSingle();
                placementInfo.Rotation = new Quaternion();
                placementInfo.Rotation.X = binaryReader.ReadSingle();
                placementInfo.Rotation.Y = binaryReader.ReadSingle();
                placementInfo.Rotation.Z = binaryReader.ReadSingle();
                placementInfo.Rotation.W = binaryReader.ReadSingle();
                placementInfo.Scale = new Vector4();
                placementInfo.Scale.X = binaryReader.ReadSingle();
                placementInfo.Scale.Y = binaryReader.ReadSingle();
                placementInfo.Scale.Z = binaryReader.ReadSingle();
                placementInfo.Scale.W = binaryReader.ReadSingle();
                placementInfo.Unknown0 = binaryReader.ReadUInt32();
                placementInfo.Unknown1 = binaryReader.ReadByte();
                placementInfo.Unknown2 = binaryReader.ReadSingle();
            }

            return LoadError.None;
        }
    }
}
