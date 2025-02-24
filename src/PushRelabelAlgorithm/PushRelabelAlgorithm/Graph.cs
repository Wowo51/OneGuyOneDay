using System.Collections.Generic;

namespace PushRelabelAlgorithm
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Capacity { get; set; }
        public int Flow { get; set; }
        public Edge? Reverse { get; set; }

        public Edge(int from, int to, int capacity)
        {
            this.From = from;
            this.To = to;
            this.Capacity = capacity;
            this.Flow = 0;
            this.Reverse = null;
        }

        public int ResidualCapacity()
        {
            return Capacity - Flow;
        }
    }

    public class Graph
    {
        public int VertexCount { get; private set; }
        public List<Edge>[] AdjacencyList { get; private set; }

        public Graph(int vertexCount)
        {
            this.VertexCount = vertexCount;
            this.AdjacencyList = new List<Edge>[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                this.AdjacencyList[i] = new List<Edge>();
            }
        }

        public void AddEdge(int from, int to, int capacity)
        {
            Edge forward = new Edge(from, to, capacity);
            Edge reverse = new Edge(to, from, 0);
            forward.Reverse = reverse;
            reverse.Reverse = forward;
            this.AdjacencyList[from].Add(forward);
            this.AdjacencyList[to].Add(reverse);
        }
    }
}