using System;
using System.Collections.Generic;

namespace CrossEntropyMethod
{
    public class CrossEntropyOptimizer
    {
        private readonly Random _random;
        public CrossEntropyOptimizer()
        {
            _random = new Random();
        }

        public double[] Optimize(Func<double[], double> objective, double[] initialMean, double[] initialStd, int sampleSize, double eliteFraction, int maxIterations)
        {
            if (objective == null || initialMean == null || initialStd == null || sampleSize <= 0 || eliteFraction <= 0 || eliteFraction > 1 || maxIterations <= 0 || initialMean.Length != initialStd.Length)
            {
                return (initialMean != null) ? initialMean : new double[0];
            }

            int dimension = initialMean.Length;
            double[] bestSolution = new double[dimension];
            double bestScore = double.MaxValue;
            double[] currentMean = new double[dimension];
            double[] currentStd = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                currentMean[i] = initialMean[i];
                currentStd[i] = initialStd[i];
            }

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                List<Candidate> candidates = new List<Candidate>();
                for (int i = 0; i < sampleSize; i++)
                {
                    double[] sample = new double[dimension];
                    for (int j = 0; j < dimension; j++)
                    {
                        sample[j] = GenerateNormalSample(_random, currentMean[j], currentStd[j]);
                    }

                    double score = objective(sample);
                    Candidate candidate = new Candidate
                    {
                        Solution = sample,
                        Score = score
                    };
                    candidates.Add(candidate);
                }

                candidates.Sort((Candidate a, Candidate b) => a.Score.CompareTo(b.Score));
                if (candidates[0].Score < bestScore)
                {
                    bestScore = candidates[0].Score;
                    bestSolution = new double[dimension];
                    for (int j = 0; j < dimension; j++)
                    {
                        bestSolution[j] = candidates[0].Solution[j];
                    }
                }

                int eliteCount = (int)Math.Max(1, Math.Round(sampleSize * eliteFraction));
                double[] newMean = new double[dimension];
                double[] newStd = new double[dimension];
                for (int j = 0; j < dimension; j++)
                {
                    double sum = 0.0;
                    for (int i = 0; i < eliteCount; i++)
                    {
                        sum += candidates[i].Solution[j];
                    }

                    newMean[j] = sum / eliteCount;
                }

                for (int j = 0; j < dimension; j++)
                {
                    double sumSq = 0.0;
                    for (int i = 0; i < eliteCount; i++)
                    {
                        double diff = candidates[i].Solution[j] - newMean[j];
                        sumSq += diff * diff;
                    }

                    double std = Math.Sqrt(sumSq / eliteCount);
                    if (std < 1e-6)
                    {
                        std = 1e-6;
                    }

                    newStd[j] = std;
                }

                for (int j = 0; j < dimension; j++)
                {
                    currentMean[j] = newMean[j];
                    currentStd[j] = newStd[j];
                }
            }

            return bestSolution;
        }

        private static double GenerateNormalSample(Random random, double mean, double std)
        {
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
            return mean + z0 * std;
        }

        private class Candidate
        {
            public double[] Solution { get; set; } = new double[0];
            public double Score { get; set; }
        }
    }
}