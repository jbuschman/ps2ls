using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Assets.Zone
{
    public class Flora
    {
        public String Name { get; private set; }
        public String Texture { get; private set; }
        public String ModelFile { get; private set; }
        public Boolean Unknown0 { get; private set; }
        public Single Unknown1 { get; private set; }
        public Single Unknown2 { get; private set; }

        public enum LoadError
        {
            None,
            NullStream
        }

        private Flora()
        {
        }

        public static LoadError LoadFromStream(Stream stream, out Flora flora)
        {
            if (stream == null)
            {
                flora = null;
                return LoadError.NullStream;
            }

            BinaryReader binaryReader = new BinaryReader(stream);

            flora = new Flora();
            flora.Name = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
            flora.Texture = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
            flora.ModelFile = ps2ls.IO.Utils.ReadNullTerminatedStringFromStream(stream);
            flora.Unknown0 = binaryReader.ReadBoolean();
            flora.Unknown1 = binaryReader.ReadSingle();
            flora.Unknown2 = binaryReader.ReadSingle();

            return LoadError.None;
        }
    }
}
