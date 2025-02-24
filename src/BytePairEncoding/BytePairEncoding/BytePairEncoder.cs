using System;
using System.Collections.Generic;

namespace BytePairEncoding
{
    public class BytePairEncoder
    {
        public static List<string> Encode(string input, int mergeOperations)
        {
            if (input == null)
            {
                return new List<string>();
            }

            List<string> tokens = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                tokens.Add(input[i].ToString());
            }

            int remaining = mergeOperations;
            while (remaining > 0)
            {
                Dictionary<string, int> pairCounts = CountPairs(tokens);
                if (pairCounts.Count == 0)
                {
                    break;
                }

                string? mostFrequentPair = null;
                int maxCount = -1;
                foreach (KeyValuePair<string, int> pair in pairCounts)
                {
                    if (pair.Value > maxCount)
                    {
                        maxCount = pair.Value;
                        mostFrequentPair = pair.Key;
                    }
                }

                if (mostFrequentPair == null || maxCount < 2)
                {
                    break;
                }

                string[] mergePair = mostFrequentPair!.Split(' ');
                string firstToken = mergePair[0];
                string secondToken = mergePair[1];
                List<string> newTokens = new List<string>();
                int index = 0;
                while (index < tokens.Count)
                {
                    if (index < tokens.Count - 1 && tokens[index] == firstToken && tokens[index + 1] == secondToken)
                    {
                        newTokens.Add(firstToken + secondToken);
                        index += 2;
                    }
                    else
                    {
                        newTokens.Add(tokens[index]);
                        index++;
                    }
                }

                tokens = newTokens;
                remaining--;
            }

            return tokens;
        }

        public static string EncodeToString(string input, int mergeOperations)
        {
            List<string> tokens = Encode(input, mergeOperations);
            return string.Join(" ", tokens);
        }

        private static Dictionary<string, int> CountPairs(List<string> tokens)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            for (int i = 0; i < tokens.Count - 1; i++)
            {
                string pair = tokens[i] + " " + tokens[i + 1];
                if (counts.ContainsKey(pair))
                {
                    counts[pair]++;
                }
                else
                {
                    counts[pair] = 1;
                }
            }

            return counts;
        }
    }
}