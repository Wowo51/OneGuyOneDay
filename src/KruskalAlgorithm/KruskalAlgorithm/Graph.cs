using System;
using System.Collections.Generic;

namespace KruskalAlgorithm
{
    public class Graph
    {
        public int VerticesCount { get; }
        public List<Edge> Edges { get; }

        public Graph(int verticesCount)
        {
            this.VerticesCount = verticesCount;
            this.Edges = new List<Edge>();
        }

        public void AddEdge(int source, int destination, int weight)
        {
            Edge edge = new Edge(source, destination, weight);
            this.Edges.Add(edge);
        }
    }
}