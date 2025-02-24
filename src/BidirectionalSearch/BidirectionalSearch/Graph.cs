using System;
using System.Collections.Generic;

namespace BidirectionalSearch
{
    public class Graph
    {
        private Dictionary<string, List<string>> _adjacencyList;
        public Graph()
        {
            _adjacencyList = new Dictionary<string, List<string>>();
        }

        public void AddNode(string node)
        {
            if (!_adjacencyList.ContainsKey(node))
            {
                _adjacencyList[node] = new List<string>();
            }
        }

        public void AddEdge(string from, string to)
        {
            AddNode(from);
            AddNode(to);
            _adjacencyList[from].Add(to);
            _adjacencyList[to].Add(from);
        }

        public List<string> GetNeighbors(string node)
        {
            if (_adjacencyList.ContainsKey(node))
            {
                return _adjacencyList[node];
            }

            return new List<string>();
        }
    }
}