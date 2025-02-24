using System;
using System.Collections.Generic;

namespace LZWLSyllableVariant
{
    internal static class DecompressionHelper
    {
        public static string Decompress(List<int>? compressed, Dictionary<int, SyllableSequence> initialDictionary)
        {
            if (initialDictionary == null || compressed == null || compressed.Count == 0)
            {
                return string.Empty;
            }

            Dictionary<int, SyllableSequence> dictionary = new Dictionary<int, SyllableSequence>(initialDictionary);
            int dictSize = dictionary.Count;
            int firstCode = compressed[0];
            if (!dictionary.TryGetValue(firstCode, out SyllableSequence? temp))
            {
                return string.Empty;
            }

            SyllableSequence w = temp;
            List<string> resultSyllables = new List<string>(w.Tokens);
            SyllableSequence entry;
            for (int i = 1; i < compressed.Count; i++)
            {
                int k = compressed[i];
                if (dictionary.ContainsKey(k))
                {
                    entry = dictionary[k];
                }
                else
                {
                    entry = w.Append(w.Tokens[0]);
                }

                resultSyllables.AddRange(entry.Tokens);
                dictionary.Add(dictSize, w.Append(entry.Tokens[0]));
                dictSize++;
                w = entry;
            }

            string result = string.Join(string.Empty, resultSyllables);
            return result;
        }
    }
}