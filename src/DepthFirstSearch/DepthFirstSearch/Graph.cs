using System.Collections.Generic;

namespace DepthFirstSearch
{
    public class Graph<T>
        where T : notnull
    {
        private Dictionary<T, List<T>> _adjList;
        public Graph()
        {
            _adjList = new Dictionary<T, List<T>>();
        }

        public void AddEdge(T source, T target)
        {
            if (!_adjList.ContainsKey(source))
            {
                _adjList[source] = new List<T>();
            }

            if (!_adjList.ContainsKey(target))
            {
                _adjList[target] = new List<T>();
            }

            _adjList[source].Add(target);
        }

        public List<T> GetNeighbors(T vertex)
        {
            if (_adjList.ContainsKey(vertex))
            {
                return _adjList[vertex];
            }

            return new List<T>();
        }
    }
}