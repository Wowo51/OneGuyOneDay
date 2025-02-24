using System;
using System.Collections.Generic;
using System.Text;

namespace ShannonFanoCoding
{
    public class ShannonFanoEncoder
    {
        public Dictionary<char, string> BuildCodes(string input)
        {
            if (input == null)
            {
                return new Dictionary<char, string>();
            }

            Dictionary<char, int> frequencies = new Dictionary<char, int>();
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (frequencies.ContainsKey(ch))
                {
                    frequencies[ch] = frequencies[ch] + 1;
                }
                else
                {
                    frequencies[ch] = 1;
                }
            }

            List<KeyValuePair<char, int>> symbolList = new List<KeyValuePair<char, int>>(frequencies);
            symbolList.Sort(delegate (KeyValuePair<char, int> pair1, KeyValuePair<char, int> pair2)
            {
                return pair2.Value.CompareTo(pair1.Value);
            });
            Dictionary<char, string> codeMap = new Dictionary<char, string>();
            BuildCodesRecursive(symbolList, codeMap, "");
            return codeMap;
        }

        private void BuildCodesRecursive(List<KeyValuePair<char, int>> symbols, Dictionary<char, string> codeMap, string prefix)
        {
            if (symbols == null || symbols.Count == 0)
            {
                return;
            }

            if (symbols.Count == 1)
            {
                char symbol = symbols[0].Key;
                codeMap[symbol] = (prefix == "" ? "0" : prefix);
                return;
            }

            int totalFrequency = 0;
            for (int i = 0; i < symbols.Count; i++)
            {
                totalFrequency += symbols[i].Value;
            }

            int sumSoFar = 0;
            int splitIndex = 0;
            int minDifference = int.MaxValue;
            for (int i = 0; i < symbols.Count; i++)
            {
                sumSoFar += symbols[i].Value;
                int difference = Math.Abs(totalFrequency - 2 * sumSoFar);
                if (difference < minDifference)
                {
                    minDifference = difference;
                    splitIndex = i;
                }
            }

            List<KeyValuePair<char, int>> leftSymbols = symbols.GetRange(0, splitIndex + 1);
            List<KeyValuePair<char, int>> rightSymbols = symbols.GetRange(splitIndex + 1, symbols.Count - splitIndex - 1);
            BuildCodesRecursive(leftSymbols, codeMap, prefix + "0");
            BuildCodesRecursive(rightSymbols, codeMap, prefix + "1");
        }

        public string Encode(string input)
        {
            if (input == null)
            {
                return "";
            }

            Dictionary<char, string> codes = BuildCodes(input);
            StringBuilder encodedBuilder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (codes.ContainsKey(ch))
                {
                    encodedBuilder.Append(codes[ch]);
                }
            }

            return encodedBuilder.ToString();
        }

        public string Decode(string encoded, Dictionary<char, string> codeMap)
        {
            if (encoded == null || codeMap == null)
            {
                return "";
            }

            Dictionary<string, char> reverseCodeMap = new Dictionary<string, char>();
            foreach (KeyValuePair<char, string> pair in codeMap)
            {
                reverseCodeMap[pair.Value] = pair.Key;
            }

            StringBuilder decodedBuilder = new StringBuilder();
            string currentCode = "";
            for (int i = 0; i < encoded.Length; i++)
            {
                currentCode += encoded[i];
                if (reverseCodeMap.ContainsKey(currentCode))
                {
                    decodedBuilder.Append(reverseCodeMap[currentCode]);
                    currentCode = "";
                }
            }

            return decodedBuilder.ToString();
        }
    }
}