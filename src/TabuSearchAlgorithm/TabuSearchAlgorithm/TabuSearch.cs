using System;
using System.Collections.Generic;

namespace TabuSearchAlgorithm
{
    public class TabuSearch
    {
        private int _maxIterations;
        private int _tabuListSize;
        private List<int> _tabuList;
        public TabuSearch(int maxIterations, int tabuListSize)
        {
            _maxIterations = maxIterations;
            _tabuListSize = tabuListSize;
            _tabuList = new List<int>();
        }

        public int Execute(int initialSolution)
        {
            int bestSolution = initialSolution;
            int currentSolution = initialSolution;
            for (int iteration = 0; iteration < _maxIterations; iteration++)
            {
                int candidate = GetCandidate(currentSolution);
                if (!IsTabu(candidate))
                {
                    currentSolution = candidate;
                    if (Evaluate(candidate) < Evaluate(bestSolution))
                    {
                        bestSolution = candidate;
                    }

                    UpdateTabuList(candidate);
                }
            }

            return bestSolution;
        }

        private int GetCandidate(int solution)
        {
            return solution + 1;
        }

        private bool IsTabu(int candidate)
        {
            return _tabuList.Contains(candidate);
        }

        private void UpdateTabuList(int candidate)
        {
            _tabuList.Add(candidate);
            if (_tabuList.Count > _tabuListSize)
            {
                _tabuList.RemoveAt(0);
            }
        }

        private int Evaluate(int solution)
        {
            return solution * solution;
        }
    }
}