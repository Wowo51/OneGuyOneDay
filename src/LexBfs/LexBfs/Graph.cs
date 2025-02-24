using System;
using System.Collections.Generic;

namespace LexBfs
{
    public class Graph<T>
        where T : notnull
    {
        private readonly Dictionary<T, HashSet<T>> _adjacencyList;
        public Graph()
        {
            _adjacencyList = new Dictionary<T, HashSet<T>>();
        }

        public void AddVertex(T vertex)
        {
            if (!_adjacencyList.ContainsKey(vertex))
            {
                _adjacencyList[vertex] = new HashSet<T>();
            }
        }

        public void AddEdge(T vertex1, T vertex2)
        {
            AddVertex(vertex1);
            AddVertex(vertex2);
            _adjacencyList[vertex1].Add(vertex2);
            _adjacencyList[vertex2].Add(vertex1);
        }

        public IEnumerable<T> Vertices
        {
            get
            {
                return _adjacencyList.Keys;
            }
        }

        public IEnumerable<T> GetNeighbors(T vertex)
        {
            if (_adjacencyList.ContainsKey(vertex))
            {
                return _adjacencyList[vertex];
            }

            return new List<T>();
        }
    }
}