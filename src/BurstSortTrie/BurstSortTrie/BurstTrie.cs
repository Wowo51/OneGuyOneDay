using System;
using System.Collections.Generic;

namespace BurstSortTrie
{
    public class BurstTrie
    {
        private TrieNode _root;
        public BurstTrie()
        {
            _root = new BurstTrieLeaf();
        }

        public void Insert(string word)
        {
            if (word == null)
            {
                // Treat null as empty string.
                word = string.Empty;
            }

            _root = _root.Insert(word, 0);
        }

        public IEnumerable<string> GetSortedWords()
        {
            return _root.Traverse(string.Empty);
        }
    }

    internal abstract class TrieNode
    {
        public abstract TrieNode Insert(string word, int index);
        public abstract IEnumerable<string> Traverse(string prefix);
    }

    internal class BurstTrieLeaf : TrieNode
    {
        private const int BurstThreshold = 10;
        private List<string> _words;
        public BurstTrieLeaf()
        {
            _words = new List<string>();
        }

        public override TrieNode Insert(string word, int index)
        {
            if (_words.Count < BurstThreshold)
            {
                _words.Add(word);
                return this;
            }
            else
            {
                BurstTrieInternal internalNode = new BurstTrieInternal();
                int i = 0;
                for (i = 0; i < _words.Count; i++)
                {
                    internalNode.Insert(_words[i], index);
                }

                return internalNode.Insert(word, index);
            }
        }

        public override IEnumerable<string> Traverse(string prefix)
        {
            // The stored words are complete; sort them lexicographically.
            List<string> sortedList = new List<string>(_words);
            sortedList.Sort(StringComparer.Ordinal);
            foreach (string word in sortedList)
            {
                yield return word;
            }
        }
    }

    internal class BurstTrieInternal : TrieNode
    {
        private SortedDictionary<char, TrieNode> _children;
        public BurstTrieInternal()
        {
            _children = new SortedDictionary<char, TrieNode>();
        }

        public override TrieNode Insert(string word, int index)
        {
            char key;
            if (index < word.Length)
            {
                key = word[index];
            }
            else
            {
                key = '\0';
            }

            TrieNode child;
            if (_children.ContainsKey(key))
            {
                child = _children[key];
            }
            else
            {
                child = new BurstTrieLeaf();
            }

            child = child.Insert(word, index + 1);
            _children[key] = child;
            return this;
        }

        public override IEnumerable<string> Traverse(string prefix)
        {
            foreach (KeyValuePair<char, TrieNode> kvp in _children)
            {
                char key = kvp.Key;
                TrieNode child = kvp.Value;
                string newPrefix;
                if (key == '\0')
                {
                    newPrefix = prefix;
                }
                else
                {
                    newPrefix = prefix + key;
                }

                foreach (string word in child.Traverse(newPrefix))
                {
                    yield return word;
                }
            }
        }
    }
}