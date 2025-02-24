using System.Collections.Generic;

namespace ViterbiAlgorithm
{
    public static class Viterbi
    {
        public static List<string> FindMostLikelySequence(HiddenMarkovModel model, List<string> observationSequence)
        {
            if (model == null || observationSequence == null || observationSequence.Count == 0 || model.States.Count == 0)
            {
                return new List<string>();
            }

            int T = observationSequence.Count;
            int N = model.States.Count;
            double[, ] delta = new double[T, N];
            int[, ] psi = new int[T, N];
            // Initialization
            int obsIndex0 = model.GetObservationIndex(observationSequence[0]);
            for (int i = 0; i < N; i++)
            {
                double emissionProb = (obsIndex0 >= 0 && model.EmissionProbability.GetLength(1) > obsIndex0) ? model.EmissionProbability[i, obsIndex0] : 0.0;
                delta[0, i] = model.StartProbability[i] * emissionProb;
                psi[0, i] = 0;
            }

            // Recursion
            for (int t = 1; t < T; t++)
            {
                int obsIndex = model.GetObservationIndex(observationSequence[t]);
                for (int j = 0; j < N; j++)
                {
                    double maxProb = 0.0;
                    int maxState = 0;
                    for (int i = 0; i < N; i++)
                    {
                        double prob = delta[t - 1, i] * model.TransitionProbability[i, j];
                        if (prob > maxProb)
                        {
                            maxProb = prob;
                            maxState = i;
                        }
                    }

                    double emissionProb = (obsIndex >= 0 && model.EmissionProbability.GetLength(1) > obsIndex) ? model.EmissionProbability[j, obsIndex] : 0.0;
                    delta[t, j] = maxProb * emissionProb;
                    psi[t, j] = maxState;
                }
            }

            // Termination
            double bestProb = 0.0;
            int bestState = 0;
            for (int i = 0; i < N; i++)
            {
                if (delta[T - 1, i] > bestProb)
                {
                    bestProb = delta[T - 1, i];
                    bestState = i;
                }
            }

            // Backtracking
            int[] stateSequence = new int[T];
            stateSequence[T - 1] = bestState;
            for (int t = T - 2; t >= 0; t--)
            {
                stateSequence[t] = psi[t + 1, stateSequence[t + 1]];
            }

            List<string> result = new List<string>();
            for (int t = 0; t < T; t++)
            {
                result.Add(model.States[stateSequence[t]]);
            }

            return result;
        }
    }
}