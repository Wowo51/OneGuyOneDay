using System;
using System.Collections.Generic;
using BensonAlgorithm.Models;

namespace BensonAlgorithm.Algorithms
{
    public class BensonSolver
    {
        public static OptimizationResult Solve(LinearVectorOptimizationProblem problem, int divisions)
        {
            if (problem == null)
            {
                return new OptimizationResult();
            }

            int k = problem.C.GetLength(0);
            int n = problem.C.GetLength(1);
            OptimizationResult result = new OptimizationResult();
            List<double[]> weightsList = GenerateWeightVectors(k, divisions);
            foreach (double[] weights in weightsList)
            {
                double[] aggregated = new double[n];
                for (int j = 0; j < n; j++)
                {
                    aggregated[j] = 0.0;
                    for (int i = 0; i < k; i++)
                    {
                        aggregated[j] += weights[i] * problem.C[i, j];
                    }
                }

                double[]? sol = SimplexSolver.SolveMinimization(aggregated, problem.A, problem.b);
                if (sol != null)
                {
                    double[] objectives = new double[k];
                    for (int i = 0; i < k; i++)
                    {
                        objectives[i] = 0.0;
                        for (int j = 0; j < n; j++)
                        {
                            objectives[i] += problem.C[i, j] * sol[j];
                        }
                    }

                    if (!ContainsSolution(result.ParetoSolutions, sol, 1e-6))
                    {
                        result.ParetoSolutions.Add(new ParetoSolution(sol, objectives));
                    }
                }
            }

            return result;
        }

        private static List<double[]> GenerateWeightVectors(int dimension, int divisions)
        {
            List<double[]> list = new List<double[]>();
            if (dimension == 1)
            {
                list.Add(new double[] { 1.0 });
            }
            else
            {
                double[] current = new double[dimension];
                GenerateWeightsRecursive(list, current, divisions, 0, 1.0);
            }

            return list;
        }

        private static void GenerateWeightsRecursive(List<double[]> list, double[] current, int divisions, int index, double remaining)
        {
            int dimension = current.Length;
            if (index == dimension - 1)
            {
                current[index] = remaining;
                double[] copy = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    copy[i] = current[i];
                }

                list.Add(copy);
            }
            else
            {
                for (int i = 0; i <= divisions; i++)
                {
                    double value = (double)i / divisions;
                    if (value > remaining)
                    {
                        break;
                    }

                    current[index] = value;
                    GenerateWeightsRecursive(list, current, divisions, index + 1, remaining - value);
                }
            }
        }

        private static bool ContainsSolution(List<ParetoSolution> solutions, double[] candidate, double tol)
        {
            foreach (ParetoSolution sol in solutions)
            {
                if (VectorsEqual(sol.DecisionVector, candidate, tol))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool VectorsEqual(double[] a, double[] b, double tol)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (Math.Abs(a[i] - b[i]) > tol)
                {
                    return false;
                }
            }

            return true;
        }
    }
}