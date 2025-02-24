using System;
using System.Collections.Generic;

namespace BurstSortTrie
{
    public static class BurstSorter
    {
        public static List<string> Sort(List<string> words)
        {
            if (words == null)
            {
                return new List<string>();
            }

            BurstTrie burstTrie = new BurstTrie();
            foreach (string word in words)
            {
                if (word == null)
                {
                    // Treat null values as empty strings.
                    burstTrie.Insert(string.Empty);
                }
                else
                {
                    burstTrie.Insert(word);
                }
            }

            List<string> sortedList = new List<string>(burstTrie.GetSortedWords());
            return sortedList;
        }
    }
}