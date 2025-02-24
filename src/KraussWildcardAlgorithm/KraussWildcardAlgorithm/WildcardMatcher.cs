using System;

namespace KraussWildcardAlgorithm
{
    public static class WildcardMatcher
    {
        public static bool IsMatch(string text, string pattern)
        {
            if (text == null || pattern == null)
            {
                return false;
            }

            int textLen = text.Length;
            int patternLen = pattern.Length;
            int i = 0;
            int j = 0;
            int starIndex = -1;
            int matchIndex = 0;
            while (i < textLen)
            {
                if (j < patternLen && (pattern[j] == '?' || pattern[j] == text[i]))
                {
                    i++;
                    j++;
                }
                else if (j < patternLen && pattern[j] == '*')
                {
                    starIndex = j;
                    matchIndex = i;
                    j++;
                }
                else if (starIndex != -1)
                {
                    j = starIndex + 1;
                    matchIndex++;
                    i = matchIndex;
                }
                else
                {
                    return false;
                }
            }

            while (j < patternLen && pattern[j] == '*')
            {
                j++;
            }

            return j == patternLen;
        }
    }
}