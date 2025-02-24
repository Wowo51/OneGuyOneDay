using System;
using System.Collections.Generic;

namespace BcjrAlgorithm
{
    public class BcjrDecoder
    {
        public int[] Decode(Trellis trellis)
        {
            int numStages = trellis.NumStages;
            int numStates = trellis.NumStates;
            double[, ] alpha = new double[numStages, numStates];
            double[, ] beta = new double[numStages, numStates];
            // Forward recursion initialization
            for (int i = 0; i < numStates; i++)
            {
                alpha[0, i] = (i == trellis.InitialState) ? 0.0 : double.NegativeInfinity;
            }

            // Forward recursion
            for (int t = 0; t < numStages - 1; t++)
            {
                for (int j = 0; j < numStates; j++)
                {
                    alpha[t + 1, j] = double.NegativeInfinity;
                }

                List<TrellisTransition> transitionsAtT = trellis.Transitions[t];
                foreach (TrellisTransition transition in transitionsAtT)
                {
                    int fromState = transition.FromState;
                    int toState = transition.ToState;
                    double candidate = alpha[t, fromState] + transition.Metric;
                    alpha[t + 1, toState] = LogAdd(alpha[t + 1, toState], candidate);
                }
            }

            // Backward recursion initialization
            for (int i = 0; i < numStates; i++)
            {
                beta[numStages - 1, i] = (i == trellis.FinalState) ? 0.0 : double.NegativeInfinity;
            }

            // Backward recursion
            for (int t = numStages - 2; t >= 0; t--)
            {
                for (int i = 0; i < numStates; i++)
                {
                    beta[t, i] = double.NegativeInfinity;
                }

                List<TrellisTransition> transitionsAtT = trellis.Transitions[t];
                foreach (TrellisTransition transition in transitionsAtT)
                {
                    int fromState = transition.FromState;
                    int toState = transition.ToState;
                    double candidate = beta[t + 1, toState] + transition.Metric;
                    beta[t, fromState] = LogAdd(beta[t, fromState], candidate);
                }
            }

            // Decision: compute log-likelihood for each transition's input bit
            int[] decodedBits = new int[numStages - 1];
            for (int t = 0; t < numStages - 1; t++)
            {
                double sum0 = double.NegativeInfinity;
                double sum1 = double.NegativeInfinity;
                List<TrellisTransition> transitionsAtT = trellis.Transitions[t];
                foreach (TrellisTransition transition in transitionsAtT)
                {
                    double candidate = alpha[t, transition.FromState] + transition.Metric + beta[t + 1, transition.ToState];
                    if (transition.Input == 0)
                    {
                        sum0 = LogAdd(sum0, candidate);
                    }
                    else if (transition.Input == 1)
                    {
                        sum1 = LogAdd(sum1, candidate);
                    }
                }

                decodedBits[t] = (sum1 >= sum0) ? 1 : 0;
            }

            return decodedBits;
        }

        private double LogAdd(double a, double b)
        {
            if (double.IsNegativeInfinity(a))
            {
                return b;
            }

            if (double.IsNegativeInfinity(b))
            {
                return a;
            }

            double maximum = (a > b) ? a : b;
            return maximum + Math.Log(Math.Exp(a - maximum) + Math.Exp(b - maximum));
        }
    }
}