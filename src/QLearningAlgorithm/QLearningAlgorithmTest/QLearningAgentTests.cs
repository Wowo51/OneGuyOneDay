using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLearningAlgorithm;

namespace QLearningAlgorithmTest
{
    [TestClass]
    public class QLearningAgentTests
    {
        [TestMethod]
        public void TestChooseAction_NullAndEmptyActions()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            int resultNull = agent.ChooseAction(1, null);
            Assert.AreEqual(0, resultNull);
            List<int> emptyList = new List<int>();
            int resultEmpty = agent.ChooseAction(1, emptyList);
            Assert.AreEqual(0, resultEmpty);
        }

        [TestMethod]
        public void TestChooseAction_ExploitationBestAction()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            List<int> actions = new List<int>
            {
                10,
                20
            };
            // Initially, both actions have a default Q-value of 0, so the first action is returned.
            int initial = agent.ChooseAction(1, actions);
            Assert.AreEqual(10, initial);
            // Update the Q-value for action 20 so it becomes the best action.
            agent.Update(1, 20, 10.0, 1, actions);
            int result = agent.ChooseAction(1, actions);
            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void TestUpdate_WithNullNextPossibleActions()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            List<int> actions = new List<int>
            {
                30,
                40
            };
            // With null nextPossibleActions, the update should compute maxNext as 0.
            // For action 40, new Q-value becomes 0.5 * (8.0 - 0) = 4.0, making it preferred over the default 0.
            agent.Update(2, 40, 8.0, 2, null);
            int chosen = agent.ChooseAction(2, actions);
            Assert.AreEqual(40, chosen);
        }

        [TestMethod]
        public void TestChooseAction_WithLargeActionSet()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            List<int> actions = new List<int>();
            for (int i = 1; i <= 10000; i++)
            {
                actions.Add(i);
            }

            int result = agent.ChooseAction(3, actions);
            // When all Q-values are equal, the first action is returned.
            Assert.AreEqual(actions[0], result);
        }

        [TestMethod]
        public void TestChooseAction_RandomExploration()
        {
            // With explorationRate set to 1.0, the agent should always choose an action at random.
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 1.0);
            List<int> actions = new List<int>
            {
                5,
                15,
                25
            };
            int result = agent.ChooseAction(4, actions);
            Assert.IsTrue(actions.Contains(result));
        }

        [TestMethod]
        public void TestUpdate_NonNullNextPossibleActions()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            List<int> actions = new List<int>
            {
                1,
                2,
                3
            };
            // Update action 2 in state 10 with reward 5.0, then update action 1 with reward 6.0.
            agent.Update(10, 2, 5.0, 10, actions);
            agent.Update(10, 1, 6.0, 10, actions);
            int chosen = agent.ChooseAction(10, actions);
            Assert.AreEqual(1, chosen);
        }

        [TestMethod]
        public void TestUpdate_NegativeReward()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            List<int> actions = new List<int>
            {
                10
            };
            agent.Update(5, 10, -2.0, 5, actions);
            int chosen = agent.ChooseAction(5, actions);
            Assert.AreEqual(10, chosen);
        }

        [TestMethod]
        public void TestMultipleUpdates()
        {
            QLearningAgent agent = new QLearningAgent(0.5, 0.9, 0.0);
            List<int> actions = new List<int>
            {
                1,
                2
            };
            agent.Update(1, 1, 10.0, 1, actions);
            agent.Update(1, 2, 5.0, 1, actions);
            int chosen = agent.ChooseAction(1, actions);
            Assert.AreEqual(1, chosen);
        }
    }
}