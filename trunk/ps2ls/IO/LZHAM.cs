using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.IO
{
    class LZHAM
    {
        public enum CompressLevel
        {
            Fastest = 0,
            Faster,
            Default,
            Better,
            Uber,
            CompressLevels,
            ForceDWORD = -1,
        }

        public enum CompressStatus
        {
            NotFinished = 0,
            NeedsMoreInput,
            FirstSuccessOrFailureCode,
            Success = FirstSuccessOrFailureCode,
            Failed,
            FailedInitializing,
            InvalidParameter,
            OutputBufferTooSmall,
            ForceDWORD = -1,
        }

        enum DecompressStatus
        {
            /// <summary>
            /// LZHAM_DECOMP_STATUS_NOT_FINISHED indicates that the decompressor is flushing its output buffer (and there may be more bytes available to decompress).
            /// </summary>
            NotFinished = 0,
            /// <summary>
            /// LZHAM_DECOMP_STATUS_NEEDS_MORE_INPUT indicates that the decompressor has consumed all input bytes and has not encountered an "end of stream" code, so it's expecting more input to proceed.
            /// </summary>
            NeedsMoreInput,
            /// <summary>
            /// All the following enums always (and MUST) indicate failure/success.
            /// </summary>
            FirstSuccessOrFailureCode,
            /// <summary>
            /// LZHAM_DECOMP_STATUS_SUCCESS indicates decompression has successfully completed.
            /// </summary>
            Success = FirstSuccessOrFailureCode,
            /// <summary>
            /// The remaining status codes indicate a failure of some sort. Most failures are unrecoverable. TODO: Document which codes are recoverable.
            /// </summary>
            FirstFailureCode,
            FailedInitializing = FirstFailureCode,
            DestBufTooSmall,
            HaveMoreOutput,
            ExpectedMoreRawBytes,
            BadCode,
            Adler32,
            BadRawBlock,
            BadCompBlockSyncCheck,
            InvalidParameter,
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct CompressParams
        {
            UInt32 StructSize;
            UInt32 DictionarySizeLog2;
            CompressLevel Level;
            UInt32 MaxHelperThreads;
            UInt32 CpuCacheTotalLines;
            UInt32 CpuCacheLineSize;
            UInt32 CompressFlags;
            UInt32 NumSeedBytes;
            IntPtr SeedBytes;
        }

        /// <summary>
        /// Compresses an arbitrarily sized block of data, writing as much available compressed data as possible to the output buffer. 
        /// This method may be called as many times as needed, but for best perf. try not to call it with tiny buffers.
        /// pState - Pointer to internal compression state, created by lzham_compress_init.
        /// pIn_buf, pIn_buf_size - Pointer to input data buffer, and pointer to a size_t containing the number of bytes available in this buffer. 
        ///                         On return, *pIn_buf_size will be set to the number of bytes read from the buffer.
        /// pOut_buf, pOut_buf_size - Pointer to the output data buffer, and a pointer to a size_t containing the max number of bytes that can be written to this buffer.
        ///                         On return, *pOut_buf_size will be set to the number of bytes written to this buffer.
        /// no_more_input_bytes_flag - Set to true to indicate that no more input bytes are available to compress (EOF). Once you call this function with this param set to true, it must stay set to true in all future calls.
        ///
        /// Normal return status codes:
        ///    LZHAM_COMP_STATUS_NOT_FINISHED - Compression can continue, but the compressor needs more input, or it needs more room in the output buffer.
        ///    LZHAM_COMP_STATUS_NEEDS_MORE_INPUT - Compression can contintue, but the compressor has no more output, and has no input but we're not at EOF. Supply more input to continue.
        /// Success/failure return status codes:
        ///    LZHAM_COMP_STATUS_SUCCESS - Compression has completed successfully.
        ///    LZHAM_COMP_STATUS_FAILED, LZHAM_COMP_STATUS_FAILED_INITIALIZING, LZHAM_COMP_STATUS_INVALID_PARAMETER - Something went wrong.
        /// </summary>
        [DllImport(@"C:\Users\Colin\Desktop\lzham_x86.dll", EntryPoint = "lzham_compress", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern CompressStatus Compress(IntPtr state, IntPtr inputBuffer, IntPtr inputBufferSize, IntPtr outputBuffer, IntPtr outputBufferSize, bool noMoreInputBytesFlag);
        
        /// <summary>
        /// Deinitializes a decompressor.
        /// returns adler32 of decompressed data if compute_adler32 was true, otherwise it returns the adler32 from the compressed stream.
        /// </summary>
        [DllImport(@"C:\Users\Colin\Desktop\lzham_x86.dll", EntryPoint = "lzham_decompress_deinit", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 DecompressDeinit(IntPtr pState);

        /// <summary>
        /// Initializes a compressor. Returns a pointer to the compressor's internal state, or NULL on failure.
        /// pParams cannot be NULL. Be sure to initialize the pParams->m_struct_size member to sizeof(lzham_compress_params) (along with the other members to reasonable values) before calling this function.
        /// TODO: With large dictionaries this function could take a while (due to memory allocation). I need to add a reinit() API for compression (decompression already has one).
        /// </summary>
        [DllImport(@"C:\Users\Colin\Desktop\lzham_x86.dll", EntryPoint = "lzham_compress_init", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CompressInit(IntPtr compressParams);

        /// <summary>
        /// Single function call compression interface.
        /// Same return codes as lzham_compress, except this function can also return LZHAM_COMP_STATUS_OUTPUT_BUF_TOO_SMALL.
        /// </summary>
        [DllImport(@"C:\Users\Colin\Desktop\lzham_x86.dll", EntryPoint = "lzham_compress_memory", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern CompressStatus CompressMemory(IntPtr compressParams, IntPtr destinationBuffer, UInt64 destinationLength, IntPtr sourceBuffer, UInt64 sourceLength, IntPtr adler32);

        //TODO: MORE FUNCTIONS

        /// <summary>
        /// Returns DLL version (LZHAM_DLL_VERSION).
        /// </summary>
        [DllImport(@"C:\Users\Colin\Desktop\lzham_x86.dll", EntryPoint = "lzham_get_version", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetVersion();
    }
}
