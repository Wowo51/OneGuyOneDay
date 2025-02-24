using System;
using System.Collections.Generic;

namespace LZWLSyllableVariant
{
    public static class SyllableSplitter
    {
        public static List<string> Split(string input)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(input))
            {
                return result;
            }

            char[] delimiters = new char[]
            {
                ' '
            };
            string[] words = input.Split(delimiters, StringSplitOptions.None);
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                List<string> syllables = SplitWordIntoSyllables(word);
                result.AddRange(syllables);
                if (i < words.Length - 1)
                {
                    result.Add(" ");
                }
            }

            return result;
        }

        private static List<string> SplitWordIntoSyllables(string word)
        {
            List<string> syllables = new List<string>();
            if (string.IsNullOrEmpty(word))
            {
                return syllables;
            }

            string vowels = "aeiouAEIOU";
            int start = 0;
            for (int i = 1; i < word.Length; i++)
            {
                if (vowels.IndexOf(word[i]) >= 0)
                {
                    string part = word.Substring(start, i - start);
                    if (part != "")
                    {
                        syllables.Add(part);
                        start = i;
                    }
                }
            }

            string lastPart = word.Substring(start);
            if (lastPart != "")
            {
                syllables.Add(lastPart);
            }

            return syllables;
        }
    }
}