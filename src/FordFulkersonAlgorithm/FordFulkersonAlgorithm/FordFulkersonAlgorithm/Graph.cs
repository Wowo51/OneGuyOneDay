using System.Collections.Generic;

namespace FordFulkersonAlgorithm
{
    public class Graph
    {
        public int VertexCount { get; }
        public List<Edge>[] AdjacencyList { get; }

        public Graph(int vertexCount)
        {
            VertexCount = vertexCount;
            AdjacencyList = new List<Edge>[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                AdjacencyList[i] = new List<Edge>();
            }
        }

        public void AddEdge(int from, int to, int capacity)
        {
            Edge forward = new Edge(from, to, capacity);
            Edge backward = new Edge(to, from, 0);
            forward.Reverse = backward;
            backward.Reverse = forward;
            AdjacencyList[from].Add(forward);
            AdjacencyList[to].Add(backward);
        }
    }
}