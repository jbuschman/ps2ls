using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ps2ls.IO;
using System.Runtime.InteropServices;
using lzhamNET;

namespace ps2ls.Assets.Cnk
{
    public class Cnk0
    {
        public const int VERSION = 1;

        private Cnk0()
        {
        }

        public static Cnk0 LoadFromStream(Stream stream)
        {
            if (stream == null)
                return null;

            BinaryReader binaryReader = new BinaryReader(stream);

            //header
            char[] magic = binaryReader.ReadChars(4);

            if (magic[0] != 'C' ||
                magic[1] != 'N' ||
                magic[2] != 'K' ||
                magic[3] != '0')
            {
                return null;
            }

            int version = binaryReader.ReadInt32();

            if (version != VERSION)
                return null;

            uint uncompressedSize = binaryReader.ReadUInt32();
            uint compressedSize = binaryReader.ReadUInt32();

            byte[] inputBuffer = binaryReader.ReadBytes((int)compressedSize);
            ulong inputBufferSize = compressedSize;

            byte[] outputBuffer = new byte[uncompressedSize];
            ulong outputBufferSize = 0;

            LZHAM.DecompressParams decompressParams = new LZHAM.DecompressParams();
            decompressParams.ComputeAdler32 = false;
            decompressParams.DictSizeLog2 = LZHAM.MinDictSizeLog2;
            decompressParams.NumSeedBytes = 0;
            decompressParams.OutputUnbuffered = true;
            decompressParams.SeedBytes = IntPtr.Zero; 
            IntPtr decompressState = LZHAM.DecompressInitialize(decompressParams);

            LZHAM.DecompressStatus decompressStatus = LZHAM.Decompress(decompressState, inputBuffer, inputBufferSize, outputBuffer, ref outputBufferSize, true);

            Cnk0 cnk0 = new Cnk0();
            return cnk0;
        }
    }
}
