using System;
using System.Collections.Generic;

namespace MetropolisHastingsAlgorithm
{
    public class MetropolisHastingsSampler
    {
        private readonly Random _random;
        public MetropolisHastingsSampler()
        {
            _random = new Random();
        }

        // Generates a sample from a normal distribution using the Box-Muller transform.
        private double GaussianSample(double mean, double stdDev)
        {
            double u1 = _random.NextDouble();
            double u2 = _random.NextDouble();
            double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
            return z0 * stdDev + mean;
        }

        // Runs the Metropolis-Hastings algorithm to sample from a target density.
        // targetDensity: target probability density function (up to a constant).
        // initialState: starting point of the Markov chain.
        // iterations: number of iterations to run.
        // proposalStd: standard deviation of the Gaussian proposal distribution.
        public IEnumerable<double> Sample(double initialState, int iterations, Func<double, double> targetDensity, double proposalStd)
        {
            double currentState = initialState;
            double currentDensity = targetDensity(currentState);
            List<double> samples = new List<double>();
            samples.Add(currentState);
            for (int i = 0; i < iterations; i++)
            {
                double proposedState = GaussianSample(currentState, proposalStd);
                double proposedDensity = targetDensity(proposedState);
                double acceptanceProbability = 1.0;
                if (currentDensity > 0)
                {
                    acceptanceProbability = Math.Min(1.0, proposedDensity / currentDensity);
                }

                double u = _random.NextDouble();
                if (u < acceptanceProbability)
                {
                    currentState = proposedState;
                    currentDensity = proposedDensity;
                }

                samples.Add(currentState);
            }

            return samples;
        }
    }
}