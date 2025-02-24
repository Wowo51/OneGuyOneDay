using System;
using System.Collections.Generic;

namespace FELICSImageCompression
{
    public class FELICSCompressor
    {
        public byte[] Compress(byte[] input)
        {
            if (input == null)
            {
                return new byte[0];
            }

            if (input.Length == 0)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            int i = 0;
            while (i < input.Length)
            {
                byte current = input[i];
                int count = 1;
                while (i + count < input.Length && count < 255 && input[i + count] == current)
                {
                    count++;
                }

                output.Add((byte)count);
                output.Add(current);
                i += count;
            }

            return output.ToArray();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            if (compressedData == null)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            int i = 0;
            while (i < compressedData.Length)
            {
                if (i + 1 >= compressedData.Length)
                {
                    break;
                }

                int count = compressedData[i];
                byte value = compressedData[i + 1];
                for (int j = 0; j < count; j++)
                {
                    output.Add(value);
                }

                i += 2;
            }

            return output.ToArray();
        }
    }
}