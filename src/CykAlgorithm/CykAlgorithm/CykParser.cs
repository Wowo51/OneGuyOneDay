using System;
using System.Collections.Generic;

namespace CykAlgorithm
{
    public class CykParser
    {
        public bool Parse(Grammar grammar, string input)
        {
            int inputLength = (input != null ? input.Length : 0);
            if (inputLength == 0)
            {
                return false;
            }

            // Create a table where table[i, j] holds the set of nonterminals
            // that can derive the substring of length j+1 starting at position i.
            HashSet<string>[, ] table = new HashSet<string>[inputLength, inputLength];
            for (int i = 0; i < inputLength; i++)
            {
                for (int j = 0; j < inputLength; j++)
                {
                    table[i, j] = new HashSet<string>();
                }
            }

            // Initialize table for substrings of length 1.
            for (int i = 0; i < inputLength; i++)
            {
                char currentChar = input[i];
                foreach (Production production in grammar.Productions)
                {
                    if (production.Right.Count == 1 && production.Right[0] == currentChar.ToString())
                    {
                        table[i, 0].Add(production.Left);
                    }
                }
            }

            // Build the table for substrings of length >= 2.
            for (int L = 2; L <= inputLength; L++)
            {
                for (int i = 0; i <= inputLength - L; i++)
                {
                    int lIndex = L - 1;
                    for (int s = 1; s < L; s++)
                    {
                        HashSet<string> leftSet = table[i, s - 1];
                        HashSet<string> rightSet = table[i + s, L - s - 1];
                        foreach (string A in leftSet)
                        {
                            foreach (string B in rightSet)
                            {
                                foreach (Production production in grammar.Productions)
                                {
                                    if (production.Right.Count == 2 && production.Right[0] == A && production.Right[1] == B)
                                    {
                                        table[i, lIndex].Add(production.Left);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Accept if the start symbol derives the entire input.
            return table[0, inputLength - 1].Contains(grammar.StartSymbol);
        }
    }
}