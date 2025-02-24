using System;
using System.Collections.Generic;

namespace KosarajuAlgorithm
{
    public class Graph
    {
        private Dictionary<int, List<int>> _adjacencyList;
        public Graph()
        {
            _adjacencyList = new Dictionary<int, List<int>>();
        }

        public void AddEdge(int from, int to)
        {
            if (!_adjacencyList.ContainsKey(from))
            {
                _adjacencyList[from] = new List<int>();
            }

            _adjacencyList[from].Add(to);
            if (!_adjacencyList.ContainsKey(to))
            {
                _adjacencyList[to] = new List<int>();
            }
        }

        public Dictionary<int, List<int>> AdjacencyList
        {
            get
            {
                return _adjacencyList;
            }
        }

        public Graph Reverse()
        {
            Graph reversed = new Graph();
            foreach (KeyValuePair<int, List<int>> kvp in _adjacencyList)
            {
                int vertex = kvp.Key;
                if (!reversed._adjacencyList.ContainsKey(vertex))
                {
                    reversed._adjacencyList[vertex] = new List<int>();
                }
            }

            foreach (KeyValuePair<int, List<int>> kvp in _adjacencyList)
            {
                int fromVertex = kvp.Key;
                foreach (int toVertex in kvp.Value)
                {
                    if (!reversed._adjacencyList.ContainsKey(toVertex))
                    {
                        reversed._adjacencyList[toVertex] = new List<int>();
                    }

                    reversed._adjacencyList[toVertex].Add(fromVertex);
                }
            }

            return reversed;
        }
    }
}