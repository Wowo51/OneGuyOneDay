using System;

namespace LempelZivMarkov
{
    // LzmaCompressor provides an interface for compression and decompression.
    // Internally, it uses LzmaEncoder and LzmaDecoder to simulate an LZMA algorithm.
    public static class LzmaCompressor
    {
        public static byte[] Compress(byte[] input)
        {
            LzmaEncoder encoder = new LzmaEncoder();
            return encoder.Encode(input);
        }

        public static byte[] Decompress(byte[] input)
        {
            LzmaDecoder decoder = new LzmaDecoder();
            return decoder.Decode(input);
        }
    }
}