using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HopcroftMooreBrzozowski.Minimizers;
using HopcroftMooreBrzozowski.Models;

namespace HopcroftMooreBrzozowskiTest
{
    [TestClass]
    public class AutomataMinimizersTests
    {
        private bool SimulateDFA(DeterministicFiniteAutomaton dfa, string input)
        {
            string currentState = dfa.StartState;
            foreach (char character in input)
            {
                if (dfa.Transitions.ContainsKey(currentState) && dfa.Transitions[currentState].ContainsKey(character))
                {
                    currentState = dfa.Transitions[currentState][character];
                }
                else
                {
                    return false;
                }
            }

            return dfa.AcceptStates.Contains(currentState);
        }

        private string ReverseString(string input)
        {
            char[] characters = input.ToCharArray();
            Array.Reverse(characters);
            return new string (characters);
        }

        private DeterministicFiniteAutomaton GetSampleDFA()
        {
            List<string> states = new List<string>
            {
                "0",
                "1",
                "2",
                "3"
            };
            List<char> alphabet = new List<char>
            {
                'a',
                'b'
            };
            Dictionary<string, Dictionary<char, string>> transitions = new Dictionary<string, Dictionary<char, string>>();
            transitions["0"] = new Dictionary<char, string>
            {
                {
                    'a',
                    "1"
                },
                {
                    'b',
                    "3"
                }
            };
            transitions["1"] = new Dictionary<char, string>
            {
                {
                    'a',
                    "1"
                },
                {
                    'b',
                    "2"
                }
            };
            transitions["2"] = new Dictionary<char, string>
            {
                {
                    'a',
                    "1"
                },
                {
                    'b',
                    "2"
                }
            };
            transitions["3"] = new Dictionary<char, string>
            {
                {
                    'a',
                    "1"
                },
                {
                    'b',
                    "3"
                }
            };
            string startState = "0";
            List<string> acceptStates = new List<string>
            {
                "1",
                "2"
            };
            return new DeterministicFiniteAutomaton(states, alphabet, transitions, startState, acceptStates);
        }

        private DeterministicFiniteAutomaton GetSyntheticDFA(int stateCount, List<char> alphabet, int seed)
        {
            List<string> states = new List<string>();
            for (int index = 0; index < stateCount; index++)
            {
                states.Add(index.ToString());
            }

            Dictionary<string, Dictionary<char, string>> transitions = new Dictionary<string, Dictionary<char, string>>();
            Random randomGenerator = new Random(seed);
            foreach (string state in states)
            {
                Dictionary<char, string> stateTransitions = new Dictionary<char, string>();
                foreach (char symbol in alphabet)
                {
                    int targetIndex = randomGenerator.Next(stateCount);
                    stateTransitions[symbol] = states[targetIndex];
                }

                transitions[state] = stateTransitions;
            }

            string startState = states[0];
            List<string> acceptStates = new List<string>();
            foreach (string state in states)
            {
                if (randomGenerator.NextDouble() < 0.5)
                {
                    acceptStates.Add(state);
                }
            }

            return new DeterministicFiniteAutomaton(states, alphabet, transitions, startState, acceptStates);
        }

        private string GenerateRandomString(int length, List<char> alphabet, Random randomGenerator)
        {
            char[] characters = new char[length];
            for (int index = 0; index < length; index++)
            {
                int symbolIndex = randomGenerator.Next(alphabet.Count);
                characters[index] = alphabet[symbolIndex];
            }

            return new string (characters);
        }

        [TestMethod]
        public void TestBrzozowskiMinimizer()
        {
            DeterministicFiniteAutomaton originalDFA = GetSampleDFA();
            BrzozowskiMinimizer minimizer = new BrzozowskiMinimizer();
            DeterministicFiniteAutomaton minimizedDFA = minimizer.Minimize(originalDFA);
            List<string> testInputs = new List<string>
            {
                "",
                "a",
                "b",
                "aa",
                "ab",
                "ba",
                "bb",
                "aba",
                "bab"
            };
            foreach (string input in testInputs)
            {
                bool originalResult = SimulateDFA(originalDFA, input);
                bool minimizedResult = SimulateDFA(minimizedDFA, input);
                Assert.AreEqual(originalResult, minimizedResult, "Brzozowski minimizer mismatch on input: " + input);
            }

            Assert.IsTrue(minimizedDFA.States.Count <= originalDFA.States.Count, "Minimized DFA should have no more states than the original DFA.");
        }

