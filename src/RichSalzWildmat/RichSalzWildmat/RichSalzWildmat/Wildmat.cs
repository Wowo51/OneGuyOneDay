using System;

namespace RichSalzWildmat
{
    public static class Wildmat
    {
        public static bool Match(string pattern, string text)
        {
            if (pattern == null || text == null)
            {
                return false;
            }

            return MatchRecursive(pattern, 0, text, 0);
        }

        private static bool MatchRecursive(string pattern, int pIndex, string text, int tIndex)
        {
            int pLen = pattern.Length;
            int tLen = text.Length;
            while (pIndex < pLen)
            {
                char c = pattern[pIndex];
                if (c == '*')
                {
                    // Skip consecutive '*' characters
                    while (pIndex + 1 < pLen && pattern[pIndex + 1] == '*')
                    {
                        pIndex++;
                    }

                    pIndex++;
                    if (pIndex >= pLen)
                    {
                        return true;
                    }

                    while (tIndex < tLen)
                    {
                        if (MatchRecursive(pattern, pIndex, text, tIndex))
                        {
                            return true;
                        }

                        tIndex++;
                    }

                    return false;
                }
                else if (c == '?')
                {
                    if (tIndex >= tLen)
                    {
                        return false;
                    }

                    pIndex++;
                    tIndex++;
                }
                else if (c == '[')
                {
                    if (tIndex >= tLen)
                    {
                        return false;
                    }

                    pIndex++;
                    bool negate = false;
                    if (pIndex < pLen && (pattern[pIndex] == '!' || pattern[pIndex] == '^'))
                    {
                        negate = true;
                        pIndex++;
                    }

                    bool match = false;
                    bool error = true;
                    char test = text[tIndex];
                    while (pIndex < pLen && pattern[pIndex] != ']')
                    {
                        error = false;
                        char start = pattern[pIndex];
                        if (pIndex + 2 < pLen && pattern[pIndex + 1] == '-' && pattern[pIndex + 2] != ']')
                        {
                            char end = pattern[pIndex + 2];
                            if (start <= test && test <= end)
                            {
                                match = true;
                            }

                            pIndex += 3;
                        }
                        else
                        {
                            if (start == test)
                            {
                                match = true;
                            }

                            pIndex++;
                        }
                    }

                    if (error || pIndex >= pLen || pattern[pIndex] != ']')
                    {
                        return false;
                    }

                    pIndex++; // Skip closing ']'
                    if (match == negate)
                    {
                        return false;
                    }

                    tIndex++;
                }
                else
                {
                    if (tIndex >= tLen || c != text[tIndex])
                    {
                        return false;
                    }

                    pIndex++;
                    tIndex++;
                }
            }

            return tIndex == tLen;
        }
    }
}