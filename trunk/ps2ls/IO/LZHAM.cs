using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace ps2ls.IO
{
    public static class LZHAM
    {
        public const int MinDictSizeLog2 = 15;
        public const int MaxDictSizeLog2x86 = 26;
        public const int MaxDictSizeLog2x65 = 29;
        public const int MaxHelperThreads = 16;

        public enum CompressFlags
        {
            /// <summary>
            /// Forces Polar codes vs. Huffman, for a slight increase in decompression speed.
            /// </summary>
            ForcePolarCoding = 1,
            /// <summary>
            /// Improves ratio by allowing the compressor's parse graph to grow "higher" (up to 4 parent nodes per output node), but is much slower.
            /// </summary>
            ExtremeParsing = 2,
            /// <summary>
            /// Guarantees that the compressed output will always be the same given the same input and parameters (no variation between runs due to kernel threading scheduling).
            /// </summary>
            DeterministicParsing = 4,
            /// <summary>
            /// If enabled, the compressor is free to use any optimizations which could lower the decompression rate (such
            /// as adaptively resetting the Huffman table update rate to maximum frequency, which is costly for the decompressor).
            /// </summary>
            TradeoffDecompressionRateForCompRatio = 16
        };

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

        public enum DecompressStatus
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

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct CompressParams
        {
            public UInt32 StructSize;
            public UInt32 DictSizeLog2;
            public CompressLevel Level;
            public UInt32 MaxHelperThreads;
            public UInt32 CpuCacheTotalLines;
            public UInt32 CpuCacheLineSize;
            public UInt32 CompressFlags;
            public UInt32 NumSeedBytes;
            public IntPtr SeedBytes;
        }

        /// <summary>
        /// Decompression parameters structure.
        /// Notes: 
        /// m_dict_size_log2 MUST match the value used during compression!
        /// If m_num_seed_bytes != 0, m_output_unbuffered MUST be false (i.e. static "seed" dictionaries are not compatible with unbuffered decompression).
        /// The seed buffer's contents and size must match the seed buffer used during compression.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct DecompressParams
        {
            /// <summary>
            /// set to sizeof(lzham_decompress_params)
            /// </summary>
            public UInt32 StructSize;
            /// <summary>
            /// set to the log2(dictionary_size), must range between [LZHAM_MIN_DICT_SIZE_LOG2, LZHAM_MAX_DICT_SIZE_LOG2_X86] for x86 LZHAM_MAX_DICT_SIZE_LOG2_X64 for x64
            /// </summary>
            public UInt32 DictSizeLog2;
            /// <summary>
            /// true if the output buffer is guaranteed to be large enough to hold the entire output stream (a bit faster)
            /// </summary>
            public byte OutputUnbuffered;
            /// <summary>
            /// true to enable adler32 checking during decompression (slightly slower)
            /// </summary>
            public byte ComputeAdler32;
            /// <summary>
            /// for delta compression (optional) - number of seed bytes pointed to by m_pSeed_bytes
            /// </summary>
            public UInt32 NumSeedBytes;
            /// <summary>
            /// for delta compression (optional) - pointer to seed bytes buffer, must be at least m_num_seed_bytes long
            /// </summary>
            public IntPtr SeedBytes;
        };

        public delegate IntPtr ReallocFunc(IntPtr p, UInt64 size, IntPtr pActual_size, bool movable, IntPtr pUser_data);
        public delegate UInt64 MSizeFunc(IntPtr p, IntPtr pUser_data);

        /// <summary>
        /// Returns DLL version (LZHAM_DLL_VERSION).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_get_version", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GetVersion();

        /// <summary>
        /// Call this function to force LZHAM to use custom memory malloc(), realloc(), free() and msize functions.
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_set_memory_callbacks", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMemoryCallbacks(ReallocFunc pRealloc, MSizeFunc pMSize, IntPtr pUser_data);

        /// <summary>
        /// Initializes a decompressor.
        /// pParams cannot be NULL. Be sure to initialize the pParams->m_struct_size member to sizeof(lzham_decompress_params) (along with the other members to reasonable values) before calling this function.
        /// Note: With large dictionaries this function could take a while (due to memory allocation). To serially decompress multiple streams, it's faster to init a compressor once and 
        /// reuse it using by calling lzham_decompress_reinit().
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_decompress_init", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr DecompressInitialize(IntPtr decompressParamsIntPtr);
        public static IntPtr DecompressInitialize(DecompressParams decompressParams)
        {
            GCHandle decompressParamsGCHandle = GCHandle.Alloc(decompressParams, GCHandleType.Pinned);
            IntPtr decompressStateIntPtr = DecompressInitialize(decompressParamsGCHandle.AddrOfPinnedObject());
            decompressParamsGCHandle.Free();
            return decompressStateIntPtr;
        }

        /// <summary>
        /// Quickly re-initializes the decompressor to its initial state given an already allocated/initialized state (doesn't do any memory alloc unless necessary).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_decompress_reinit", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr DecompressReinitialize(IntPtr statePointer, IntPtr decompressParamsIntPtr);
        public static IntPtr DecompressReinitialize(IntPtr statePointer, DecompressParams decompressParams)
        {
            IntPtr decompressParamsIntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DecompressParams)));
            Marshal.StructureToPtr(decompressParams, decompressParamsIntPtr, false);
            IntPtr returnStatePointer = DecompressReinitialize(statePointer, decompressParamsIntPtr);
            Marshal.FreeHGlobal(decompressParamsIntPtr);
            return returnStatePointer;
        }

        /// <summary>
        /// Deinitializes a decompressor.
        /// returns adler32 of decompressed data if compute_adler32 was true, otherwise it returns the adler32 from the compressed stream.
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_decompress_deinit", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 DecompressDeinitialize(IntPtr statePointer);

        /// <summary>
        /// Decompresses an arbitrarily sized block of compressed data, writing as much available decompressed data as possible to the output buffer. 
        /// This method is implemented as a coroutine so it may be called as many times as needed. However, for best perf. try not to call it with tiny buffers.
        /// pState - Pointer to internal decompression state, originally created by lzham_decompress_init.
        /// pIn_buf, pIn_buf_size - Pointer to input data buffer, and pointer to a size_t containing the number of bytes available in this buffer. 
        ///                         On return, *pIn_buf_size will be set to the number of bytes read from the buffer.
        /// pOut_buf, pOut_buf_size - Pointer to the output data buffer, and a pointer to a size_t containing the max number of bytes that can be written to this buffer.
        ///                         On return, *pOut_buf_size will be set to the number of bytes written to this buffer.
        /// no_more_input_bytes_flag - Set to true to indicate that no more input bytes are available to compress (EOF). Once you call this function with this param set to true, it must stay set to true in all future calls.
        /// Notes:
        /// In unbuffered mode, the output buffer MUST be large enough to hold the entire decompressed stream. Otherwise, you'll receive the
        ///  LZHAM_DECOMP_STATUS_FAILED_DEST_BUF_TOO_SMALL error (which is currently unrecoverable during unbuffered decompression).
        /// In buffered mode, if the output buffer's size is 0 bytes, the caller is indicating that no more output bytes are expected from the
        ///  decompressor. In this case, if the decompressor actually has more bytes you'll receive the LZHAM_DECOMP_STATUS_FAILED_HAVE_MORE_OUTPUT
        ///  error (which is recoverable in the buffered case - just call lzham_decompress() again with a non-zero size output buffer).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_decompress", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern DecompressStatus Decompress(IntPtr decompressState, IntPtr pIn_buf, IntPtr pIn_buf_size, IntPtr pOut_buf, IntPtr pOut_buf_size, bool no_more_input_bytes_flag);
        public static DecompressStatus Decompress(IntPtr decompressState, Byte[] inputBuffer, Byte[] outputBuffer, bool noMoreInputBytesFlag)
        {
            UInt64 inputBufferSize = (UInt64)inputBuffer.Length;
            UInt64 outputBufferSize = (UInt64)outputBuffer.Length;

            GCHandle inputBufferGCHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
            GCHandle inputBufferSizeGCHandle = GCHandle.Alloc(inputBufferSize, GCHandleType.Pinned);
            GCHandle outputBufferGCHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
            GCHandle outputBufferSizeGCHandle = GCHandle.Alloc(outputBufferSize, GCHandleType.Pinned);

            DecompressStatus result = Decompress(decompressState, inputBufferGCHandle.AddrOfPinnedObject(), inputBufferSizeGCHandle.AddrOfPinnedObject(), outputBufferGCHandle.AddrOfPinnedObject(), outputBufferSizeGCHandle.AddrOfPinnedObject(), true);

            inputBufferGCHandle.Free();
            inputBufferSizeGCHandle.Free();
            outputBufferGCHandle.Free();
            outputBufferSizeGCHandle.Free();

            return result;
        }

        /// <summary>
        /// Single function call interface.
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_decompress_memory", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern DecompressStatus DecompressMemory(IntPtr pParams, IntPtr pDst_buf, IntPtr pDst_len, IntPtr pSrc_buf, UInt64 src_len, IntPtr pAdler32);

        /// <summary>
        /// Initializes a compressor. Returns a pointer to the compressor's internal state, or NULL on failure.
        /// pParams cannot be NULL. Be sure to initialize the pParams->m_struct_size member to sizeof(lzham_compress_params) (along with the other members to reasonable values) before calling this function.
        /// TODO: With large dictionaries this function could take a while (due to memory allocation). I need to add a reinit() API for compression (decompression already has one).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_compress_init", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CompressInitialize(IntPtr compressParams);

        /// <summary>
        /// Deinitializes a compressor, releasing all allocated memory.
        //  returns adler32 of source data (valid only on success).
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_compress_deinit", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 CompressDeinitialize(IntPtr p);

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
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_compress", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern CompressStatus Compress(IntPtr state, IntPtr inputBuffer, IntPtr inputBufferSize, IntPtr outputBuffer, IntPtr outputBufferSize, bool noMoreInputBytesFlag);

        /// <summary>
        /// Single function call compression interface.
        /// Same return codes as lzham_compress, except this function can also return LZHAM_COMP_STATUS_OUTPUT_BUF_TOO_SMALL.
        /// </summary>
        [DllImport("lzham_x86.dll", EntryPoint = "lzham_compress_memory", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern CompressStatus CompressMemory(IntPtr compressParams, IntPtr destinationBuffer, UInt64 destinationLength, IntPtr sourceBuffer, UInt64 sourceLength, IntPtr adler32);
    }
}