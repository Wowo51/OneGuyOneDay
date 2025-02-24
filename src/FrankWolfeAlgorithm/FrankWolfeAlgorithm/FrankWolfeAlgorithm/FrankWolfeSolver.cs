using System;

namespace FrankWolfeAlgorithm
{
    public delegate double ObjectiveFunction(Vector x);
    public delegate Vector GradientFunction(Vector x);
    public delegate Vector LinearMinimizationOracle(Vector gradient);
    public class FrankWolfeSolver
    {
        public static Vector Solve(Vector x0, ObjectiveFunction objective, GradientFunction gradient, LinearMinimizationOracle oracle, double tolerance, int maxIterations)
        {
            if (x0 == null)
            {
                return new Vector(new double[0]);
            }

            Vector current = x0;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                Vector grad = (gradient != null) ? gradient(current) : new Vector(new double[current.Dimension]);
                Vector s = (oracle != null) ? oracle(grad) : new Vector(new double[current.Dimension]);
                Vector direction = s - current;
                if (direction.Norm() < tolerance)
                {
                    break;
                }

                double gamma = 2.0 / (iteration + 2);
                current = current + gamma * direction;
            }

            return current;
        }
    }
}