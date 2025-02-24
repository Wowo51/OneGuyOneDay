using System;
using System.Collections.Generic;

namespace RandomHillClimb
{
    public class RandomRestartHillClimber<T>
    {
        public T FindOptimalSolution(Func<T> randomStateGenerator, Func<T, List<T>> neighborGenerator, Func<T, double> objective, int maxRestarts, int maxIterations)
        {
            if (maxRestarts <= 0)
            {
                T initial = randomStateGenerator();
                return initial;
            }

            T bestSolution = default(T)!;
            double bestScore = 0.0;
            bool firstIteration = true;
            for (int i = 0; i < maxRestarts; i++)
            {
                T current = randomStateGenerator();
                int iteration = 0;
                while (iteration < maxIterations)
                {
                    List<T> neighbors = neighborGenerator(current);
                    if (neighbors == null)
                    {
                        neighbors = new List<T>();
                    }

                    T bestNeighbor = current;
                    double bestNeighborScore = objective(current);
                    bool improvementFound = false;
                    foreach (T neighbor in neighbors)
                    {
                        double score = objective(neighbor);
                        if (score > bestNeighborScore)
                        {
                            bestNeighbor = neighbor;
                            bestNeighborScore = score;
                            improvementFound = true;
                        }
                    }

                    if (!improvementFound)
                    {
                        break;
                    }

                    current = bestNeighbor;
                    iteration++;
                }

                double currentScore = objective(current);
                if (firstIteration)
                {
                    bestSolution = current;
                    bestScore = currentScore;
                    firstIteration = false;
                }
                else if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    bestSolution = current;
                }
            }

            return bestSolution;
        }
    }
}