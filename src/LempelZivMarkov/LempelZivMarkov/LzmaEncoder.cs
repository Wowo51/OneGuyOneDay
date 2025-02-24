using System;
using System.Collections.Generic;

namespace LempelZivMarkov
{
    public class LzmaEncoder
    {
        private const byte Header0 = 0x4C; // 'L'
        private const byte Header1 = 0x5A; // 'Z'
        private const byte Header2 = 0x4D; // 'M'
        private const byte Header3 = 0x41; // 'A'
        private const byte ESC = 0xFF;
        private const int MinMatchLength = 3;
        private const int MaxOffset = 255;
        private const int MaxMatchLength = 255;
        public byte[] Encode(byte[] data)
        {
            if (data == null)
            {
                return new byte[0];
            }

            List<byte> output = new List<byte>();
            // Write header "LZMA"
            output.Add(Header0);
            output.Add(Header1);
            output.Add(Header2);
            output.Add(Header3);
            // Build and serialize MarkovModel
            MarkovModel model = new MarkovModel();
            model.Build(data);
            byte[] modelData = model.Serialize();
            byte[] modelLengthBytes = BitConverter.GetBytes(modelData.Length);
            for (int i = 0; i < modelLengthBytes.Length; i++)
            {
                output.Add(modelLengthBytes[i]);
            }

            for (int i = 0; i < modelData.Length; i++)
            {
                output.Add(modelData[i]);
            }

            int index = 0;
            while (index < data.Length)
            {
                int bestLength = 0;
                int bestOffset = 0;
                int searchLimit = index < MaxOffset ? index : MaxOffset;
                for (int offset = 1; offset <= searchLimit; offset++)
                {
                    int pos = index - offset;
                    int length = 0;
                    while (index + length < data.Length && length < MaxMatchLength && data[pos + length] == data[index + length])
                    {
                        length++;
                    }

                    if (length > bestLength && length >= MinMatchLength)
                    {
                        bestLength = length;
                        bestOffset = offset;
                    }
                }

                if (bestLength >= MinMatchLength)
                {
                    // Emit a back-reference command: ESC marker, match length, and offset.
                    output.Add(ESC);
                    output.Add((byte)bestLength);
                    output.Add((byte)bestOffset);
                    index += bestLength;
                }
                else
                {
                    // Output literal byte; if it equals the ESC marker, escape it with a zero.
                    byte literal = data[index];
                    if (literal == ESC)
                    {
                        output.Add(ESC);
                        output.Add(0);
                    }
                    else
                    {
                        output.Add(literal);
                    }

                    index++;
                }
            }

            return output.ToArray();
        }
    }
}