using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Assets.Zone
{
    public class Eco
    {
        public class Layer
        {
            public float Density { get; private set; }
            public float MinScale { get; private set; }
            public float MaxScale { get; private set; }
            public float SlopePeak { get; private set; }
            public float SlopeExtent { get; private set; }
            public float MinElevation { get; private set; }
            public float MaxElevation { get; private set; }
            public byte Unknown0 { get; private set; }
            public string Flora { get; private set; }
            public uint TintCount { get; private set; }

            public enum LoadError
            {
                None,
                NullStream
            }

            private Layer()
            {
            }

            public static LoadError LoadFromStream(Stream stream, out Layer layer)
            {
                if(stream == null)
                {
                    layer = null;
                    return LoadError.NullStream;
                }

                BinaryReader binaryReader = new BinaryReader(stream);

                layer = new Layer();
                layer.Density = binaryReader.ReadSingle();
                layer.MinScale = binaryReader.ReadSingle();
                layer.MaxScale = binaryReader.ReadSingle();
                layer.SlopePeak = binaryReader.ReadSingle();
                layer.SlopeExtent = binaryReader.ReadSingle();
                layer.MinElevation = binaryReader.ReadSingle();
                layer.MaxElevation = binaryReader.ReadSingle();
                layer.Unknown0 = binaryReader.ReadByte();
                layer.Flora = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
                layer.TintCount = binaryReader.ReadUInt32();

                for (int i = 0; i < layer.TintCount; ++i)
                {
                    binaryReader.ReadUInt32();  //unknown
                    binaryReader.ReadUInt32();  //unknown
                }

                return LoadError.None;
            }
        }

        public uint Unknown0 { get; private set; }
        public string Name { get; private set; }
        public string ColorNxMap { get; private set; }
        public string SpecNyMap { get; private set; }
        public uint DetailRepeat { get; private set; }
        public float BlendStrength { get; private set; }
        public float SpecMin { get; private set; }
        public float SpecMax { get; private set; }
        public float SpecSmoothnessMin { get; private set; }
        public float SpecSmoothnessMax { get; private set; }
        public string PhysicsMaterial { get; private set; }
        public List<Layer> Layers { get; private set; }

        public enum LoadError
        {
            None,
            NullStream,
            BadLayer
        }

        private Eco()
        {
        }

        public static LoadError LoadFromStream(Stream stream, out Eco eco)
        {
            if (stream == null)
            {
                eco = null;
                return LoadError.NullStream;
            }

            BinaryReader binaryReader = new BinaryReader(stream, Encoding.Default, true);

            eco = new Eco();

            binaryReader.ReadUInt32(); //unknown
            eco.Name = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
            eco.ColorNxMap = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
            eco.SpecNyMap = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
            eco.DetailRepeat = binaryReader.ReadUInt32();
            eco.BlendStrength = binaryReader.ReadSingle();
            eco.SpecMin = binaryReader.ReadSingle();
            eco.SpecMax = binaryReader.ReadSingle();
            eco.SpecSmoothnessMin = binaryReader.ReadSingle();
            eco.SpecSmoothnessMax = binaryReader.ReadSingle();
            eco.PhysicsMaterial = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);

            uint layerCount = binaryReader.ReadUInt32();
            eco.Layers = new List<Layer>((int)layerCount);

            for (uint i = 0; i < layerCount; ++i)
            {
                Layer layer;
                
                if(Layer.LoadFromStream(stream, out layer) != Layer.LoadError.None)
                {
                    eco = null;
                    return LoadError.BadLayer;
                }

                eco.Layers.Add(layer);
            }

            return LoadError.None;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
