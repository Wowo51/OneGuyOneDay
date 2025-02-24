namespace NestedSamplingAlgorithm
{
    public class SamplePoint
    {
        public double[] Parameters { get; set; }
        public double Likelihood { get; set; }

        public SamplePoint(double[] parameters, double likelihood)
        {
            this.Parameters = parameters;
            this.Likelihood = likelihood;
        }
    }
}