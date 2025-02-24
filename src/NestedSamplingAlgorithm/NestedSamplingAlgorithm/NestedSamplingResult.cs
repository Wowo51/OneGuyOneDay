using System.Collections.Generic;

namespace NestedSamplingAlgorithm
{
    public class NestedSamplingResult
    {
        public double Evidence { get; set; }
        public double Uncertainty { get; set; }
        public List<SamplePoint> Samples { get; set; }

        public NestedSamplingResult(double evidence, double uncertainty, List<SamplePoint> samples)
        {
            this.Evidence = evidence;
            this.Uncertainty = uncertainty;
            this.Samples = samples;
        }
    }
}