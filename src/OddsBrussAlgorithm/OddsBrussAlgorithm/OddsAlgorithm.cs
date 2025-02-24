using System;
using System.Collections.Generic;

namespace OddsBrussAlgorithm
{
    public sealed class OddsAlgorithm
    {
        private readonly double[] _probabilities;
        private readonly int _totalEvents;
        private readonly int _thresholdIndex;
        private int _currentIndex;
        public OddsAlgorithm(double[] probabilities)
        {
            if (probabilities == null)
            {
                probabilities = new double[0];
            }

            _probabilities = probabilities;
            _totalEvents = probabilities.Length;
            if (_totalEvents == 0)
            {
                _thresholdIndex = 0;
                _currentIndex = 0;
                return;
            }

            double totalOdds = 0.0;
            for (int i = 0; i < _totalEvents; i++)
            {
                double p = probabilities[i];
                if (p < 0.0)
                {
                    p = 0.0;
                }

                if (p > 1.0)
                {
                    p = 1.0;
                }

                double odds = (p < 1.0) ? p / (1.0 - p) : 1e10;
                totalOdds += odds;
            }

            if (totalOdds < 1.0)
            {
                _thresholdIndex = _totalEvents - 1;
            }
            else
            {
                double sumOdds = 0.0;
                int candidateThreshold = _totalEvents - 1;
                for (int i = _totalEvents - 1; i >= 0; i--)
                {
                    double p = probabilities[i];
                    double odds = (p < 1.0) ? p / (1.0 - p) : 1e10;
                    sumOdds += odds;
                    candidateThreshold = i;
                    if (sumOdds >= 1.0)
                    {
                        break;
                    }
                }

                _thresholdIndex = candidateThreshold;
            }

            _currentIndex = 0;
        }

        // Online decision: determines whether the current candidate should be selected.
        public bool ShouldSelectCurrent(bool isDistinguished)
        {
            if (_currentIndex >= _totalEvents)
            {
                return false;
            }

            // Always select the last candidate.
            if (_currentIndex == _totalEvents - 1)
            {
                _currentIndex++;
                return true;
            }

            if (_currentIndex < _thresholdIndex)
            {
                _currentIndex++;
                return false;
            }
            else
            {
                _currentIndex++;
                return isDistinguished;
            }
        }

        // Processes a complete sequence of outcomes and returns the index of the chosen candidate.
        public int Run(IList<bool> outcomes)
        {
            if (outcomes == null)
            {
                return -1;
            }

            int index = 0;
            while (index < outcomes.Count && _currentIndex < _totalEvents)
            {
                if (ShouldSelectCurrent(outcomes[index]))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        // Resets the internal state for a new run.
        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}