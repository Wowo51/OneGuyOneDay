using System;
using System.Text;

namespace DoubleDabble
{
    public static class DoubleDabbleConverter
    {
        // Converts a 32‐bit unsigned binary number to its Binary-Coded Decimal (BCD) representation.
        // The result is returned as an array of decimal digits (with no leading zeros).
        public static int[] ConvertToBCD(uint binaryNumber)
        {
            int numberOfDigits = 10;
            int[] bcd = new int[numberOfDigits];
            for (int bit = 31; bit >= 0; bit--)
            {
                // For each BCD digit, if the value is 5 or more, add 3.
                for (int i = 0; i < numberOfDigits; i++)
                {
                    if (bcd[i] >= 5)
                    {
                        bcd[i] += 3;
                    }
                }

                // Get the current bit from the binary number.
                int carry = (int)((binaryNumber >> bit) & 1);
                // Shift left the entire BCD array by 1 bit.
                for (int i = numberOfDigits - 1; i >= 0; i--)
                {
                    int shifted = (bcd[i] << 1) | carry;
                    bcd[i] = shifted & 0xF;
                    carry = (shifted >> 4) & 1;
                }
            }

            return RemoveLeadingZeros(bcd);
        }

        // Converts the binary number to a BCD string representation.
        public static string ConvertToBCDString(uint binaryNumber)
        {
            int[] digits = ConvertToBCD(binaryNumber);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < digits.Length; i++)
            {
                sb.Append(digits[i]);
            }

            return sb.ToString();
        }

        // Helper method to remove leading zeros (ensuring at least one digit remains).
        private static int[] RemoveLeadingZeros(int[] bcd)
        {
            int start = 0;
            while (start < bcd.Length - 1 && bcd[start] == 0)
            {
                start++;
            }

            int length = bcd.Length - start;
            int[] result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = bcd[start + i];
            }

            return result;
        }
    }
}