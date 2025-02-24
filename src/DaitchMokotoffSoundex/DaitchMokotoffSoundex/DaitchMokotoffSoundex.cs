using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace DaitchMokotoffSoundex
{
    public static class DaitchMokotoffSoundex
    {
        public static string[] Encode(string? surname)
        {
            if (surname == null)
            {
                return new string[0];
            }

            // Remove diacritics to normalize Slavic characters.
            string normalizedSurname = NormalizeSurname(surname);
            string input = normalizedSurname.ToUpperInvariant();
            List<string> results = new List<string>();
            EncodeRecursive(input, 0, "", results);
            HashSet<string> uniqueCodes = new HashSet<string>();
            foreach (string code in results)
            {
                string formatted = PadOrTrim(code);
                uniqueCodes.Add(formatted);
            }

            string[] output = new string[uniqueCodes.Count];
            int i = 0;
            foreach (string code in uniqueCodes)
            {
                output[i++] = code;
            }

            return output;
        }

        private static string NormalizeSurname(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalizedString)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private static void EncodeRecursive(string input, int index, string current, List<string> results)
        {
            if (index >= input.Length)
            {
                results.Add(current);
                return;
            }

            // Handle three-letter sequence SCH.
            if (index + 3 <= input.Length && input.Substring(index, 3) == "SCH")
            {
                if (index == 0)
                {
                    // At start, allow both alternative mappings.
                    EncodeRecursive(input, index + 3, current + "4", results);
                    EncodeRecursive(input, index + 3, current + "5", results);
                }
                else
                {
                    EncodeRecursive(input, index + 3, current + "4", results);
                }

                return;
            }

            // Handle two-letter sequence DZ.
            if (index + 2 <= input.Length && input.Substring(index, 2) == "DZ")
            {
                EncodeRecursive(input, index + 2, current + "3", results);
                return;
            }

            if (index + 2 <= input.Length)
            {
                string twoChars = input.Substring(index, 2);
                if (twoChars == "CH" || twoChars == "CK")
                {
                    EncodeRecursive(input, index + 2, current + "5", results);
                    return;
                }
                else if (twoChars == "PH" || twoChars == "PF")
                {
                    EncodeRecursive(input, index + 2, current + "8", results);
                    return;
                }
                else if (twoChars == "DT" || twoChars == "DD" || twoChars == "TT")
                {
                    EncodeRecursive(input, index + 2, current + "3", results);
                    return;
                }
            }

            char currentChar = input[index];
            List<string> mappings = GetMappings(currentChar, index);
            foreach (string mapping in mappings)
            {
                EncodeRecursive(input, index + 1, current + mapping, results);
            }
        }

        private static string PadOrTrim(string code)
        {
            if (code.Length < 6)
            {
                StringBuilder padded = new StringBuilder(code);
                while (padded.Length < 6)
                {
                    padded.Append("0");
                }

                return padded.ToString();
            }
            else if (code.Length > 6)
            {
                return code.Substring(0, 6);
            }

            return code;
        }

        private static List<string> GetMappings(char c, int index)
        {
            List<string> mappings = new List<string>();
            // Vowels and specific consonants are dropped.
            if ("AEIOUYHW".IndexOf(c) >= 0)
            {
                mappings.Add("");
                return mappings;
            }

            switch (c)
            {
                case 'B':
                case 'P':
                    mappings.Add("7");
                    break;
                case 'F':
                case 'V':
                    mappings.Add("8");
                    break;
                case 'C':
                    if (index == 0)
                    {
                        mappings.Add("4");
                        mappings.Add("5");
                    }
                    else
                    {
                        mappings.Add("5");
                    }

                    break;
                case 'K':
                case 'G':
                case 'Q':
                    mappings.Add("5");
                    break;
                case 'J':
                    mappings.Add("4");
                    break;
                case 'S':
                case 'Z':
                    mappings.Add("4");
                    break;
                case 'X':
                    mappings.Add("5");
                    break;
                case 'D':
                case 'T':
                    mappings.Add("3");
                    break;
                case 'L':
                    mappings.Add("8");
                    break;
                case 'M':
                case 'N':
                    mappings.Add("6");
                    break;
                case 'R':
                    mappings.Add("9");
                    break;
                default:
                    mappings.Add("");
                    break;
            }

            return mappings;
        }
    }
}