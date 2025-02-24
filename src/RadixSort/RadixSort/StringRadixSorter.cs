using System;

namespace RadixSort
{
    public static class StringRadixSorter
    {
        public static string[] Sort(string[] input)
        {
            if (input == null)
            {
                return new string[0];
            }

            int n = input.Length;
            if (n == 0)
            {
                return input;
            }

            int maxLen = 0;
            for (int i = 0; i < n; i++)
            {
                string current = input[i] ?? string.Empty;
                if (current.Length > maxLen)
                {
                    maxLen = current.Length;
                }
            }

            string[] result = new string[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = input[i] ?? string.Empty;
            }

            for (int pos = maxLen - 1; pos >= 0; pos--)
            {
                int[] count = new int[256];
                for (int i = 0; i < n; i++)
                {
                    string current = result[i];
                    char ch = pos < current.Length ? current[pos] : (char)0;
                    int index = (int)ch;
                    count[index]++;
                }

                for (int i = 1; i < 256; i++)
                {
                    count[i] += count[i - 1];
                }

                string[] output = new string[n];
                for (int i = n - 1; i >= 0; i--)
                {
                    string current = result[i];
                    char ch = pos < current.Length ? current[pos] : (char)0;
                    int index = (int)ch;
                    count[index]--;
                    output[count[index]] = current;
                }

                result = output;
            }

            return result;
        }
    }
}