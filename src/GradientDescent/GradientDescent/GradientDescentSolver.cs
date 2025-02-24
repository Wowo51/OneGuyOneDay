using System;

namespace GradientDescent
{
    public class GradientDescentSolver
    {
        public double LearningRate { get; set; }
        public int MaxIterations { get; set; }
        public double Tolerance { get; set; }

        public GradientDescentSolver(double learningRate, int maxIterations, double tolerance)
        {
            LearningRate = learningRate;
            MaxIterations = maxIterations;
            Tolerance = tolerance;
        }

        public double[] Solve(Func<double[], double> cost, Func<double[], double[]> gradient, double[] initialGuess)
        {
            if (initialGuess == null)
            {
                return new double[0];
            }

            int dimension = initialGuess.Length;
            double[] x = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                x[i] = initialGuess[i];
            }

            if (cost == null || gradient == null)
            {
                return x;
            }

            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                double[] grad = gradient(x);
                double gradNorm = Norm(grad);
                if (gradNorm < Tolerance)
                {
                    break;
                }

                for (int i = 0; i < dimension; i++)
                {
                    x[i] = x[i] - LearningRate * grad[i];
                }
            }

            return x;
        }

        private double Norm(double[] vector)
        {
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i] * vector[i];
            }

            return Math.Sqrt(sum);
        }
    }
}