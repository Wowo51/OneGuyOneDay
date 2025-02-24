using System.Collections.Generic;

namespace FordFulkersonAlgorithm
{
    public class Edge
    {
        public int Source { get; }
        public int Target { get; }
        public int Capacity { get; }
        public int Flow { get; set; }
        public Edge? Reverse { get; set; }

        public Edge(int source, int target, int capacity)
        {
            Source = source;
            Target = target;
            Capacity = capacity;
            Flow = 0;
        }

        public int ResidualCapacity()
        {
            return Capacity - Flow;
        }
    }
}