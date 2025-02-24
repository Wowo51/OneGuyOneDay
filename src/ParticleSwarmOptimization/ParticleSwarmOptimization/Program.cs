using System;

namespace ParticleSwarmOptimization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Func<double[], double> objectiveFunction = delegate (double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    sum += position[i] * position[i];
                }

                return sum;
            };
            int particleCount = 30;
            int dimension = 2;
            double minBound = -10.0;
            double maxBound = 10.0;
            int iterations = 100;
            double inertiaWeight = 0.729;
            double cognitiveCoefficient = 1.49445;
            double socialCoefficient = 1.49445;
            ParticleSwarmOptimizer optimizer = new ParticleSwarmOptimizer(particleCount, dimension, minBound, maxBound, iterations, inertiaWeight, cognitiveCoefficient, socialCoefficient);
            OptimizationResult result = optimizer.Optimize(objectiveFunction);
            Console.WriteLine("Best Fitness: " + result.BestFitness);
            Console.Write("Best Position: ");
            for (int i = 0; i < result.BestPosition.Length; i++)
            {
                Console.Write(result.BestPosition[i] + " ");
            }

            Console.WriteLine();
        }
    }
}