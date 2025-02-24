using System;

namespace NewtonMethod
{
    public class NewtonOptimizer
    {
        public double[] Optimize(Func<double[], double> f, Func<double[], double[]> grad, Func<double[], double[, ]> hess, double[] initial, double tolerance, int maxIterations)
        {
            double[] current = initial;
            for (int i = 0; i < maxIterations; i++)
            {
                double[] gradient = grad(current);
                if (VectorUtil.Norm(gradient) < tolerance)
                {
                    return current;
                }

                double[, ] hessianMatrix = hess(current);
                double[]? step = MatrixUtil.Solve(hessianMatrix, gradient);
                if (step == null)
                {
                    return current;
                }

                double[]? newCurrent = VectorUtil.Subtract(current, step);
                if (newCurrent == null)
                {
                    return current;
                }

                current = newCurrent;
            }

            return current;
        }
    }
}