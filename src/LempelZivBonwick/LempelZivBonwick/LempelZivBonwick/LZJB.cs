using System;

namespace LempelZivBonwick
{
    public static class LZJB
    {
        // LZJB parameters: minimum match length is 3, maximum match length is 10,
        // and the sliding window size is 8192 bytes.
        private const int MATCH_MIN = 3;
        private const int MATCH_MAX = 10;
        private const int WINDOW_SIZE = 8192;
        // Compress uses 1-byte control groups where each bit indicates whether the corresponding
        // token is a literal (bit = 1) or a back-reference (bit = 0). A back-reference token is encoded
        // in 2 bytes: the upper 3 bits encode (matchLength - 3) and the lower 13 bits encode the offset.
        public static byte[] Compress(byte[] input)
        {
            if (input == null)
            {
                return new byte[0];
            }

            int inputLength = input.Length;
            // Allocate output buffer worst-case: every literal might add 1 extra control byte per 8 literals.
            byte[] output = new byte[inputLength * 2];
            int src = 0;
            int dst = 0;
            // Process input in groups of up to 8 tokens.
            while (src < inputLength)
            {
                int controlIndex = dst;
                dst++; // Reserve one byte for the control flags.
                byte control = 0;
                for (int bit = 0; bit < 8 && src < inputLength; bit++)
                {
                    int bestLength = 0;
                    int bestOffset = 0;
                    int windowStart = (src > WINDOW_SIZE ? src - WINDOW_SIZE : 0);
                    // Search for a match in the sliding window.
                    for (int i = windowStart; i < src; i++)
                    {
                        int length = 0;
                        while (length < MATCH_MAX && src + length < inputLength && input[i + length] == input[src + length])
                        {
                            length++;
                        }

                        if (length >= MATCH_MIN && length > bestLength)
                        {
                            bestLength = length;
                            bestOffset = src - i;
                        }
                    }

                    // If a match of at least MATCH_MIN is found and the offset is encodable.
                    if (bestLength >= MATCH_MIN && bestOffset <= 0x1FFF)
                    {
                        // Encode the match as a 16-bit token:
                        // Bits 15-13: (matchLength - 3), Bits 12-0: offset.
                        ushort token = (ushort)(((bestLength - 3) << 13) | bestOffset);
                        output[dst++] = (byte)(token & 0xFF);
                        output[dst++] = (byte)((token >> 8) & 0xFF);
                        src += bestLength;
                    }
                    else
                    {
                        // No acceptable match; output the literal byte.
                        control |= (byte)(1 << bit);
                        output[dst++] = input[src++];
                    }
                }

                // Write back the control byte at the reserved location.
                output[controlIndex] = control;
            }

            // Copy the compressed data to an array of the correct size.
            byte[] result = new byte[dst];
            System.Buffer.BlockCopy(output, 0, result, 0, dst);
            return result;
        }

        // Decompress reconstructs the original data using the control flags and tokens.
        // It requires the expected decompressedLength.
        public static byte[] Decompress(byte[] compressed, int decompressedLength)
        {
            if (compressed == null)
            {
                return new byte[0];
            }

            byte[] output = new byte[decompressedLength];
            int src = 0;
            int dst = 0;
            // Process each control group.
            while (src < compressed.Length && dst < decompressedLength)
            {
                byte control = compressed[src++];
                for (int bit = 0; bit < 8 && dst < decompressedLength; bit++)
                {
                    if ((control & (1 << bit)) != 0)
                    {
                        // Control bit set => literal byte.
                        output[dst++] = compressed[src++];
                    }
                    else
                    {
                        // Back-reference token: read two bytes.
                        if (src + 1 >= compressed.Length)
                        {
                            break;
                        }

                        ushort token = (ushort)(compressed[src] | (compressed[src + 1] << 8));
                        src += 2;
                        int length = (token >> 13) + MATCH_MIN;
                        int offset = token & 0x1FFF;
                        int refPos = dst - offset;
                        // Copy the sequence from the already decompressed data.
                        for (int k = 0; k < length && dst < decompressedLength; k++)
                        {
                            output[dst++] = output[refPos + k];
                        }
                    }
                }
            }

            return output;
        }
    }
}