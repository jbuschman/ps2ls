using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.IO
{
    public static class Utils
    {
        public static string ReadNullTerminatedStringFromStream(Stream stream)
        {
            if (stream == null)
                return String.Empty;

            BinaryReader binaryReader = new BinaryReader(stream);
            StringBuilder stringBuilder = new StringBuilder();

            char c = binaryReader.ReadChar();

            while (c != '\0')
            {
                stringBuilder.Append(c);
                c = binaryReader.ReadChar();
            }

            return stringBuilder.ToString();
        }
    }
}
