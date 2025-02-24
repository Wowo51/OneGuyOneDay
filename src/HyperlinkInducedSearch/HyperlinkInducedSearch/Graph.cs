using System;
using System.Collections.Generic;

namespace HyperlinkInducedSearch
{
    public class Graph
    {
        private Dictionary<string, List<string>> outgoingLinks = new Dictionary<string, List<string>>();
        public Graph()
        {
        }

        public void AddEdge(string source, string target)
        {
            if (!outgoingLinks.ContainsKey(source))
            {
                outgoingLinks[source] = new List<string>();
            }

            outgoingLinks[source].Add(target);
            if (!outgoingLinks.ContainsKey(target))
            {
                outgoingLinks[target] = new List<string>();
            }
        }

        public List<string> GetNodes()
        {
            List<string> nodes = new List<string>();
            foreach (string key in outgoingLinks.Keys)
            {
                if (!nodes.Contains(key))
                {
                    nodes.Add(key);
                }
            }

            return nodes;
        }

        public List<string> GetOutgoing(string node)
        {
            if (outgoingLinks.ContainsKey(node))
            {
                return outgoingLinks[node];
            }

            return new List<string>();
        }

        public List<string> GetIncoming(string node)
        {
            List<string> incoming = new List<string>();
            foreach (KeyValuePair<string, List<string>> entry in outgoingLinks)
            {
                string source = entry.Key;
                List<string> targets = entry.Value;
                if (targets.Contains(node))
                {
                    incoming.Add(source);
                }
            }

            return incoming;
        }
    }
}