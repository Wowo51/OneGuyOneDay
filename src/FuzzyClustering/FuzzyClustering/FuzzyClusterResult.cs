namespace FuzzyClustering
{
    public class FuzzyClusterResult
    {
        public double[][] Centers { get; set; }
        public double[][] Membership { get; set; }

        public FuzzyClusterResult(double[][] centers, double[][] membership)
        {
            if (centers == null)
            {
                centers = new double[0][];
            }

            if (membership == null)
            {
                membership = new double[0][];
            }

            this.Centers = centers;
            this.Membership = membership;
        }
    }
}