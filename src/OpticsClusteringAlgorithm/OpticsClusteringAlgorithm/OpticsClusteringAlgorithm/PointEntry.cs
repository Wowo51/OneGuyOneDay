namespace OpticsClusteringAlgorithm
{
    public class PointEntry
    {
        public Point Value { get; set; }
        public bool Processed { get; set; }
        public double? CoreDistance { get; set; }
        public double? ReachabilityDistance { get; set; }

        public PointEntry(Point value)
        {
            this.Value = value;
            this.Processed = false;
            this.CoreDistance = null;
            this.ReachabilityDistance = null;
        }
    }
}