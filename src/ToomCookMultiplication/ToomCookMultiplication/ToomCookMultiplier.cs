using System;
using System.Numerics;

namespace ToomCookMultiplication
{
    public static class ToomCookMultiplier
    {
        private const int Threshold = 4;
        public static string Multiply(string? num1, string? num2)
        {
            string number1 = NormalizeNumber(num1);
            string number2 = NormalizeNumber(num2);
            if (number1 == "0" || number2 == "0")
            {
                return "0";
            }

            if (number1.Length <= Threshold || number2.Length <= Threshold)
            {
                BigInteger a = BigInteger.Parse(number1);
                BigInteger b = BigInteger.Parse(number2);
                return (a * b).ToString();
            }

            int n = Math.Max(number1.Length, number2.Length);
            int m = (n + 2) / 3;
            int paddedLength = 3 * m;
            string paddedA = number1.PadLeft(paddedLength, '0');
            string paddedB = number2.PadLeft(paddedLength, '0');
            string a0 = paddedA.Substring(paddedLength - m, m);
            string a1 = paddedA.Substring(paddedLength - 2 * m, m);
            string a2 = paddedA.Substring(paddedLength - 3 * m, m);
            string b0 = paddedB.Substring(paddedLength - m, m);
            string b1 = paddedB.Substring(paddedLength - 2 * m, m);
            string b2 = paddedB.Substring(paddedLength - 3 * m, m);
            BigInteger A0 = BigInteger.Parse(a0);
            BigInteger A1 = BigInteger.Parse(a1);
            BigInteger A2 = BigInteger.Parse(a2);
            BigInteger B0 = BigInteger.Parse(b0);
            BigInteger B1 = BigInteger.Parse(b1);
            BigInteger B2 = BigInteger.Parse(b2);
            BigInteger baseB = BigInteger.Pow(10, m);
            BigInteger p0 = A0 * B0;
            BigInteger p1 = (A0 + A1 + A2) * (B0 + B1 + B2);
            BigInteger pNeg1 = (A0 - A1 + A2) * (B0 - B1 + B2);
            BigInteger p2 = (A0 + 2 * A1 + 4 * A2) * (B0 + 2 * B1 + 4 * B2);
            BigInteger pInf = A2 * B2;
            BigInteger r0 = p0;
            BigInteger r4 = pInf;
            BigInteger r2 = (p1 + pNeg1 - 2 * p0 - 2 * pInf) / 2;
            BigInteger r1PlusR3 = (p1 - pNeg1) / 2;
            BigInteger r3 = (p2 - r0 - 4 * r2 - 16 * r4 - 2 * r1PlusR3) / 6;
            BigInteger r1 = r1PlusR3 - r3;
            BigInteger result = r0 + r1 * baseB + r2 * BigInteger.Pow(baseB, 2) + r3 * BigInteger.Pow(baseB, 3) + r4 * BigInteger.Pow(baseB, 4);
            return result.ToString();
        }

        private static string NormalizeNumber(string? num)
        {
            if (string.IsNullOrWhiteSpace(num))
            {
                return "0";
            }

            string trimmed = num.Trim();
            int index = 0;
            while (index < trimmed.Length - 1 && trimmed[index] == '0')
            {
                index++;
            }

            return trimmed.Substring(index);
        }
    }
}