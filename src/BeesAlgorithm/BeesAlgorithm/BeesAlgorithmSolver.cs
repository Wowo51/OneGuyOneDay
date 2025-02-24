using System;
using System.Collections.Generic;

namespace BeesAlgorithm
{
    public class BeesAlgorithmSolver
    {
        private Random _random = new Random();
        public delegate double ObjectiveFunction(double[] position);
        public BeeSolution? FindOptimal(ObjectiveFunction objective, double[] lowerBounds, double[] upperBounds, int numScoutBees, int numEliteSites, int numEliteBees, int numSelectedSites, double neighborhoodSize, int maxIterations)
        {
            if (objective == null || lowerBounds == null || upperBounds == null || lowerBounds.Length != upperBounds.Length)
            {
                return null;
            }

            int dimension = lowerBounds.Length;
            double bestFitness = double.MinValue;
            BeeSolution? bestSolution = null;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                List<BeeSolution> scouts = new List<BeeSolution>();
                for (int i = 0; i < numScoutBees; i++)
                {
                    double[] position = GenerateRandomPosition(lowerBounds, upperBounds);
                    double fitness = objective(position);
                    BeeSolution solution = new BeeSolution(position, fitness);
                    scouts.Add(solution);
                    if (fitness > bestFitness)
                    {
                        bestFitness = fitness;
                        bestSolution = new BeeSolution((double[])position.Clone(), fitness);
                    }
                }

                scouts.Sort(delegate (BeeSolution left, BeeSolution right)
                {
                    return right.Fitness.CompareTo(left.Fitness);
                });
                List<BeeSolution> neighborhoodSolutions = new List<BeeSolution>();
                for (int i = 0; i < numEliteSites && i < scouts.Count; i++)
                {
                    BeeSolution site = scouts[i];
                    for (int j = 0; j < numEliteBees; j++)
                    {
                        double[] newPosition = LocalSearch(site.Position, neighborhoodSize, lowerBounds, upperBounds);
                        double newFitness = objective(newPosition);
                        neighborhoodSolutions.Add(new BeeSolution(newPosition, newFitness));
                        if (newFitness > bestFitness)
                        {
                            bestFitness = newFitness;
                            bestSolution = new BeeSolution((double[])newPosition.Clone(), newFitness);
                        }
                    }
                }

                for (int i = numEliteSites; i < numSelectedSites && i < scouts.Count; i++)
                {
                    BeeSolution site = scouts[i];
                    double[] newPosition = LocalSearch(site.Position, neighborhoodSize, lowerBounds, upperBounds);
                    double newFitness = objective(newPosition);
                    neighborhoodSolutions.Add(new BeeSolution(newPosition, newFitness));
                    if (newFitness > bestFitness)
                    {
                        bestFitness = newFitness;
                        bestSolution = new BeeSolution((double[])newPosition.Clone(), newFitness);
                    }
                }

                neighborhoodSize *= 0.95;
            }

            return bestSolution;
        }

        private double[] GenerateRandomPosition(double[] lowerBounds, double[] upperBounds)
        {
            int dimension = lowerBounds.Length;
            double[] position = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                double range = upperBounds[i] - lowerBounds[i];
                double randomValue = _random.NextDouble();
                position[i] = lowerBounds[i] + randomValue * range;
            }

            return position;
        }

        private double[] LocalSearch(double[] currentPosition, double neighborhoodSize, double[] lowerBounds, double[] upperBounds)
        {
            int dimension = currentPosition.Length;
            double[] newPosition = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                double perturbation = (2 * _random.NextDouble() - 1) * neighborhoodSize;
                newPosition[i] = currentPosition[i] + perturbation;
                if (newPosition[i] < lowerBounds[i])
                {
                    newPosition[i] = lowerBounds[i];
                }

                if (newPosition[i] > upperBounds[i])
                {
                    newPosition[i] = upperBounds[i];
                }
            }

            return newPosition;
        }
    }
}