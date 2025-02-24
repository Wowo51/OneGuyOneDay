using System;
using System.Collections.Generic;

namespace LempelZivMarkov
{
    public class LzmaDecoder
    {
        private const byte Header0 = 0x4C; // 'L'
        private const byte Header1 = 0x5A; // 'Z'
        private const byte Header2 = 0x4D; // 'M'
        private const byte Header3 = 0x41; // 'A'
        private const byte ESC = 0xFF;
        public byte[] Decode(byte[] data)
        {
            if (data == null || data.Length < 4)
            {
                return new byte[0];
            }

            // Verify header "LZMA"
            if (data[0] != Header0 || data[1] != Header1 || data[2] != Header2 || data[3] != Header3)
            {
                return data;
            }

            List<byte> output = new List<byte>();
            int index = 4;
            // Read MarkovModel data length and data
            if (data.Length < index + 4)
            {
                return new byte[0];
            }

            int modelLength = BitConverter.ToInt32(data, index);
            index += 4;
            if (data.Length < index + modelLength)
            {
                return new byte[0];
            }

            byte[] modelData = new byte[modelLength];
            Array.Copy(data, index, modelData, 0, modelLength);
            index += modelLength;
            MarkovModel model = MarkovModel.Deserialize(modelData);
            while (index < data.Length)
            {
                if (data[index] == ESC)
                {
                    index++;
                    if (index >= data.Length)
                    {
                        break;
                    }

                    byte command = data[index];
                    index++;
                    if (command == 0)
                    {
                        // Escaped literal ESC marker.
                        output.Add(ESC);
                    }
                    else
                    {
                        int matchLength = command;
                        if (index >= data.Length)
                        {
                            break;
                        }

                        byte offset = data[index];
                        index++;
                        int start = output.Count - offset;
                        if (start < 0)
                        {
                            return new byte[0];
                        }

                        for (int i = 0; i < matchLength; i++)
                        {
                            output.Add(output[start + i]);
                        }
                    }
                }
                else
                {
                    output.Add(data[index]);
                    index++;
                }
            }

            return output.ToArray();
        }
    }
}