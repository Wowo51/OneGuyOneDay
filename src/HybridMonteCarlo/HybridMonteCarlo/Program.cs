using System;

namespace HybridMonteCarlo
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            double[] initialState = new double[]
            {
                1.0,
                1.0
            };
            IPotentialEnergy potential = new QuadraticPotential();
            HybridMonteCarloSampler sampler = new HybridMonteCarloSampler(potential, 0.1, 10, random);
            double[] newState = sampler.Sample(initialState);
            Console.WriteLine("New state: " + string.Join(", ", newState));
        }
    }
}