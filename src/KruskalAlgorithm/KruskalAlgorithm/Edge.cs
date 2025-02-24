using System;

namespace KruskalAlgorithm
{
    public class Edge : IComparable<Edge>
    {
        public int Source { get; }
        public int Destination { get; }
        public int Weight { get; }

        public Edge(int source, int destination, int weight)
        {
            this.Source = source;
            this.Destination = destination;
            this.Weight = weight;
        }

        public int CompareTo(Edge? other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            return $"({Source} -- {Weight} --> {Destination})";
        }
    }
}