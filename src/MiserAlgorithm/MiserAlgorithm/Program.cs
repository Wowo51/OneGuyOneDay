using System;

namespace MiserAlgorithm
{
    internal static class Program
    {
        private static void Main()
        {
            MiserIntegrator integrator = new MiserIntegrator();
            // Integrate f(x) = x^2 over [0,1]
            double[] lower = new double[]
            {
                0.0
            };
            double[] upper = new double[]
            {
                1.0
            };
            Func<double[], double> f = delegate (double[] x)
            {
                return x[0] * x[0];
            };
            long samples = 100000;
            double result = integrator.Integrate(f, lower, upper, samples);
            Console.WriteLine("Integration result: " + result);
        }
    }
}