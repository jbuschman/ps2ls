using ps2ls.Assets.Cnk;
using ps2ls.Assets.Pack;
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
        public int QuadsPerTile { get; private set; }
        public float TileSize { get; private set; }
        public uint Unknown1 { get; private set; }
        public uint VerticesPerTile { get; private set; }
        public uint TilesPerChunk { get; private set; }
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public uint ChunksX { get; private set; }
        public uint ChunksY { get; private set; }
        public Eco[] Ecos { get; private set; }
        public Flora[] Floras { get; private set; }
        public Actor[] Actors { get; private set; }
        public Cnk0[,] Cnk0s { get; private set; }

        public enum LoadError
        {
            None,
            NullStream,
            BadHeader,
            BadEco,
            BadFlora,
            BadCnk,
            BadActor
        };

        private Zone()
        {
        }

        public static LoadError LoadFromStream(string name, Stream stream, out Zone zone)
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
            uint ecoCount = binaryReader.ReadUInt32();
            zone.Ecos = new Eco[ecoCount];

            for (uint i = 0; i < ecoCount; ++i)
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
            uint floraCount = binaryReader.ReadUInt32();
            zone.Floras = new Flora[floraCount];

            for (uint i = 0; i < floraCount; ++i)
            {
                Flora flora;
                if(Flora.LoadFromStream(stream, out flora) != Flora.LoadError.None)
                {
                    zone = null;
                    return LoadError.BadFlora;
                }

                zone.Floras[i] = flora;
            }

            //invisible walls
            uint invisibleWallCount = binaryReader.ReadUInt32();
            for (uint i = 0; i < invisibleWallCount; ++i)
            {
                 binaryReader.ReadUInt32();
            }

            //todo: runtime objects (actors)

            //actors
            uint actorCount = binaryReader.ReadUInt32();
            zone.Actors = new Actor[actorCount];

            for (uint i = 0; i < actorCount; ++i)
            {
                Actor actor;
                if (Actor.LoadFromStream(stream, out actor) != Actor.LoadError.None)
                {
                    zone = null;
                    return LoadError.BadActor;
                }

                zone.Actors[i] = actor;
            }

            //todo: lights

            zone.Cnk0s = new Cnk0[32, 32];

            //cnk0s
            int z = 0;

            for (int x = zone.StartX; x < -zone.StartX; x += 4)
            {
                int w = 0;

                for (int y = zone.StartY; y < -zone.StartY; y += 4)
                {
                    string cnk0FileName = string.Format("{0}_{1}_{2}.cnk0", name, x, y);

                    Cnk0 cnk0 = null;
                    MemoryStream memoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(cnk0FileName);

                    if (memoryStream != null)
                    {
                        Cnk0.LoadError loadError = Cnk0.LoadFromStream(memoryStream, out cnk0);

                        if (loadError != Cnk0.LoadError.None)
                        {
                            zone = null;
                            return LoadError.BadCnk;
                        }
                    }

                    zone.Cnk0s[z, w++] = cnk0;

                    memoryStream.Close();
                }

                z++;
            }

            return LoadError.None;
        }
    }
}
