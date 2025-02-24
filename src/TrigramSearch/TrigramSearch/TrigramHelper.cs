using System;
using System.Collections.Generic;

namespace TrigramSearch
{
    public static class TrigramHelper
    {
        public static HashSet<string> GetTrigrams(string text)
        {
            if (text == null)
            {
                return new HashSet<string>();
            }

            string trimmed = text.Trim();
            HashSet<string> trigrams = new HashSet<string>();
            if (trimmed.Length < 3)
            {
                if (!string.IsNullOrEmpty(trimmed))
                {
                    trigrams.Add(trimmed);
                }

                return trigrams;
            }

            for (int i = 0; i <= trimmed.Length - 3; i++)
            {
                string trigram = trimmed.Substring(i, 3);
                trigrams.Add(trigram);
            }

            return trigrams;
        }
    }
}