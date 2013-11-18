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
        public struct Instance
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
        public Instance[] Instances { get; private set; }

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

            uint instanceCount = binaryReader.ReadUInt32();

            actor.Instances = new Instance[instanceCount];

            for(uint i = 0; i < instanceCount; ++i)
            {
                Instance instance = new Instance();
                instance.Translation = new Vector4();
                instance.Translation.X = binaryReader.ReadSingle();
                instance.Translation.Y = binaryReader.ReadSingle();
                instance.Translation.Z = binaryReader.ReadSingle();
                instance.Translation.W = binaryReader.ReadSingle();
                instance.Rotation = new Quaternion();
                instance.Rotation.X = binaryReader.ReadSingle();
                instance.Rotation.Y = binaryReader.ReadSingle();
                instance.Rotation.Z = binaryReader.ReadSingle();
                instance.Rotation.W = binaryReader.ReadSingle();
                instance.Scale = new Vector4();
                instance.Scale.X = binaryReader.ReadSingle();
                instance.Scale.Y = binaryReader.ReadSingle();
                instance.Scale.Z = binaryReader.ReadSingle();
                instance.Scale.W = binaryReader.ReadSingle();
                instance.Unknown0 = binaryReader.ReadUInt32();
                instance.Unknown1 = binaryReader.ReadByte();
                instance.Unknown2 = binaryReader.ReadSingle();

                actor.Instances[i] = instance;
            }

            return LoadError.None;
        }

        public override string ToString()
        {
            return AdrFileName;
        }
    }
}
