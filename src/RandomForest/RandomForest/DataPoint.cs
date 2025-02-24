namespace RandomForest
{
    public class DataPoint
    {
        public double[] Features { get; }
        public int Label { get; }

        public DataPoint(double[] features, int label)
        {
            Features = features;
            Label = label;
        }
    }
}