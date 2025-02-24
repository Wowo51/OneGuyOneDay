using System;

namespace MiserAlgorithm
{
    public class MiserIntegrator
    {
        private const long MIN_SAMPLES = 100;
        private const long ESTIMATE_SAMPLES = 50;
        private readonly Random random;
        public MiserIntegrator()
        {
            random = new Random();
        }

        public double Integrate(Func<double[], double> f, double[] lower, double[] upper, long numSamples)
        {
            if (lower == null || upper == null || lower.Length != upper.Length || lower.Length == 0)
            {
                return BasicMonteCarloIntegration(f, lower, upper, numSamples);
            }

            if (numSamples <= MIN_SAMPLES)
            {
                return BasicMonteCarloIntegration(f, lower, upper, numSamples);
            }

            return MiserRecursive(f, lower, upper, numSamples);
        }

        private double MiserRecursive(Func<double[], double> f, double[] lower, double[] upper, long numSamples)
        {
            if (lower == null || upper == null || lower.Length != upper.Length || lower.Length == 0)
            {
                return BasicMonteCarloIntegration(f, lower, upper, numSamples);
            }

            if (numSamples <= MIN_SAMPLES)
            {
                return BasicMonteCarloIntegration(f, lower, upper, numSamples);
            }

            int dimensions = lower.Length;
            double bestScore = double.PositiveInfinity;
            double bestError1 = 0.0;
            double bestError2 = 0.0;
            double[] bestUpper1 = (double[])upper.Clone();
            double[] bestLower2 = (double[])lower.Clone();
            for (int i = 0; i < dimensions; i++)
            {
                double mid = (lower[i] + upper[i]) / 2.0;
                double[] upper1 = (double[])upper.Clone();
                upper1[i] = mid;
                double[] lower2 = (double[])lower.Clone();
                lower2[i] = mid;
                double error1 = EstimateError(f, lower, upper1, ESTIMATE_SAMPLES);
                double error2 = EstimateError(f, lower2, upper, ESTIMATE_SAMPLES);
                double score = error1 + error2;
                if (score < bestScore)
                {
                    bestScore = score;
                    bestError1 = error1;
                    bestError2 = error2;
                    bestUpper1 = upper1;
                    bestLower2 = lower2;
                }
            }

            double totalError = bestError1 + bestError2;
            long samples1 = (totalError > 0.0) ? (long)(numSamples * bestError1 / totalError) : numSamples / 2;
            if (samples1 < 1)
            {
                samples1 = 1;
            }

            long samples2 = numSamples - samples1;
            if (samples2 < 1)
            {
                samples2 = 1;
            }

            double result1 = MiserRecursive(f, lower, bestUpper1, samples1);
            double result2 = MiserRecursive(f, bestLower2, upper, samples2);
            return result1 + result2;
        }

        private double BasicMonteCarloIntegration(Func<double[], double> f, double[] lower, double[] upper, long samples)
        {
            if (lower == null || upper == null || lower.Length == 0 || lower.Length != upper.Length)
            {
                return 0.0;
            }

            double volume = ComputeVolume(lower, upper);
            double sum = 0.0;
            int dimensions = lower.Length;
            double[] point = new double[dimensions];
            for (long i = 0; i < samples; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    double range = upper[j] - lower[j];
                    point[j] = lower[j] + range * random.NextDouble();
                }

                sum += f(point);
            }

            return volume * sum / samples;
        }

        private double EstimateError(Func<double[], double> f, double[] lower, double[] upper, long samples)
        {
            if (lower == null || upper == null || lower.Length == 0 || lower.Length != upper.Length)
            {
                return 0.0;
            }

            double volume = ComputeVolume(lower, upper);
            double mean = 0.0;
            double m2 = 0.0;
            int dimensions = lower.Length;
            for (long i = 1; i <= samples; i++)
            {
                double[] point = new double[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    double range = upper[j] - lower[j];
                    point[j] = lower[j] + range * random.NextDouble();
                }

                double value = f(point);
                double delta = value - mean;
                mean += delta / i;
                m2 += delta * (value - mean);
            }

            double variance = (samples > 1) ? m2 / (samples - 1) : 0.0;
            double error = volume * Math.Sqrt(variance / samples);
            return error;
        }

        private double ComputeVolume(double[] lower, double[] upper)
        {
            if (lower == null || upper == null || lower.Length == 0 || lower.Length != upper.Length)
            {
                return 0.0;
            }

            double volume = 1.0;
            int dimensions = lower.Length;
            for (int i = 0; i < dimensions; i++)
            {
                volume *= (upper[i] - lower[i]);
            }

            return volume;
        }
    }
}