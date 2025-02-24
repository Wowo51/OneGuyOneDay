using System;
using System.Collections.Generic;

namespace LeskAlgorithm
{
    public static class LeskDisambiguator
    {
        public static string? Disambiguate(string context, Dictionary<string, string> definitions)
        {
            if (string.IsNullOrWhiteSpace(context) || definitions == null || definitions.Count == 0)
            {
                return null;
            }

            int bestOverlap = -1;
            string? bestSense = null;
            string[] contextTokens = Tokenize(context);
            foreach (KeyValuePair<string, string> entry in definitions)
            {
                string sense = entry.Key;
                string definition = entry.Value;
                int overlap = CalculateOverlap(contextTokens, Tokenize(definition));
                if (overlap > bestOverlap)
                {
                    bestOverlap = overlap;
                    bestSense = sense;
                }
            }

            return bestSense;
        }

        private static string[] Tokenize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new string[0];
            }

            string[] tokens = text.Split(new char[] { ' ', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].ToLowerInvariant();
            }

            return tokens;
        }

        private static int CalculateOverlap(string[] contextTokens, string[] definitionTokens)
        {
            if (contextTokens == null || definitionTokens == null)
            {
                return 0;
            }

            HashSet<string> contextSet = new HashSet<string>(contextTokens);
            int count = 0;
            for (int i = 0; i < definitionTokens.Length; i++)
            {
                if (contextSet.Contains(definitionTokens[i]))
                {
                    count++;
                }
            }

            return count;
        }
    }
}