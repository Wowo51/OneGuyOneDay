using System;

namespace FibonacciSearchTechnique
{
    public static class FibonacciSearch
    {
        public static int Search<T>(T[] sortedArray, T value)
            where T : IComparable<T>
        {
            if (sortedArray == null)
            {
                return -1;
            }

            int n = sortedArray.Length;
            int fibMMm2 = 0; // (m-2)'th Fibonacci number
            int fibMMm1 = 1; // (m-1)'th Fibonacci number
            int fibM = fibMMm2 + fibMMm1; // m'th Fibonacci number
            // Find the smallest Fibonacci number greater than or equal to n
            while (fibM < n)
            {
                fibMMm2 = fibMMm1;
                fibMMm1 = fibM;
                fibM = fibMMm2 + fibMMm1;
            }

            int offset = -1;
            while (fibM > 1)
            {
                int i = Math.Min(offset + fibMMm2, n - 1);
                if (sortedArray[i].CompareTo(value) < 0)
                {
                    fibM = fibMMm1;
                    fibMMm1 = fibMMm2;
                    fibMMm2 = fibM - fibMMm1;
                    offset = i;
                }
                else if (sortedArray[i].CompareTo(value) > 0)
                {
                    fibM = fibMMm2;
                    fibMMm1 = fibMMm1 - fibMMm2;
                    fibMMm2 = fibM - fibMMm1;
                }
                else
                {
                    return i;
                }
            }

            if (fibMMm1 != 0 && (offset + 1) < n && sortedArray[offset + 1].CompareTo(value) == 0)
            {
                return offset + 1;
            }

            return -1;
        }
    }
}