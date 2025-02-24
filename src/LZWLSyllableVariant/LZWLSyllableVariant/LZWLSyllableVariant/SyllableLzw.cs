using System;
using System.Collections.Generic;

namespace LZWLSyllableVariant
{
    public static class SyllableLzw
    {
        public static CompressionResult Compress(string? text)
        {
            if (text == null)
            {
                return new CompressionResult(new List<int>(), new Dictionary<int, SyllableSequence>());
            }

            List<string> syllables = SyllableSplitter.Split(text);
            int length = syllables.Count;
            if (length == 0)
            {
                return new CompressionResult(new List<int>(), new Dictionary<int, SyllableSequence>());
            }

            Dictionary<SyllableSequence, int> dictionary = new Dictionary<SyllableSequence, int>();
            for (int i = 0; i < length; i++)
            {
                string syllable = syllables[i];
                SyllableSequence seq = new SyllableSequence(new List<string> { syllable });
                if (!dictionary.ContainsKey(seq))
                {
                    dictionary.Add(seq, dictionary.Count);
                }
            }

            List<int> compressed = new List<int>();
            SyllableSequence w = new SyllableSequence(new List<string> { syllables[0] });
            for (int i = 1; i < length; i++)
            {
                string c = syllables[i];
                SyllableSequence wc = w.Append(c);
                if (dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    compressed.Add(dictionary[w]);
                    dictionary.Add(wc, dictionary.Count);
                    w = new SyllableSequence(new List<string> { c });
                }
            }

            compressed.Add(dictionary[w]);
            Dictionary<int, SyllableSequence> initialDictionary = ConvertDictionary(dictionary);
            return new CompressionResult(compressed, initialDictionary);
        }

        private static Dictionary<int, SyllableSequence> ConvertDictionary(Dictionary<SyllableSequence, int> sourceDictionary)
        {
            Dictionary<int, SyllableSequence> result = new Dictionary<int, SyllableSequence>();
            foreach (KeyValuePair<SyllableSequence, int> pair in sourceDictionary)
            {
                result.Add(pair.Value, pair.Key);
            }

            return result;
        }

        public static string Decompress(CompressionResult? result)
        {
            if (result == null)
            {
                return string.Empty;
            }

            return DecompressionHelper.Decompress(result.CompressedData, result.InitialDictionary);
        }
    }
}