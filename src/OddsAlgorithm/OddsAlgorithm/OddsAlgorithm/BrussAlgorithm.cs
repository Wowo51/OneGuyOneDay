using System;
using System.Collections.Generic;

namespace OddsAlgorithm
{
    public class BrussAlgorithm
    {
        // Computes the threshold index using the Bruss odds algorithm.
        // For a list of event probabilities, returns the smallest index (0-based)
        // such that the sum of odds from that index to the end is at least 1.
        // If the input list is null or empty, returns -1.
        public int ComputeThresholdIndex(IList<double> probabilities)
        {
            if (probabilities == null || probabilities.Count == 0)
            {
                return -1;
            }

            double sumOdds = 0.0;
            for (int index = probabilities.Count - 1; index >= 0; index--)
            {
                double probability = probabilities[index];
                double odds = (probability >= 1.0) ? double.MaxValue : probability / Math.Max(1.0 - probability, 1E-10);
                sumOdds += odds;
                if (sumOdds >= 1.0)
                {
                    return index;
                }
            }

            return probabilities.Count;
        }

        // Given a sequence of outcomes (true for event occurrence) and the constant event probability,
        // this method applies the optimal stopping rule: starting at the computed threshold index,
        // it selects the first occurrence of the event. This is intended to predict the last occurrence.
        // Returns the selected index (0-based) or -1 if no event is selected.
        public int PredictLastOccurrence(IList<bool> outcomes, double eventProbability)
        {
            if (outcomes == null || outcomes.Count == 0 || eventProbability < 0.0 || eventProbability > 1.0)
            {
                return -1;
            }

            int trialCount = outcomes.Count;
            int threshold = ComputeThresholdIndex(CreateUniformProbabilityList(trialCount, eventProbability));
            for (int i = threshold; i < trialCount; i++)
            {
                if (outcomes[i])
                {
                    return i;
                }
            }

            return -1;
        }

        // Helper: Creates a list of length 'length' with each element set to eventProbability.
        public IList<double> CreateUniformProbabilityList(int length, double eventProbability)
        {
            List<double> probabilities = new List<double>();
            for (int i = 0; i < length; i++)
            {
                probabilities.Add(eventProbability);
            }

            return probabilities;
        }
    }
}