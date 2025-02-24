using System;

namespace KargersAlgorithm
{
    public class Edge
    {
        public int Vertex1 { get; set; }
        public int Vertex2 { get; set; }

        public Edge(int vertex1, int vertex2)
        {
            this.Vertex1 = vertex1;
            this.Vertex2 = vertex2;
        }
    }
}