using System;
using System.Collections.Generic;

namespace LocalSearch
{
    public static class LocalSearchAlgorithm
    {
        public static T Search<T>(T initialSolution, Func<T, double> objective, Func<T, IEnumerable<T>> getNeighbors, Func<double, double, bool>? isBetter = null, int maxIterations = 1000)
        {
            if (objective == null || getNeighbors == null)
            {
                return initialSolution;
            }

            if (isBetter == null)
            {
                isBetter = (double current, double candidate) => candidate < current;
            }

            T currentSolution = initialSolution;
            double currentValue = objective(currentSolution);
            int iteration = 0;
            while (iteration < maxIterations)
            {
                IEnumerable<T> neighborCollection = getNeighbors(currentSolution);
                if (neighborCollection == null)
                {
                    neighborCollection = System.Array.Empty<T>();
                }

                T bestNeighbor = currentSolution;
                double bestValue = currentValue;
                bool improvementFound = false;
                foreach (T neighbor in neighborCollection)
                {
                    double neighborValue = objective(neighbor);
                    if (isBetter(bestValue, neighborValue))
                    {
                        bestNeighbor = neighbor;
                        bestValue = neighborValue;
                        improvementFound = true;
                    }
                }

                if (!improvementFound)
                {
                    break;
                }

                currentSolution = bestNeighbor;
                currentValue = bestValue;
                iteration++;
            }

            return currentSolution;
        }
    }
}