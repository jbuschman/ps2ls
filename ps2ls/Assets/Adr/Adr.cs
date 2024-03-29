﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ps2ls.Assets.Adr
{
    //Based on work by Herbert Harrison @ http://sktest.aruarose.com/binxml.h
    public class Adr
    {
        public class Tag
        {
            public byte ID { get; private set; }
            public byte[] Data { get; private set; }

            private Tag()
            {
            }

            public static Tag LoadFromStream(Stream stream)
            {
                BinaryReader binaryReader = new BinaryReader(stream);

                Tag tag = new Tag();

                tag.ID = binaryReader.ReadByte();

                byte b;
                uint size;

                b = binaryReader.ReadByte();

                if (b < 0x80)
                {
                    size = b;
                }
                else if (b == 0xFF)
                {
                    size = binaryReader.ReadUInt32();
                }
                else
                {
                    size = ((uint)b & 0x7F) << 8;

                    b = binaryReader.ReadByte();

                    size |= b;
                }

                tag.Data = binaryReader.ReadBytes((int)size);

                return tag;
            }
        }

        public List<Tag> Tags { get; private set; }

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
