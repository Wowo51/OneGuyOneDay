using System;
using System.Collections.Generic;

namespace LongestCommonSubstring
{
    public static class LongestCommonSubstringFinder
    {
        public static List<string> FindLongestCommonSubstrings(string[] inputs)
        {
            if (inputs == null || inputs.Length < 2)
            {
                return new List<string>();
            }

            string reference = inputs[0] ?? "";
            int refLength = reference.Length;
            for (int length = refLength; length >= 1; length--)
            {
                HashSet<string> commonSubstrings = new HashSet<string>();
                for (int i = 0; i <= refLength - length; i++)
                {
                    string candidate = reference.Substring(i, length);
                    bool isCommon = true;
                    for (int j = 1; j < inputs.Length; j++)
                    {
                        string current = inputs[j] ?? "";
                        if (!current.Contains(candidate))
                        {
                            isCommon = false;
                            break;
                        }
                    }

                    if (isCommon)
                    {
                        commonSubstrings.Add(candidate);
                    }
                }

                if (commonSubstrings.Count > 0)
                {
                    List<string> result = new List<string>(commonSubstrings);
                    return result;
                }
            }

            return new List<string>();
        }
    }
}