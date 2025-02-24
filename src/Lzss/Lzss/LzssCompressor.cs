using System;
using System.Collections.Generic;

namespace Lzss
{
    public static class LzssCompressor
    {
        private const int WindowSize = 255;
        private const int LookaheadBufferSize = 18;
        private const int MinMatchLength = 3;
        public static byte[] Compress(byte[] input)
        {
            if (input == null)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            int i = 0;
            while (i < input.Length)
            {
                int bestMatchLength = 0;
                int bestMatchOffset = 0;
                int startSearch = (i - WindowSize > 0) ? i - WindowSize : 0;
                for (int j = startSearch; j < i; j++)
                {
                    int length = 0;
                    while ((i + length < input.Length) && (j + length < input.Length) && (input[j + length] == input[i + length]))
                    {
                        length++;
                        if (length == LookaheadBufferSize)
                        {
                            break;
                        }
                    }

                    if (length > bestMatchLength)
                    {
                        bestMatchLength = length;
                        bestMatchOffset = i - j;
                    }
                }

                if (bestMatchLength >= MinMatchLength)
                {
                    output.Add(0x00);
                    output.Add((byte)bestMatchOffset);
                    output.Add((byte)bestMatchLength);
                    i += bestMatchLength;
                }
                else
                {
                    output.Add(0x01);
                    output.Add(input[i]);
                    i++;
                }
            }

            return output.ToArray();
        }

        public static byte[] Decompress(byte[] input)
        {
            if (input == null)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            int i = 0;
            while (i < input.Length)
            {
                byte tag = input[i];
                i++;
                if (tag == 0x01)
                {
                    if (i < input.Length)
                    {
                        byte literal = input[i];
                        i++;
                        output.Add(literal);
                    }
                }
                else if (tag == 0x00)
                {
                    if (i + 1 < input.Length)
                    {
                        int offset = input[i];
                        int length = input[i + 1];
                        i += 2;
                        int start = output.Count - offset;
                        for (int j = 0; j < length; j++)
                        {
                            output.Add(output[start + j]);
                        }
                    }
                }
                else
                {
                    output.Add(tag);
                }
            }

            return output.ToArray();
        }
    }
}