using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BcjrAlgorithm;

namespace BcjrAlgorithmTest
{
    [TestClass]
    public class BcjrDecoderTests
    {
        [TestMethod]
        public void TestSimpleTrellis()
        {
            int numStages = 3;
            int numStates = 2;
            Trellis trellis = new Trellis();
            trellis.NumStages = numStages;
            trellis.NumStates = numStates;
            trellis.InitialState = 0;
            trellis.FinalState = 0;
            trellis.Transitions = new List<List<TrellisTransition>>();
            List<TrellisTransition> stage0 = new List<TrellisTransition>();
            stage0.Add(new TrellisTransition { FromState = 0, ToState = 0, Input = 0, Metric = 1.0 });
            stage0.Add(new TrellisTransition { FromState = 0, ToState = 1, Input = 1, Metric = 0.5 });
            trellis.Transitions.Add(stage0);
            List<TrellisTransition> stage1 = new List<TrellisTransition>();
            stage1.Add(new TrellisTransition { FromState = 0, ToState = 0, Input = 0, Metric = 1.0 });
            stage1.Add(new TrellisTransition { FromState = 1, ToState = 0, Input = 1, Metric = 2.0 });
            trellis.Transitions.Add(stage1);
            BcjrDecoder decoder = new BcjrDecoder();
            int[] decodedBits = decoder.Decode(trellis);
            int[] expected = new int[]
            {
                1,
                1
            };
            Assert.AreEqual(expected.Length, decodedBits.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], decodedBits[i]);
            }
        }

        [TestMethod]
        public void TestDecodeTie()
        {
            int numStages = 2;
            int numStates = 1;
            Trellis trellis = new Trellis();
            trellis.NumStages = numStages;
            trellis.NumStates = numStates;
            trellis.InitialState = 0;
            trellis.FinalState = 0;
            trellis.Transitions = new List<List<TrellisTransition>>();
            List<TrellisTransition> stage0 = new List<TrellisTransition>();
            stage0.Add(new TrellisTransition { FromState = 0, ToState = 0, Input = 0, Metric = 1.0 });
            stage0.Add(new TrellisTransition { FromState = 0, ToState = 0, Input = 1, Metric = 1.0 });
            trellis.Transitions.Add(stage0);
            BcjrDecoder decoder = new BcjrDecoder();
            int[] decodedBits = decoder.Decode(trellis);
            int[] expected = new int[]
            {
                1
            };
            Assert.AreEqual(expected.Length, decodedBits.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], decodedBits[i]);
            }
        }

        [TestMethod]
        public void TestRandomTrellis()
        {
            int numStages = 10;
            int numStates = 3;
            Trellis trellis = CreateCompleteTrellis(numStages, numStates, 0, 0);
            BcjrDecoder decoder = new BcjrDecoder();
            int[] decodedBits = decoder.Decode(trellis);
            Assert.AreEqual(numStages - 1, decodedBits.Length);
            for (int i = 0; i < decodedBits.Length; i++)
            {
                Assert.IsTrue(decodedBits[i] == 0 || decodedBits[i] == 1);
            }
        }

        private Trellis CreateCompleteTrellis(int numStages, int numStates, int initialState, int finalState)
        {
            Trellis trellis = new Trellis();
            trellis.NumStages = numStages;
            trellis.NumStates = numStates;
            trellis.InitialState = initialState;
            trellis.FinalState = finalState;
            trellis.Transitions = new List<List<TrellisTransition>>();
            Random random = new Random(42);
            for (int t = 0; t < numStages - 1; t++)
            {
                List<TrellisTransition> stageTransitions = new List<TrellisTransition>();
                for (int from = 0; from < numStates; from++)
                {
                    for (int to = 0; to < numStates; to++)
                    {
                        int input = random.Next(0, 2);
                        double metric = random.NextDouble() * 10.0;
                        stageTransitions.Add(new TrellisTransition { FromState = from, ToState = to, Input = input, Metric = metric });
                    }
                }

                trellis.Transitions.Add(stageTransitions);
            }

            return trellis;
        }
    }
}