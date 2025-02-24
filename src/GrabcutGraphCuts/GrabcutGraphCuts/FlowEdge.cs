namespace GrabcutGraphCuts
{
    public class FlowEdge
    {
        public int From { get; }
        public int To { get; }
        public double Capacity { get; }
        public double Flow { get; private set; }
        public FlowEdge Reverse { get; set; } = default !;

        public FlowEdge(int from, int to, double capacity)
        {
            this.From = from;
            this.To = to;
            this.Capacity = capacity;
            this.Flow = 0.0;
        }

        public double ResidualCapacityTo(int vertex)
        {
            if (vertex == this.To)
            {
                return this.Capacity - this.Flow;
            }
            else if (vertex == this.From)
            {
                return this.Flow;
            }

            return 0;
        }

        public void AddResidualFlowTo(int vertex, double delta)
        {
            if (vertex == this.To)
            {
                this.Flow += delta;
                this.Reverse.Flow -= delta;
            }
            else if (vertex == this.From)
            {
                this.Flow -= delta;
                this.Reverse.Flow += delta;
            }
        }
    }
}