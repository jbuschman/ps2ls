using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ps2ls.Cryptography;
using System.Xml.XPath;

namespace ps2ls.Graphics.Materials
{
    public class VertexLayout
    {
        public class Entry
        {
            public enum EntryType
            {
                None = -1,
                Float3,
                D3dcolor,
                Float2,
                Float4,
                ubyte4n,
                float16_2,
                Short2,
                Float1,
            }

            public enum EntryUsage
            {
                None = -1,
                Position,
                Color,
                Texcoord,
                Tangent,
                Binormal,
                BlendWeight,
                BlendIndices
            }

            private static String[] usageStrings =
            {
                "Position",
                "Color",
                "Texcoord",
                "Tangent",
                "Binormal",
                "BlendWeight",
                "BlendIndices"
            };

            private static String[] typeStrings =
            {
                "Float3",
                "D3dcolor",
                "Float2",
                "Float4",
                "ubyte4n",
                "float16_2",
                "Short2",
                "Float1"
            };

            public static Int32[] typeSizes =
            {
                12, //Float3
                4,  //D3dcolor
                8,  //Float2
                16, //Float4
                4,  //ubyte4n
                8,  //float16_2
                4,  //Short2
                4   //Float1
            };

            public UInt32 Stream;
            public EntryType Type;
            public EntryUsage Usage;
            public UInt32 UsageIndex;

            public static void GetTypeFromString(String typeString, out EntryType type)
            {
                for (Int32 i = 0; i < typeStrings.Length; ++i)
                {
                    if (String.Compare(typeString, typeStrings[i], true) >= 0)
                    {
                        type = (EntryType)i;
                    }
                }

                type = EntryType.None;
            }

            public static void GetUsageFromString(String usageString, out EntryUsage usage)
            {
                for (Int32 i = 0; i < usageStrings.Length; ++i)
                {
                    if (String.Compare(usageString, usageStrings[i], true) >= 0)
                    {
                        usage = (EntryUsage)i;
                    }
                }

                usage = EntryUsage.None;
            }

            public static Int32 GetTypeSize(EntryType type)
            {
                return typeSizes[(Int32)type];
            }
        }

        public String Name { get; private set; }
        public UInt32 NameHash { get; private set; }
        public List<Entry> Entries { get; private set; }

        private VertexLayout()
        {
            Entries = new List<Entry>();
        }

        public static VertexLayout LoadFromXPathNavigator(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                return null;
            }

            VertexLayout vertexLayout = new VertexLayout();

            //name
            vertexLayout.Name = navigator.GetAttribute("Name", String.Empty);

            //name hash
            vertexLayout.NameHash = Jenkins.OneAtATime(vertexLayout.Name);

            //entries
            XPathNodeIterator entries = navigator.Select("./Array[@Name='Entries']/Object[@Class='LayoutEntry']");

            while (entries.MoveNext())
            {
                navigator = entries.Current;

                VertexLayout.Entry entry = new Entry();

                //stream
                entry.Stream = UInt32.Parse(navigator.GetAttribute("Stream", String.Empty));

                //type
                String typeString = navigator.GetAttribute("Type", String.Empty);
                Entry.GetTypeFromString(typeString, out entry.Type);

                //usage
                String usageString = navigator.GetAttribute("Usage", String.Empty);
                Entry.GetUsageFromString(usageString, out entry.Usage);

                //usage index
                entry.UsageIndex = UInt32.Parse(navigator.GetAttribute("UsageIndex", String.Empty));

                vertexLayout.Entries.Add(entry);
            }

            return vertexLayout;
        }

        public override string ToString()
        {
            return Name;
        }

        public Boolean HasEntryUsage(Entry.EntryUsage usage)
        {
            return GetEntryCountByUsage(usage) > 0;
        }

        public Int32 GetEntryCountByUsage(Entry.EntryUsage usage)
        {
            Int32 count = 0;

            foreach (Entry entry in Entries)
            {
                if (entry.Usage == usage)
                {
                    ++count;
                }
            }

            return count;
        }

        public Int32 GetOffsetFromEntryUsageAndIndex(Entry.EntryUsage usage, Int32 index)
        {
            Int32 offset = 0;

            foreach (Entry entry in Entries)
            {
                if (entry.Usage == usage)
                {
                    if (index == 0)
                    {
                        return offset;
                    }
                }

                offset += Entry.GetTypeSize(entry.Type);

                --index;
            }

            throw new ArgumentOutOfRangeException("index");
        }
    }
}
