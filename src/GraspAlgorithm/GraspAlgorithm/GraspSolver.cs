using System;

namespace GraspAlgorithm
{
    public class GraspSolver
    {
        private int _maxIterations;
        private int _target;
        private int _min;
        private int _max;
        private Random _random;
        public GraspSolver(int maxIterations, int target, int min, int max)
        {
            _maxIterations = maxIterations;
            _target = target;
            _min = min;
            _max = max;
            _random = new Random();
        }

        public int Solve()
        {
            int bestSolution = 0;
            double bestScore = Double.NegativeInfinity;
            for (int iteration = 0; iteration < _maxIterations; iteration++)
            {
                int candidate = ConstructSolution();
                int improved = LocalSearch(candidate);
                double score = EvaluateSolution(improved);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestSolution = improved;
                }
            }

            return bestSolution;
        }

        private int ConstructSolution()
        {
            return _random.Next(_min, _max + 1);
        }

        private int LocalSearch(int solution)
        {
            bool improvement = true;
            int current = solution;
            while (improvement)
            {
                improvement = false;
                int bestNeighbor = current;
                double bestNeighborScore = EvaluateSolution(current);
                for (int neighbor = current - 1; neighbor <= current + 1; neighbor++)
                {
                    if (neighbor < _min || neighbor > _max || neighbor == current)
                    {
                        continue;
                    }

                    double score = EvaluateSolution(neighbor);
                    if (score > bestNeighborScore)
                    {
                        bestNeighborScore = score;
                        bestNeighbor = neighbor;
                        improvement = true;
                    }
                }

                current = bestNeighbor;
            }

            return current;
        }

        private double EvaluateSolution(int solution)
        {
            return -Math.Abs(solution - _target);
        }
    }
}