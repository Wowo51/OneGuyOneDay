using System;
using System.Collections.Generic;

namespace RadialBasisNetwork
{
    public class RadialBasisNetwork
    {
        public List<double[]> Centers { get; private set; }
        public List<double> Weights { get; private set; }
        public double Beta { get; private set; }

        public RadialBasisNetwork(List<double[]> centers, List<double> weights, double beta)
        {
            if (centers == null || weights == null || centers.Count != weights.Count)
            {
                Centers = new List<double[]>();
                Weights = new List<double>();
            }
            else
            {
                Centers = centers;
                Weights = weights;
            }

            Beta = beta;
        }

        public double ComputeOutput(double[] input)
        {
            if (input == null)
            {
                return 0.0;
            }

            double output = 0.0;
            for (int i = 0; i < Centers.Count; i++)
            {
                double[] center = Centers[i];
                if (center == null || input.Length != center.Length)
                {
                    continue;
                }

                double activation = RadialBasisFunction.Activate(input, center, Beta);
                output += Weights[i] * activation;
            }

            return output;
        }
    }
}