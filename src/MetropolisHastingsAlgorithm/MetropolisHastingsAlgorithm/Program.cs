using System;
using System.Collections.Generic;

namespace MetropolisHastingsAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MetropolisHastingsSampler sampler = new MetropolisHastingsSampler();
            // Define the target density function: standard normal density (up to a constant factor)
            Func<double, double> targetDensity = delegate (double x)
            {
                return Math.Exp(-0.5 * x * x);
            };
            // Run the sampler starting from 0.0 for 10000 iterations with a proposal standard deviation of 1.0.
            IEnumerable<double> samples = sampler.Sample(0.0, 10000, targetDensity, 1.0);
            List<double> sampleList = new List<double>(samples);
            // Output the first 10 samples.
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(sampleList[i]);
            }
        }
    }
}