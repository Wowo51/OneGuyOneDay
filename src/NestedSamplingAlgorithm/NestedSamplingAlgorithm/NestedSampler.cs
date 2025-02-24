using System;
using System.Collections.Generic;

namespace NestedSamplingAlgorithm
{
    public class NestedSampler
    {
        private ILikelihoodEvaluator likelihoodEvaluator;
        private IPrior prior;
        private int livePoints;
        private int maxIterations;
        private double tolerance;
        public NestedSampler(ILikelihoodEvaluator likelihoodEvaluator, IPrior prior, int livePoints, int maxIterations, double tolerance)
        {
            this.likelihoodEvaluator = likelihoodEvaluator;
            this.prior = prior;
            this.livePoints = livePoints;
            this.maxIterations = maxIterations;
            this.tolerance = tolerance;
        }

        public NestedSamplingResult Run()
        {
            List<SamplePoint> liveSamples = new List<SamplePoint>();
            for (int i = 0; i < this.livePoints; i++)
            {
                double[] sample = this.prior.Sample()!;
                double likelihood = this.likelihoodEvaluator.Evaluate(sample);
                liveSamples.Add(new SamplePoint(sample, likelihood));
            }

            double evidence = 0.0;
            double deltaEvidence = 1.0;
            int iteration = 0;
            double X = 1.0;
            while (iteration < this.maxIterations && deltaEvidence > this.tolerance)
            {
                SamplePoint worstSample = liveSamples[0];
                int worstIndex = 0;
                for (int i = 1; i < liveSamples.Count; i++)
                {
                    if (liveSamples[i].Likelihood < worstSample.Likelihood)
                    {
                        worstSample = liveSamples[i];
                        worstIndex = i;
                    }
                }

                double previousX = X;
                X *= Math.Exp(-1.0 / this.livePoints);
                double weight = previousX - X;
                evidence += weight * worstSample.Likelihood;
                // Initialize newSample and newLikelihood with worstSample defaults.
                double[] newSample = worstSample.Parameters;
                double newLikelihood = worstSample.Likelihood;
                int trial = 0;
                int maxTries = 1000;
                while (trial < maxTries)
                {
                    double[] candidate = this.prior.Sample()!;
                    double candidateLikelihood = this.likelihoodEvaluator.Evaluate(candidate);
                    if (candidateLikelihood >= worstSample.Likelihood)
                    {
                        newSample = candidate;
                        newLikelihood = candidateLikelihood;
                        break;
                    }

                    trial++;
                }

                liveSamples[worstIndex] = new SamplePoint(newSample, newLikelihood);
                double remainingEvidence = X * ComputeAverageLikelihood(liveSamples);
                deltaEvidence = remainingEvidence;
                iteration++;
            }

            double uncertainty = deltaEvidence;
            NestedSamplingResult result = new NestedSamplingResult(evidence, uncertainty, liveSamples);
            return result;
        }

        private double ComputeAverageLikelihood(List<SamplePoint> samples)
        {
            double total = 0.0;
            for (int i = 0; i < samples.Count; i++)
            {
                total += samples[i].Likelihood;
            }

            return total / samples.Count;
        }
    }
}