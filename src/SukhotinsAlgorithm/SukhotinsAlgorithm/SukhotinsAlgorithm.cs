using System;
using System.Collections.Generic;

namespace SukhotinsAlgorithm
{
    public class ClassificationResult
    {
        public List<char> Vowels { get; set; }
        public List<char> Consonants { get; set; }

        public ClassificationResult()
        {
            Vowels = new List<char>();
            Consonants = new List<char>();
        }
    }

    public class SukhotinsAlgorithm
    {
        public static ClassificationResult Classify(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            Dictionary<char, Dictionary<char, int>> adjacency = BuildAdjacencyMatrix(text);
            if (adjacency.Count == 0)
            {
                return new ClassificationResult();
            }

            Dictionary<char, int> totalCounts = new Dictionary<char, int>();
            foreach (KeyValuePair<char, Dictionary<char, int>> kv in adjacency)
            {
                int sum = 0;
                foreach (KeyValuePair<char, int> inner in kv.Value)
                {
                    sum += inner.Value;
                }

                totalCounts[kv.Key] = sum;
            }

            char r = '\0';
            int maxCount = -1;
            foreach (KeyValuePair<char, int> kv in totalCounts)
            {
                if (kv.Value > maxCount)
                {
                    maxCount = kv.Value;
                    r = kv.Key;
                }
            }

            // Initialize vowels with the letter r.
            HashSet<char> vowels = new HashSet<char>();
            vowels.Add(r);
            // Iteratively add letters that have strong association with the vowel set.
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (char letter in adjacency.Keys)
                {
                    if (vowels.Contains(letter))
                    {
                        continue;
                    }

                    int vowelCount = 0;
                    foreach (char vowel in vowels)
                    {
                        if (adjacency[letter].TryGetValue(vowel, out int count))
                        {
                            vowelCount += count;
                        }
                    }

                    if (vowelCount * 2 > totalCounts[letter])
                    {
                        vowels.Add(letter);
                        changed = true;
                    }
                }
            }

            ClassificationResult result = new ClassificationResult();
            foreach (char letter in adjacency.Keys)
            {
                if (vowels.Contains(letter))
                {
                    result.Vowels.Add(letter);
                }
                else
                {
                    result.Consonants.Add(letter);
                }
            }

            return result;
        }

        private static Dictionary<char, Dictionary<char, int>> BuildAdjacencyMatrix(string text)
        {
            Dictionary<char, Dictionary<char, int>> matrix = new Dictionary<char, Dictionary<char, int>>();
            char previous = '\0';
            bool hasPrevious = false;
            for (int i = 0; i < text.Length; i++)
            {
                char current = text[i];
                if (!Char.IsLetter(current))
                {
                    hasPrevious = false;
                    continue;
                }

                current = Char.ToLower(current);
                if (!matrix.ContainsKey(current))
                {
                    matrix[current] = new Dictionary<char, int>();
                }

                if (hasPrevious)
                {
                    if (!matrix[previous].ContainsKey(current))
                    {
                        matrix[previous][current] = 0;
                    }

                    matrix[previous][current] = matrix[previous][current] + 1;
                    if (!matrix[current].ContainsKey(previous))
                    {
                        matrix[current][previous] = 0;
                    }

                    matrix[current][previous] = matrix[current][previous] + 1;
                }

                previous = current;
                hasPrevious = true;
            }

            return matrix;
        }
    }
}