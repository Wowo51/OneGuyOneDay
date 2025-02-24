using System;

namespace EllipsoidMethod
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int dimension = 2;
            double[] initialCenter = new double[2]
            {
                0.0,
                0.0
            };
            double[, ] initialShape = new double[2, 2]
            {
                {
                    4.0,
                    0.0
                },
                {
                    0.0,
                    4.0
                }
            };
            EllipsoidSolver solver = new EllipsoidSolver(dimension, initialCenter, initialShape);
            double[] solution = solver.Solve(EllipsoidMethodOracle);
            Console.WriteLine("Feasible solution found: [{0}, {1}]", solution[0], solution[1]);
        }

        // Separation oracle for the convex feasibility problem:
        // Constraint 1: x[0]^2 + x[1]^2 <= 4  (a circle of radius 2)
        // Constraint 2: x[0] + x[1] >= 1, rewritten as 1 - (x[0] + x[1]) <= 0
        public static double[]? EllipsoidMethodOracle(double[] point)
        {
            if (point.Length != 2)
            {
                return new double[0];
            }

            double sumSquares = point[0] * point[0] + point[1] * point[1];
            if (sumSquares > 4.0)
            {
                // Subgradient of f(x)= x0^2+x1^2 (for violation of x0^2+x1^2<=4)
                double[] subgradient = new double[2]
                {
                    2.0 * point[0],
                    2.0 * point[1]
                };
                return subgradient;
            }

            double sum = point[0] + point[1];
            if (sum < 1.0)
            {
                // Rewrite constraint as 1 - (x0 + x1) <= 0; subgradient is (-1, -1)
                double[] subgradient = new double[2]
                {
                    -1.0,
                    -1.0
                };
                return subgradient;
            }

            return null;
        }
    }
}