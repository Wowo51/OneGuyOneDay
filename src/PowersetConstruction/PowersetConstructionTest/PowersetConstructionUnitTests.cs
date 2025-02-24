using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowersetConstruction;

namespace PowersetConstructionTest
{
    [TestClass]
    public class PowersetConstructionUnitTests
    {
        [TestMethod]
        public void TestSimpleNFAConversion()
        {
            NFA nfa = new NFA();
            nfa.StartState = 0;
            nfa.AddAcceptState(0);
            DFA dfa = PowersetConverter.ConvertNfaToDfa(nfa);
            // Expect one state "0" with no transitions.
            Assert.AreEqual(1, dfa.States.Count);
            Assert.IsTrue(dfa.States.Contains("0"));
            Assert.AreEqual("0", dfa.StartState);
            Assert.IsTrue(dfa.AcceptStates.Contains("0"));
            Assert.AreEqual(0, dfa.Transitions.Count);
        }

        [TestMethod]
        public void TestMultipleTransitionsNFAConversion()
        {
            NFA nfa = new NFA();
            nfa.StartState = 0;
            nfa.AddAcceptState(2);
            nfa.AddTransition(0, 'a', 0);
            nfa.AddTransition(0, 'a', 1);
            nfa.AddTransition(1, 'b', 2);
            DFA dfa = PowersetConverter.ConvertNfaToDfa(nfa);
            // Expected DFA states: "0", "0,1", "2"
            HashSet<string> expectedStates = new HashSet<string>
            {
                "0",
                "0,1",
                "2"
            };
            CollectionAssert.AreEquivalent(expectedStates.ToList(), dfa.States.ToList());
            // Verify start state and accept states.
            Assert.AreEqual("0", dfa.StartState);
            Assert.AreEqual(1, dfa.AcceptStates.Count);
            Assert.IsTrue(dfa.AcceptStates.Contains("2"));
            // Verify transitions.
            Tuple<string, char> key0 = new Tuple<string, char>("0", 'a');
            Assert.IsTrue(dfa.Transitions.ContainsKey(key0));
            Assert.AreEqual("0,1", dfa.Transitions[key0]);
            Tuple<string, char> key1 = new Tuple<string, char>("0,1", 'a');
            Assert.IsTrue(dfa.Transitions.ContainsKey(key1));
            Assert.AreEqual("0,1", dfa.Transitions[key1]);
            Tuple<string, char> key2 = new Tuple<string, char>("0,1", 'b');
            Assert.IsTrue(dfa.Transitions.ContainsKey(key2));
            Assert.AreEqual("2", dfa.Transitions[key2]);
        }

        [TestMethod]
        public void TestLargeSyntheticNFAConversion()
        {
            // Create a synthetic NFA with a self-loop and chain transitions on 'a'
            NFA nfa = new NFA();
            int numberOfStates = 10;
            for (int i = 0; i < numberOfStates; i++)
            {
                nfa.AddState(i);
                nfa.AddTransition(i, 'a', i);
                if (i < numberOfStates - 1)
                {
                    nfa.AddTransition(i, 'a', i + 1);
                }
            }

            nfa.StartState = 0;
            nfa.AddAcceptState(numberOfStates - 1);
            DFA dfa = PowersetConverter.ConvertNfaToDfa(nfa);
            // In this construction, the reachable DFA states grow cumulatively.
            string finalState = string.Join(",", Enumerable.Range(0, numberOfStates));
            Assert.IsTrue(dfa.States.Contains(finalState));
            Assert.IsTrue(dfa.AcceptStates.Contains(finalState));
            Assert.AreEqual("0", dfa.StartState);
        }
    }
}