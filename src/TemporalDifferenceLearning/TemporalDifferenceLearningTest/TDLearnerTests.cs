using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemporalDifferenceLearning;
using System;
using System.Collections.Generic;

namespace TemporalDifferenceLearningTest
{
    [TestClass]
    public class TDLearnerTests
    {
        [TestMethod]
        public void TestInitialValue()
        {
            TemporalDifferenceLearning.TDLearner learner = new TemporalDifferenceLearning.TDLearner(0.1, 0.9);
            double value = learner.GetValue(0);
            Assert.AreEqual(0.0, value, 0.0001, "Initial value should be zero.");
        }

        [TestMethod]
        public void TestSingleUpdate()
        {
            TemporalDifferenceLearning.TDLearner learner = new TemporalDifferenceLearning.TDLearner(0.1, 0.9);
            learner.Update(0, 1, 5.0);
            double expected = 0.1 * 5.0; // Expected 0.5
            double value = learner.GetValue(0);
            Assert.AreEqual(expected, value, 0.0001, "Single update did not produce the expected value.");
        }

        [TestMethod]
        public void TestSelfTransitionConvergence()
        {
            TemporalDifferenceLearning.TDLearner learner = new TemporalDifferenceLearning.TDLearner(0.1, 0.9);
            int iterations = 500;
            for (int i = 0; i < iterations; i++)
            {
                learner.Update(0, 0, 1.0);
            }

            // The fixed point for self-transition with reward 1 is 1/(1-0.9)=10.
            double expected = 1.0 / (1.0 - 0.9);
            double value = learner.GetValue(0);
            Assert.AreEqual(expected, value, 0.1, "Self-transition convergence failed.");
        }

        [TestMethod]
        public void TestChainUpdates()
        {
            TemporalDifferenceLearning.TDLearner learner = new TemporalDifferenceLearning.TDLearner(0.1, 0.9);
            // First update: state0 -> state1 with reward 5.0 => V0 becomes 0.1 * 5.0 = 0.5.
            learner.Update(0, 1, 5.0);
            // Second update: state1 -> state2 with reward 3.0 => V1 becomes 0.1 * 3.0 = 0.3.
            learner.Update(1, 2, 3.0);
            // Third update: state0 -> state1 with reward 2.0.
            // Current V0 = 0.5, and V1 = 0.3 so TD error = 2.0 + 0.9 * 0.3 - 0.5 = 1.77.
            // New V0 = 0.5 + 0.1 * 1.77 = 0.677.
            learner.Update(0, 1, 2.0);
            double expected = 0.677;
            double value = learner.GetValue(0);
            Assert.AreEqual(expected, value, 0.001, "Chain updates produced incorrect value for state 0.");
        }

        [TestMethod]
        public void TestLargeSyntheticData()
        {
            TemporalDifferenceLearning.TDLearner learner = new TemporalDifferenceLearning.TDLearner(0.1, 0.9);
            Random random = new Random(42);
            List<int> updatedStates = new List<int>();
            int iterations = 1000;
            for (int i = 0; i < iterations; i++)
            {
                int state = random.Next(-100, 101);
                int nextState = random.Next(-100, 101);
                double reward = random.NextDouble() * 20.0 - 10.0;
                learner.Update(state, nextState, reward);
                if (!updatedStates.Contains(state))
                {
                    updatedStates.Add(state);
                }
            }

            foreach (int state in updatedStates)
            {
                double value = learner.GetValue(state);
                Assert.IsFalse(Double.IsNaN(value), "Value for state should not be NaN.");
                Assert.IsFalse(Double.IsInfinity(value), "Value for state should be finite.");
            }
        }
    }
}