        [TestMethod]
        public void TestHopcroftMinimizer()
        {
            DeterministicFiniteAutomaton originalDFA = GetSampleDFA();
            HopcroftMinimizer minimizer = new HopcroftMinimizer();
            DeterministicFiniteAutomaton minimizedDFA = minimizer.Minimize(originalDFA);
            List<string> testInputs = new List<string>
            {
                "",
                "a",
                "b",
                "aa",
                "ab",
                "ba",
                "bb",
                "aba",
                "bab"
            };
            foreach (string input in testInputs)
            {
                bool originalResult = SimulateDFA(originalDFA, input);
                bool minimizedResult = SimulateDFA(minimizedDFA, input);
                Assert.AreEqual(originalResult, minimizedResult, "Hopcroft minimizer mismatch on input: " + input);
            }

            Assert.IsTrue(minimizedDFA.States.Count <= originalDFA.States.Count, "Minimized DFA should have no more states than the original DFA.");
        }

        [TestMethod]
        public void TestMooreMinimizer()
        {
            DeterministicFiniteAutomaton originalDFA = GetSampleDFA();
            MooreMinimizer minimizer = new MooreMinimizer();
            DeterministicFiniteAutomaton minimizedDFA = minimizer.Minimize(originalDFA);
            List<string> testInputs = new List<string>
            {
                "",
                "a",
                "b",
                "aa",
                "ab",
                "ba",
                "bb",
                "aba",
                "bab"
            };
            foreach (string input in testInputs)
            {
                bool originalResult = SimulateDFA(originalDFA, input);
                bool minimizedResult = SimulateDFA(minimizedDFA, input);
                Assert.AreEqual(originalResult, minimizedResult, "Moore minimizer mismatch on input: " + input);
            }

            Assert.IsTrue(minimizedDFA.States.Count <= originalDFA.States.Count, "Minimized DFA should have no more states than the original DFA.");
        }

        [TestMethod]
        public void TestSyntheticDFAWithMinimizers()
        {
            List<char> alphabet = new List<char>
            {
                '0',
                '1'
            };
            DeterministicFiniteAutomaton syntheticDFA = GetSyntheticDFA(10, alphabet, 12345);
            Random randomGenerator = new Random(54321);
            List<Func<DeterministicFiniteAutomaton, DeterministicFiniteAutomaton>> minimizerFunctions = new List<Func<DeterministicFiniteAutomaton, DeterministicFiniteAutomaton>>
            {
                new Func<DeterministicFiniteAutomaton, DeterministicFiniteAutomaton>(dfa => (new BrzozowskiMinimizer()).Minimize(dfa)),
                new Func<DeterministicFiniteAutomaton, DeterministicFiniteAutomaton>(dfa => (new HopcroftMinimizer()).Minimize(dfa)),
                new Func<DeterministicFiniteAutomaton, DeterministicFiniteAutomaton>(dfa => (new MooreMinimizer()).Minimize(dfa))
            };
            foreach (Func<DeterministicFiniteAutomaton, DeterministicFiniteAutomaton> minimizerFunc in minimizerFunctions)
            {
                DeterministicFiniteAutomaton minimizedDFA = minimizerFunc(syntheticDFA);
                for (int index = 0; index < 10; index++)
                {
                    string testInput = GenerateRandomString(10, alphabet, randomGenerator);
                    bool originalResult = SimulateDFA(syntheticDFA, testInput);
                    bool minimizedResult = SimulateDFA(minimizedDFA, testInput);
                    Assert.AreEqual(originalResult, minimizedResult, "Synthetic DFA minimizer mismatch on input: " + testInput);
                }

                Assert.IsTrue(minimizedDFA.States.Count <= syntheticDFA.States.Count, "Minimized synthetic DFA should have no more states than the original.");
            }
        }

        [TestMethod]
        public void TestReverseDeterminize()
        {
            DeterministicFiniteAutomaton originalDFA = GetSampleDFA();
            NondeterministicFiniteAutomaton reversedNFA = AutomataUtils.ReverseDFA(originalDFA);
            DeterministicFiniteAutomaton determinizedReversedDFA = AutomataUtils.DeterminizeNFA(reversedNFA);
            List<string> testInputs = new List<string>
            {
                "",
                "a",
                "b",
                "aa",
                "ab",
                "ba",
                "bb",
                "aba",
                "bab"
            };
            foreach (string input in testInputs)
            {
                // In the reversed automaton, a string is accepted if and only if its reverse is accepted by the original DFA.
                bool expectedResult = SimulateDFA(originalDFA, ReverseString(input));
                bool actualResult = SimulateDFA(determinizedReversedDFA, input);
                Assert.AreEqual(expectedResult, actualResult, "Reverse determinize mismatch on input: " + input);
            }
        }
    }
}