using System.Collections.Generic;

namespace BoruvkasAlgorithm
{
    public class Edge
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public double Weight { get; set; }

        public Edge(int source, int destination, double weight)
        {
            this.Source = source;
            this.Destination = destination;
            this.Weight = weight;
        }
    }

    public class Graph
    {
        public int Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }

        public Graph(int vertices)
        {
            this.Vertices = vertices;
            this.Edges = new List<Edge>();
        }

        public void AddEdge(int source, int destination, double weight)
        {
            this.Edges.Add(new Edge(source, destination, weight));
        }
    }
}