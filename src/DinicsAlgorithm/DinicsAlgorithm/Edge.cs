namespace DinicsAlgorithm
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Capacity { get; set; }
        public int Flow { get; set; }
        public Edge Reverse { get; set; } = null !;

        public Edge(int from, int to, int capacity)
        {
            this.From = from;
            this.To = to;
            this.Capacity = capacity;
            this.Flow = 0;
        }

        public int ResidualCapacity()
        {
            return Capacity - Flow;
        }
    }
}