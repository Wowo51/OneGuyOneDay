using System;
using System.Collections.Generic;

namespace QLearningAlgorithm
{
    public class QLearningAgent
    {
        private readonly double learningRate;
        private readonly double discountFactor;
        private readonly double explorationRate;
        private readonly Dictionary<Tuple<int, int>, double> qTable;
        private readonly Random random;
        public QLearningAgent(double learningRate, double discountFactor, double explorationRate)
        {
            this.learningRate = learningRate;
            this.discountFactor = discountFactor;
            this.explorationRate = explorationRate;
            this.qTable = new Dictionary<Tuple<int, int>, double>();
            this.random = new Random();
        }

        public int ChooseAction(int state, List<int> possibleActions)
        {
            if (possibleActions == null || possibleActions.Count == 0)
            {
                return 0;
            }

            if (this.random.NextDouble() < this.explorationRate)
            {
                int index = this.random.Next(possibleActions.Count);
                return possibleActions[index];
            }

            double bestValue = double.NegativeInfinity;
            int bestAction = possibleActions[0];
            foreach (int action in possibleActions)
            {
                Tuple<int, int> key = Tuple.Create(state, action);
                double value;
                if (!this.qTable.TryGetValue(key, out value))
                {
                    value = 0;
                }

                if (value > bestValue)
                {
                    bestValue = value;
                    bestAction = action;
                }
            }

            return bestAction;
        }

        public void Update(int state, int action, double reward, int nextState, List<int> nextPossibleActions)
        {
            Tuple<int, int> key = Tuple.Create(state, action);
            double oldValue;
            if (!this.qTable.TryGetValue(key, out oldValue))
            {
                oldValue = 0;
            }

            double maxNext = 0;
            if (nextPossibleActions != null)
            {
                foreach (int nextAction in nextPossibleActions)
                {
                    Tuple<int, int> nextKey = Tuple.Create(nextState, nextAction);
                    double nextValue;
                    if (!this.qTable.TryGetValue(nextKey, out nextValue))
                    {
                        nextValue = 0;
                    }

                    if (nextValue > maxNext)
                    {
                        maxNext = nextValue;
                    }
                }
            }

            double newValue = oldValue + this.learningRate * (reward + this.discountFactor * maxNext - oldValue);
            this.qTable[key] = newValue;
        }
    }
}