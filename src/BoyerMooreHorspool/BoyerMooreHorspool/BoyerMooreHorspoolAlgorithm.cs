using System;
using System.Collections.Generic;

namespace BoyerMooreHorspool
{
    public static class BoyerMooreHorspoolAlgorithm
    {
        public static int Search(string text, string pattern)
        {
            if (text == null || pattern == null)
            {
                return -1;
            }

            int textLength = text.Length;
            int patternLength = pattern.Length;
            if (patternLength == 0)
            {
                return 0;
            }

            if (patternLength > textLength)
            {
                return -1;
            }

            Dictionary<char, int> shift = new Dictionary<char, int>();
            for (int i = 0; i < patternLength - 1; i++)
            {
                shift[pattern[i]] = patternLength - i - 1;
            }

            int index = 0;
            while (index <= textLength - patternLength)
            {
                int j = patternLength - 1;
                while (j >= 0 && text[index + j] == pattern[j])
                {
                    j--;
                }

                if (j < 0)
                {
                    return index;
                }

                char badChar = text[index + patternLength - 1];
                int step = shift.ContainsKey(badChar) ? shift[badChar] : patternLength;
                index += step;
            }

            return -1;
        }
    }
}