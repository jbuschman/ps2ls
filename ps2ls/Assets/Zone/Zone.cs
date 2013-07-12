using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Assets.Zone
{
    public class Zone
    {
        public Int32 QuadsPerTile { get; private set; }
        public Single TileSize { get; private set; }
        public UInt32 Unknown1 { get; private set; }
        public UInt32 VerticesPerTile { get; private set; }
        public UInt32 TilesPerChunk { get; private set; }
        public Int32 StartX { get; private set; }
        public Int32 StartY { get; private set; }
        public UInt32 ChunksX { get; private set; }
        public UInt32 ChunksY { get; private set; }
        public Eco[] Ecos { get; private set; }
        public Flora[] Floras { get; private set; }
        public Actor[] Actors { get; private set; }

        public enum LoadError
        {
            None,
            NullStream,
            BadHeader,
            BadEco,
            BadFlora
        };

        private Zone()
        {
        }

        public static LoadError LoadFromStream(Stream stream, out Zone zone)
        {
            //https://github.com/RoyAwesome/ps2ls/wiki/Zone
            if (stream == null)
            {
                zone = null;
                return LoadError.NullStream;
            }

            BinaryReader binaryReader = new BinaryReader(stream);

            byte[] magic = binaryReader.ReadBytes(4);

            if (magic[0] != 'Z' ||
                magic[1] != 'O' ||
                magic[2] != 'N' ||
                magic[3] != 'E')
            {
                zone = null;
                return LoadError.BadHeader;
            }

            //unknown
            byte[] unknown0 = binaryReader.ReadBytes(28);

            zone = new Zone();
            zone.QuadsPerTile = binaryReader.ReadInt32();
            zone.TileSize = binaryReader.ReadSingle();
            zone.Unknown1 = binaryReader.ReadUInt32();
            zone.VerticesPerTile = binaryReader.ReadUInt32();
            zone.TilesPerChunk = binaryReader.ReadUInt32();
            zone.StartX = binaryReader.ReadInt32();
            zone.StartY = binaryReader.ReadInt32();
            zone.ChunksX = binaryReader.ReadUInt32();
            zone.ChunksY = binaryReader.ReadUInt32();

            //ecos
            UInt32 ecoCount = binaryReader.ReadUInt32();
            zone.Ecos = new Eco[ecoCount];

            for (UInt32 i = 0; i < ecoCount; ++i)
            {
                Eco eco;
                if (Eco.LoadFromStream(stream, out eco) != Eco.LoadError.None)
                {
                    zone = null;
                    return LoadError.BadEco;
                }

                zone.Ecos[i] = eco;
            }

            //flora
            UInt32 floraCount = binaryReader.ReadUInt32();
            zone.Floras = new Flora[floraCount];

            for (UInt32 i = 0; i < floraCount; ++i)
            {
                Flora flora;
                if(Flora.LoadFromStream(stream, out flora) != Flora.LoadError.None)
                {
                    zone = null;
                    return LoadError.BadFlora;
                }

                zone.Floras[i] = flora;
            }

            //actors
            UInt32 actorCount = binaryReader.ReadUInt32();
            zone.Actors = new Actor[actorCount];

            for (UInt32 i = 0; i < floraCount; ++i)
            {
                Actor actor;
                if (Actor.LoadFromStream(stream, out actor) != Actor.LoadError.None)
                {
                    zone = null;
                    return LoadError.BadFlora;
                }

                zone.Actors[i] = actor;
            }

            return LoadError.None;
        }
    }
}
