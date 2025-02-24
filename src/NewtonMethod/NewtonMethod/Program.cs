using System;

namespace NewtonMethod
{
    public class Program
    {
        public static void Main()
        {
            NewtonOptimizer optimizer = new NewtonOptimizer();
            Func<double[], double> f = delegate (double[] x)
            {
                return (x[0] - 3) * (x[0] - 3);
            };
            Func<double[], double[]> grad = delegate (double[] x)
            {
                return new double[]
                {
                    2 * (x[0] - 3)
                };
            };
            Func<double[], double[, ]> hess = delegate (double[] x)
            {
                double[, ] hessian = new double[1, 1];
                hessian[0, 0] = 2;
                return hessian;
            };
            double[] initial = new double[]
            {
                0
            };
            double tolerance = 1e-6;
            int maxIterations = 100;
            double[] optimum = optimizer.Optimize(f, grad, hess, initial, tolerance, maxIterations);
            Console.WriteLine("Optimum at x = " + optimum[0]);
        }
    }
}