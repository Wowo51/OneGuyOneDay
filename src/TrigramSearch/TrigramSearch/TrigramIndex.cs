using System;
using System.Collections.Generic;

namespace TrigramSearch
{
    public class TrigramIndex
    {
        private Dictionary<string, HashSet<string>> _index;
        public TrigramIndex()
        {
            _index = new Dictionary<string, HashSet<string>>();
        }

        public void AddDocument(string docId, string content)
        {
            if (docId == null)
            {
                return;
            }

            HashSet<string> trigrams = TrigramHelper.GetTrigrams(content);
            _index[docId] = trigrams;
        }

        public List<string> Search(string query, double threshold)
        {
            HashSet<string> queryTrigrams = TrigramHelper.GetTrigrams(query);
            List<string> matches = new List<string>();
            foreach (KeyValuePair<string, HashSet<string>> document in _index)
            {
                HashSet<string> docTrigrams = document.Value;
                int intersectCount = 0;
                foreach (string trigram in queryTrigrams)
                {
                    if (docTrigrams.Contains(trigram))
                    {
                        intersectCount++;
                    }
                }

                int unionCount = queryTrigrams.Count + docTrigrams.Count - intersectCount;
                double similarity = unionCount > 0 ? ((double)intersectCount) / unionCount : 0.0;
                if (similarity >= threshold)
                {
                    matches.Add(document.Key);
                }
            }

            return matches;
        }
    }
}