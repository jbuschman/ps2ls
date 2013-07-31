using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Assets.Dme
{
    public class VertexStream : Stream
    {
        private byte[] buffer;

        public VertexStream(Stream stream, int vertexCount, int bytesPerVertex)
        {
            BinaryReader binaryReader = new BinaryReader(stream);

            BytesPerVertex = bytesPerVertex;
            buffer = binaryReader.ReadBytes(vertexCount * bytesPerVertex);
        }

        public int BytesPerVertex { get; private set; }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new InvalidOperationException("Cannot flush vertex stream.");
        }

        public override long Length
        {
            get { return buffer.GetLongLength(0); }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (offset < 0 || offset >= Length)
                throw new ArgumentOutOfRangeException("offset");

            if (count < 0 || offset + count > Length)
                throw new ArgumentOutOfRangeException("count");

            Array.ConstrainedCopy(this.buffer, offset, buffer, 0, count);

            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.End:
                    Position = Length - 1 + offset;
                    break;
            }

            return Position;
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException("Vertex stream cannot have its length set outside of the constructor.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException("Vertex stream cannot be written to.");
        }
    }
}
