using System;
using System.Collections.Generic;

namespace TemporalDifferenceLearning
{
    public class TDLearner
    {
        private readonly Dictionary<int, double> _valueFunction;
        public double Alpha { get; }
        public double Gamma { get; }

        public TDLearner(double alpha, double gamma)
        {
            Alpha = alpha;
            Gamma = gamma;
            _valueFunction = new Dictionary<int, double>();
        }

        public double GetValue(int state)
        {
            double value;
            if (_valueFunction.TryGetValue(state, out value))
            {
                return value;
            }

            return 0.0;
        }

        public void Update(int state, int nextState, double reward)
        {
            double currentValue = GetValue(state);
            double nextValue = GetValue(nextState);
            double tdError = reward + Gamma * nextValue - currentValue;
            _valueFunction[state] = currentValue + Alpha * tdError;
        }
    }
}