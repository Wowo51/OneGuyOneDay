using System.Collections.Generic;
using System.Text;

namespace FibonacciCoding
{
    public static class FibonacciCoder
    {
        public static string Encode(int number)
        {
            if (number < 1)
            {
                return "0";
            }

            List<long> fibonacciNumbers = new List<long>();
            fibonacciNumbers.Add(1);
            fibonacciNumbers.Add(2);
            while (fibonacciNumbers[fibonacciNumbers.Count - 1] <= number)
            {
                long next = fibonacciNumbers[fibonacciNumbers.Count - 1] + fibonacciNumbers[fibonacciNumbers.Count - 2];
                fibonacciNumbers.Add(next);
            }

            if (fibonacciNumbers[fibonacciNumbers.Count - 1] > number)
            {
                fibonacciNumbers.RemoveAt(fibonacciNumbers.Count - 1);
            }

            StringBuilder code = new StringBuilder();
            bool started = false;
            int remainder = number;
            for (int i = fibonacciNumbers.Count - 1; i >= 0; i--)
            {
                if (fibonacciNumbers[i] <= remainder)
                {
                    code.Append("1");
                    remainder -= (int)fibonacciNumbers[i];
                    started = true;
                }
                else if (started)
                {
                    code.Append("0");
                }
            }

            code.Append("1");
            return code.ToString();
        }

        public static int Decode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return 0;
            }

            int lengthCode = code.Length;
            string bits = code.Substring(0, lengthCode - 1);
            int bitsLength = bits.Length;
            if (bitsLength == 0)
            {
                return 0;
            }

            List<int> fibs = new List<int>();
            fibs.Add(1);
            if (bitsLength > 1)
            {
                fibs.Add(2);
                for (int i = 2; i < bitsLength; i++)
                {
                    fibs.Add(fibs[i - 1] + fibs[i - 2]);
                }
            }

            int result = 0;
            for (int i = 0; i < bitsLength; i++)
            {
                if (bits[i] == '1')
                {
                    result += fibs[bitsLength - i - 1];
                }
            }

            return result;
        }
    }
}