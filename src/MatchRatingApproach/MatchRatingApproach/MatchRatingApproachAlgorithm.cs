using System;
using System.Text;
using System.Collections.Generic;

namespace MatchRatingApproach
{
    public static class MatchRatingApproachAlgorithm
    {
        public static bool IsSimilar(string first, string second)
        {
            if (first == null || second == null)
            {
                return false;
            }

            string normalizedFirst = first.Trim().ToUpperInvariant();
            string normalizedSecond = second.Trim().ToUpperInvariant();
            if (Math.Abs(normalizedFirst.Length - normalizedSecond.Length) > 3)
            {
                return false;
            }

            string code1 = EncodeNameForMRA(normalizedFirst);
            string code2 = EncodeNameForMRA(normalizedSecond);
            if (string.IsNullOrEmpty(code1) || string.IsNullOrEmpty(code2))
            {
                return false;
            }

            // Enforce that the first and last letters match.
            if (code1[0] != code2[0] || code1[code1.Length - 1] != code2[code2.Length - 1])
            {
                return false;
            }

            // Save the full minimum length for threshold calculation.
            int fullMinLength = code1.Length < code2.Length ? code1.Length : code2.Length;
            // Remove the first and last letters for the inner comparison.
            string innerCode1 = code1.Length > 2 ? code1.Substring(1, code1.Length - 2) : string.Empty;
            string innerCode2 = code2.Length > 2 ? code2.Substring(1, code2.Length - 2) : string.Empty;
            int matches = GetMatchingCharactersCount(innerCode1, innerCode2);
            int threshold;
            if (fullMinLength <= 2)
            {
                threshold = 5;
            }
            else if (fullMinLength == 3)
            {
                threshold = 4;
            }
            else if (fullMinLength == 4)
            {
                threshold = 3;
            }
            else
            {
                threshold = 2;
            }

            return matches >= threshold;
        }

        private static string EncodeNameForMRA(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            if (input.Length <= 2)
            {
                return input;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(input[0]);
            for (int i = 1; i < input.Length - 1; i++)
            {
                if (!IsVowel(input[i]))
                {
                    stringBuilder.Append(input[i]);
                }
            }

            stringBuilder.Append(input[input.Length - 1]);
            return stringBuilder.ToString();
        }

        private static bool IsVowel(char character)
        {
            char normalized = Char.ToUpperInvariant(character);
            return normalized == 'A' || normalized == 'E' || normalized == 'I' || normalized == 'O' || normalized == 'U';
        }

        private static int GetMatchingCharactersCount(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            {
                return 0;
            }

            int matchingCount = 0;
            List<char> s2Chars = new List<char>();
            for (int i = 0; i < s2.Length; i++)
            {
                s2Chars.Add(s2[i]);
            }

            for (int i = 0; i < s1.Length; i++)
            {
                char letter = s1[i];
                if (s2Chars.Contains(letter))
                {
                    matchingCount++;
                    s2Chars.Remove(letter);
                }
            }

            return matchingCount;
        }
    }
}