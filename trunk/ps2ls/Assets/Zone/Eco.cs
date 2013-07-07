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
            public Single Density { get; private set; }
            public Single MinScale { get; private set; }
            public Single MaxScale { get; private set; }
            public Single SlopePeak { get; private set; }
            public Single SlopeExtent { get; private set; }
            public Single MinElevation { get; private set; }
            public Single MaxElevation { get; private set; }
            public String Flora { get; private set; }
            public UInt32 TintCount { get; private set; }

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

        public UInt32 Unknown0 { get; private set; }
        public String Name { get; private set; }
        public String ColorNxMap { get; private set; }
        public String SpecNyMap { get; private set; }
        public UInt32 DetailRepeat { get; private set; }
        public Single BlendStrength { get; private set; }
        public Single SpecMin { get; private set; }
        public Single SpecMax { get; private set; }
        public Single SpecSmoothnessMin { get; private set; }
        public Single SpecSmoothnessMax { get; private set; }
        public String PhysicsMaterial { get; private set; }
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

            BinaryReader binaryReader = new BinaryReader(stream);

            eco = new Eco();

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

            UInt32 layerCount = binaryReader.ReadUInt32();
            eco.Layers = new List<Layer>((int)layerCount);

            for (UInt32 i = 0; i < layerCount; ++i)
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
    }
}
