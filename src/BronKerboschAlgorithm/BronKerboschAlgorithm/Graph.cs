using System;
using System.Collections.Generic;
using System.Linq;

namespace BronKerboschAlgorithm
{
    public class Graph
    {
        private Dictionary<int, HashSet<int>> _adjacencyList = new Dictionary<int, HashSet<int>>();
        public void AddVertex(int vertex)
        {
            if (!_adjacencyList.ContainsKey(vertex))
            {
                _adjacencyList[vertex] = new HashSet<int>();
            }
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            AddVertex(vertex1);
            AddVertex(vertex2);
            _adjacencyList[vertex1].Add(vertex2);
            _adjacencyList[vertex2].Add(vertex1);
        }

        public IEnumerable<int> Vertices => _adjacencyList.Keys;

        public HashSet<int> Neighbors(int vertex)
        {
            if (_adjacencyList.ContainsKey(vertex))
            {
                return new HashSet<int>(_adjacencyList[vertex]);
            }

            return new HashSet<int>();
        }
    }
}