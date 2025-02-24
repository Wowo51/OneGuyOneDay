using System.Collections.Generic;

namespace BellmanFordAlgorithm
{
    public class Graph
    {
        public int VertexCount { get; }
        public List<Edge> Edges { get; }

        public Graph(int vertexCount)
        {
            VertexCount = vertexCount;
            Edges = new List<Edge>();
        }

        public void AddEdge(int source, int target, int weight)
        {
            Edge edge = new Edge(source, target, weight);
            Edges.Add(edge);
        }
    }

    public class Edge
    {
        public int Source { get; }
        public int Target { get; }
        public int Weight { get; }

        public Edge(int source, int target, int weight)
        {
            Source = source;
            Target = target;
            Weight = weight;
        }
    }
}