using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Reflection;

namespace lzhamNET
{
    public static class Lzham
    {
        public enum ZFlush
        {
            /// <summary>
            /// compression/decompression
            /// </summary>
            NoFlush = 0,
            /// <summary>
            /// compression/decompression, same as LZHAM_Z_SYNC_FLUSH
            /// </summary>
            PartialFlush = 1,
            /// <summary>
            /// compression/decompression, when compressing: flush current block (if any), always outputs sync block (aligns output to byte boundary, a 0xFFFF0000 marker will appear in the output stream)
            /// </summary>
            SyncFlush = 2,
            /// <summary>
            /// compression/decompression, when compressing: same as LZHAM_Z_SYNC_FLUSH but also forces a full state flush (LZ dictionary, all symbol statistics)
            /// </summary>
            FullFlush = 3,
            /// <summary>
            /// compression/decompression
            /// </summary>
            Finish = 4,
            /// <summary>
            /// not supported
            /// </summary>
            Block = 5,
            /// <summary>
            /// compression only, resets all symbol table update rates to maximum frequency (LZHAM extension)
            /// </summary>
            TableFlush = 10
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ZStream
        {
            /// <summary>
            /// pointer to next byte to read
            /// </summary>
            public IntPtr NextInputByte;
            /// <summary>
            /// number of bytes available at next_in
            /// </summary>
            public UInt32 AvailableInputBytes;
            /// <summary>
            /// total number of bytes consumed so far
            /// </summary>
            public UInt32 TotalInputBytes;
            /// <summary>
            /// pointer to next byte to write
            /// </summary>
            public IntPtr NextOutputByte;
            /// <summary>
            /// number of bytes that can be written to next_out
            /// </summary>
            public UInt32 AvailableOutputBytes;
            /// <summary>
            /// total number of bytes produced so far
            /// </summary>
            public UInt32 TotalOutputBytes;
            /// <summary>
            /// error msg (unused)
            /// </summary>
            public IntPtr ErrorMessage;
            /// <summary>
            /// internal state, allocated by zalloc/zfree
            /// </summary>
            public IntPtr State;
            /// <summary>
            /// optional heap allocation function (defaults to malloc)
            /// </summary>
            public IntPtr ZAllocFunc;
            /// <summary>
            /// optional heap free function (defaults to free)
            /// </summary>
            public IntPtr ZFreeFunc;
            /// <summary>
            /// heap alloc function user pointer
            /// </summary>
            public IntPtr Opaque;
            /// <summary>
            /// data_type (unused)
            /// </summary>
            public Int32 DataType;
            /// <summary>
            /// adler32 of the source or uncompressed data
            /// </summary>
            public UInt32 Adler32;
            /// <summary>
            /// not used
            /// </summary>
            public UInt32 Reserved;
        }

        /// <summary>
        /// lzham_z_inflateInit2() is like lzham_z_inflateInit() with an additional option that controls the window size and whether or not the stream has been wrapped with a zlib header/footer:
        /// window_bits must be LZHAM_Z_DEFAULT_WINDOW_BITS (to parse zlib header/footer) or -LZHAM_Z_DEFAULT_WINDOW_BITS (raw stream with no zlib header/footer).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_z_inflateInit2", CallingConvention = CallingConvention.Cdecl)]
        private extern static Int32 ZInflateInit2(IntPtr pStream, Int32 window_bits);
        public static Int32 ZInflateInit2(ZStream zStream, Int32 windowBits)
        {
            GCHandle zStreamGCHandle = GCHandle.Alloc(zStream, GCHandleType.Pinned);
            IntPtr zStreamIntPtr = zStreamGCHandle.AddrOfPinnedObject();

            Int32 result = ZInflateInit2(zStreamIntPtr, windowBits);

            zStreamGCHandle.Free();

            return result;
        }

        /// <summary>
        /// Deinitializes a decompressor.
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_z_inflateEnd", CallingConvention = CallingConvention.Cdecl)]
        private extern static Int32 ZInflateEnd(IntPtr pStream);
        public static Int32 ZInflateEnd(ZStream zStream)
        {
            GCHandle zStreamGCHandle = GCHandle.Alloc(zStream, GCHandleType.Pinned);

            Int32 result = ZInflateEnd(zStreamGCHandle.AddrOfPinnedObject());

            zStreamGCHandle.Free();

            return result;
        }

        /// <summary>
        /// Decompresses the input stream to the output, consuming only as much of the input as needed, and writing as much to the output as possible.
        /// Parameters:
        ///   pStream is the stream to read from and write to. You must initialize/update the next_in, avail_in, next_out, and avail_out members.
        ///   flush may be LZHAM_Z_NO_FLUSH, LZHAM_Z_SYNC_FLUSH, or LZHAM_Z_FINISH.
        ///   On the first call, if flush is LZHAM_Z_FINISH it's assumed the input and output buffers are both sized large enough to decompress the entire stream in a single call (this is slightly faster).
        ///   LZHAM_Z_FINISH implies that there are no more source bytes available beside what's already in the input buffer, and that the output buffer is large enough to hold the rest of the decompressed data.
        /// Return values:
        ///   LZHAM_Z_OK on success. Either more input is needed but not available, and/or there's more output to be written but the output buffer is full.
        ///   LZHAM_Z_STREAM_END if all needed input has been consumed and all output bytes have been written. For zlib streams, the adler-32 of the decompressed data has also been verified.
        ///   LZHAM_Z_STREAM_ERROR if the stream is bogus.
        ///   LZHAM_Z_DATA_ERROR if the deflate stream is invalid.
        ///   LZHAM_Z_PARAM_ERROR if one of the parameters is invalid.
        ///   LZHAM_Z_BUF_ERROR if no forward progress is possible because the input buffer is empty but the inflater needs more input to continue, or if the output buffer is not large enough. Call lzham_inflate() again
        ///   with more input data, or with more room in the output buffer (except when using single call decompression, described above).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_z_inflate", CallingConvention = CallingConvention.Cdecl)]
        private extern static Int32 ZInflate(IntPtr pStream, int flush);
        public static Int32 ZInflate(ZStream zStream, int flush)
        {
            GCHandle zStreamGCHandle = GCHandle.Alloc(zStream, GCHandleType.Pinned);

            Int32 result = ZInflate(zStreamGCHandle.AddrOfPinnedObject(), flush);

            zStreamGCHandle.Free();

            return result;
        }
    }
}