using System;
using System.Collections.Generic;
using System.Text;

namespace LempelZivWelch
{
    public static class Lzw
    {
        public static List<int> Compress(string uncompressed)
        {
            if (uncompressed == null)
            {
                return new List<int>();
            }

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            string w = "";
            List<int> result = new List<int>();
            foreach (char c in uncompressed)
            {
                string wc = w + c;
                if (dictionary.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    result.Add(dictionary[w]);
                    dictionary.Add(wc, dictionary.Count);
                    w = c.ToString();
                }
            }

            if (w != "")
            {
                result.Add(dictionary[w]);
            }

            return result;
        }

        public static string Decompress(List<int> compressed)
        {
            if (compressed == null || compressed.Count == 0)
            {
                return "";
            }

            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(i, ((char)i).ToString());
            }

            int index = compressed[0];
            string w = dictionary[index];
            StringBuilder result = new StringBuilder();
            result.Append(w);
            for (int i = 1; i < compressed.Count; i++)
            {
                int k = compressed[i];
                string entry;
                if (dictionary.ContainsKey(k))
                {
                    entry = dictionary[k];
                }
                else
                {
                    entry = w + w[0];
                }

                result.Append(entry);
                dictionary.Add(dictionary.Count, w + entry[0]);
                w = entry;
            }

            return result.ToString();
        }
    }
}