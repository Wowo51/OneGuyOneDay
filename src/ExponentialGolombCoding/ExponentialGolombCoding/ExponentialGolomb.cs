using System;

namespace ExponentialGolombCoding
{
    public static class ExponentialGolomb
    {
        // Encodes a non-negative integer value using exponential-Golomb coding.
        // Returns the encoded bit string (e.g. "00110" for value 5).
        public static string Encode(uint value)
        {
            uint valuePlusOne = value + 1u;
            int m = 0;
            uint temp = valuePlusOne;
            while (temp > 1u)
            {
                m = m + 1;
                temp = temp >> 1;
            }

            string binaryRepresentation = ToBinary(valuePlusOne, m + 1);
            string prefix = new string ('0', m);
            return prefix + binaryRepresentation;
        }

        // Decodes an exponential-Golomb coded bit stream starting at index 0.
        // bitsRead returns the number of bits consumed during decoding.
        // Returns 0 if the bitstream is null or incomplete.
        public static uint Decode(string bitstream, out int bitsRead)
        {
            bitsRead = 0;
            if (bitstream == null)
            {
                return 0u;
            }

            int leadingZeros = 0;
            while (bitsRead < bitstream.Length && bitstream[bitsRead] == '0')
            {
                leadingZeros = leadingZeros + 1;
                bitsRead = bitsRead + 1;
            }

            if (bitsRead >= bitstream.Length)
            {
                return 0u;
            }

            int totalBits = leadingZeros + 1;
            int startPos = bitsRead;
            if (startPos + totalBits > bitstream.Length)
            {
                return 0u;
            }

            string codeword = bitstream.Substring(startPos, totalBits);
            uint valuePlusOne = BinaryToUInt(codeword);
            bitsRead = bitsRead + totalBits;
            return valuePlusOne - 1u;
        }

        // Converts an unsigned integer to its binary representation with a fixed width.
        private static string ToBinary(uint number, int width)
        {
            char[] bits = new char[width];
            int index = width - 1;
            while (index >= 0)
            {
                bits[index] = ((number & 1u) == 1u) ? '1' : '0';
                number = number >> 1;
                index = index - 1;
            }

            return new string (bits);
        }

        // Converts a binary string to an unsigned integer.
        private static uint BinaryToUInt(string binary)
        {
            uint result = 0u;
            for (int i = 0; i < binary.Length; i = i + 1)
            {
                result = result << 1;
                result = result + ((binary[i] == '1') ? 1u : 0u);
            }

            return result;
        }
    }
}