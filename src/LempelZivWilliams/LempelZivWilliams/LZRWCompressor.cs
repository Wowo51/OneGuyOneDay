using System;
using System.Collections.Generic;

namespace LempelZivWilliams
{
    public static class LZRWCompressor
    {
        private const int HASH_SIZE = 4096;
        private const int MIN_MATCH = 3;
        public static byte[]? Compress(byte[]? input)
        {
            if (input == null)
            {
                return null;
            }

            if (input.Length == 0)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            int[] dictionary = new int[HASH_SIZE];
            for (int j = 0; j < HASH_SIZE; j++)
            {
                dictionary[j] = -1;
            }

            int i = 0;
            while (i < input.Length)
            {
                int flagIndex = output.Count;
                output.Add(0); // placeholder for flag byte
                int flag = 0;
                int tokenCount = 0;
                while (tokenCount < 8 && i < input.Length)
                {
                    if (i <= input.Length - MIN_MATCH)
                    {
                        int hash = (((int)input[i] << 4) ^ input[i + 1]) & (HASH_SIZE - 1);
                        int candidate = dictionary[hash];
                        dictionary[hash] = i;
                        int matchLength = 0;
                        if (candidate != -1 && (i - candidate) <= 4095)
                        {
                            int maxLength = input.Length - i;
                            if (maxLength > 18)
                            {
                                maxLength = 18;
                            }

                            while (matchLength < maxLength && (candidate + matchLength) < input.Length && input[candidate + matchLength] == input[i + matchLength])
                            {
                                matchLength++;
                            }
                        }

                        if (matchLength >= MIN_MATCH)
                        {
                            int offset = i - candidate;
                            int lengthField = matchLength - MIN_MATCH;
                            int token = (offset << 4) | (lengthField & 0xF);
                            byte tokenLow = (byte)(token & 0xFF);
                            byte tokenHigh = (byte)((token >> 8) & 0xFF);
                            output.Add(tokenLow);
                            output.Add(tokenHigh);
                            int baseIndex = i;
                            int k = 1;
                            while (k < matchLength && (baseIndex + k) < input.Length - 1)
                            {
                                int hashTemp = (((int)input[baseIndex + k] << 4) ^ input[baseIndex + k + 1]) & (HASH_SIZE - 1);
                                dictionary[hashTemp] = baseIndex + k;
                                k++;
                            }

                            i += matchLength;
                            tokenCount++;
                            continue;
                        }
                    }

                    output.Add(input[i]);
                    flag |= (1 << tokenCount);
                    i++;
                    tokenCount++;
                }

                output[flagIndex] = (byte)flag;
            }

            return output.ToArray();
        }

        public static byte[]? Decompress(byte[]? input)
        {
            if (input == null)
            {
                return null;
            }

            if (input.Length == 0)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            int i = 0;
            while (i < input.Length)
            {
                byte flag = input[i];
                i++;
                for (int tokenIndex = 0; tokenIndex < 8 && i < input.Length; tokenIndex++)
                {
                    if ((flag & (1 << tokenIndex)) != 0)
                    {
                        output.Add(input[i]);
                        i++;
                    }
                    else
                    {
                        if (i + 1 >= input.Length)
                        {
                            break;
                        }

                        int token = input[i] | (input[i + 1] << 8);
                        i += 2;
                        int offset = token >> 4;
                        int lengthField = token & 0xF;
                        int matchLength = lengthField + MIN_MATCH;
                        int start = output.Count - offset;
                        for (int j = 0; j < matchLength; j++)
                        {
                            output.Add(output[start + j]);
                        }
                    }
                }
            }

            return output.ToArray();
        }
    }
}