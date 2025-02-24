using System;

namespace LevenbergMarquardtAlgorithm
{
    public class LevenbergMarquardtSolver
    {
        public double[] Solve(double[] initialParameters, Func<double[], double[]> residualFunction, Func<double[], double[, ]> jacobianFunction, double lambda = 0.001, int maxIterations = 100, double tolerance = 1e-6)
        {
            int parameterCount = initialParameters.Length;
            double[] parameters = new double[parameterCount];
            for (int i = 0; i < parameterCount; i++)
            {
                parameters[i] = initialParameters[i];
            }

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double[] residuals = residualFunction(parameters);
                double cost = 0.0;
                for (int i = 0; i < residuals.Length; i++)
                {
                    cost += residuals[i] * residuals[i];
                }

                double[, ] jacobian = jacobianFunction(parameters);
                int m = residuals.Length;
                int n = parameterCount;
                double[, ] JTJ = new double[n, n];
                double[] gradient = new double[n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        double sum = 0.0;
                        for (int k = 0; k < m; k++)
                        {
                            sum += jacobian[k, i] * jacobian[k, j];
                        }

                        JTJ[i, j] = sum;
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < m; k++)
                    {
                        sum += jacobian[k, i] * residuals[k];
                    }

                    gradient[i] = sum;
                }

                double normGradient = 0.0;
                for (int i = 0; i < n; i++)
                {
                    normGradient += gradient[i] * gradient[i];
                }

                normGradient = Math.Sqrt(normGradient);
                if (normGradient < tolerance)
                {
                    break;
                }

                double[, ] A = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] = JTJ[i, j];
                    }

                    A[i, i] += lambda;
                }

                double[] negGradient = new double[n];
                for (int i = 0; i < n; i++)
                {
                    negGradient[i] = -gradient[i];
                }

                double[]? delta = LinearAlgebra.SolveLinearSystem(A, negGradient);
                if (delta == null)
                {
                    break;
                }

                double[] newParameters = new double[n];
                for (int i = 0; i < n; i++)
                {
                    newParameters[i] = parameters[i] + delta[i];
                }

                double[] newResiduals = residualFunction(newParameters);
                double newCost = 0.0;
                for (int i = 0; i < newResiduals.Length; i++)
                {
                    newCost += newResiduals[i] * newResiduals[i];
                }

                if (newCost < cost)
                {
                    parameters = newParameters;
                    lambda = lambda / 10.0;
                }
                else
                {
                    lambda = lambda * 10.0;
                }
            }

            return parameters;
        }
    }
}