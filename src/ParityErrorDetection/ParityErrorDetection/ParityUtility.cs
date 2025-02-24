using System;

namespace ParityErrorDetection
{
    public static class ParityUtility
    {
        /// <summary>
        /// Computes the even parity bit for a given byte.
        /// Returns 1 if the number of 1 bits is odd, 0 if even.
        /// </summary>
        public static int ComputeEvenParityBit(byte input)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                int mask = 1 << i;
                if ((input & mask) != 0)
                {
                    count++;
                }
            }

            return (count % 2 == 1) ? 1 : 0;
        }

        /// <summary>
        /// Validates that the provided parityBit matches the computed parity for input.
        /// </summary>
        public static bool ValidateParity(byte input, int parityBit)
        {
            return ComputeEvenParityBit(input) == parityBit;
        }

        /// <summary>
        /// Appends a parity byte to the end of the data array.
        /// If data is null, returns an empty array.
        /// The parity byte is computed as the XOR of all bytes in the array.
        /// </summary>
        public static byte[] AppendParityBit(byte[] data)
        {
            if (data == null)
            {
                return new byte[0];
            }

            int length = data.Length;
            byte parity = 0;
            for (int index = 0; index < length; index++)
            {
                parity ^= data[index];
            }

            byte[] result = new byte[length + 1];
            Array.Copy(data, result, length);
            result[length] = parity;
            return result;
        }
    }
}