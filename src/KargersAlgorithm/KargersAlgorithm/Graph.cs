using System;
using System.Collections.Generic;

namespace KargersAlgorithm
{
    public class Graph
    {
        public List<int> Vertices { get; set; }
        public List<Edge> Edges { get; set; }

        public Graph(List<int> vertices, List<Edge> edges)
        {
            this.Vertices = vertices;
            this.Edges = edges;
        }

        public Graph Clone()
        {
            List<int> verticesClone = new List<int>(this.Vertices);
            List<Edge> edgesClone = new List<Edge>();
            foreach (Edge edge in this.Edges)
            {
                edgesClone.Add(new Edge(edge.Vertex1, edge.Vertex2));
            }

            return new Graph(verticesClone, edgesClone);
        }
    }
}