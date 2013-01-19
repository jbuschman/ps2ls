using System;
using System.Collections.Generic;
using System.IO;

namespace ps2ls.Assets.Adr
{
    public class Adr
    {
        public class Tag
        {
            Byte ID { get; private set; }
            Byte[] Data { get; private set; }

            private Tag()
            {
            }

            public static Tag LoadFromStream(Stream stream)
            {
                BinaryReader binaryReader = new BinaryReader(stream);

                Tag tag = new Tag();

                tag.ID = binaryReader.ReadByte();

                UInt32 size = binaryReader.ReadUInt32();
                tag.Data = binaryReader.ReadBytes((Int32)size);

                return tag;
            }
        }

        List<Tag> Tags { get; private set; }

        private Adr()
        {
            Tags = new List<Tag>();
        }

        public static Adr LoadFromStream(Stream stream)
        {
            if (stream == null)
                return null;

            Adr adr = new Adr();

            while (stream.Position < stream.Length)
            {
                Tag tag = Tag.LoadFromStream(stream);

                adr.Tags.Add(tag);
            }

            return adr;
        }
    }
}
