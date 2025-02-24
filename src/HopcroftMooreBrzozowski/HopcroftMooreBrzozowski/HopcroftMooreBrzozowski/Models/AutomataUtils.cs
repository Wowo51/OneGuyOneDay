using System;
using System.Collections.Generic;
using System.Linq;

namespace HopcroftMooreBrzozowski.Models
{
    public static class AutomataUtils
    {
        public static NondeterministicFiniteAutomaton ReverseDFA(DeterministicFiniteAutomaton dfa)
        {
            List<string> states = new List<string>(dfa.States);
            List<char> alphabet = new List<char>(dfa.Alphabet);
            var transitions = new Dictionary<string, Dictionary<char, List<string>>>();
            // Initialize transition table for each state.
            foreach (string state in states)
            {
                transitions[state] = new Dictionary<char, List<string>>();
                foreach (char symbol in alphabet)
                {
                    transitions[state][symbol] = new List<string>();
                }
            }

            // Reverse transitions.
            foreach (var entry in dfa.Transitions)
            {
                string fromState = entry.Key;
                foreach (var tran in entry.Value)
                {
                    char symbol = tran.Key;
                    string toState = tran.Value;
                    if (!transitions.ContainsKey(toState))
                    {
                        transitions[toState] = new Dictionary<char, List<string>>();
                        foreach (char sym in alphabet)
                        {
                            transitions[toState][sym] = new List<string>();
                        }
                    }

                    transitions[toState][symbol].Add(fromState);
                }
            }

            return new NondeterministicFiniteAutomaton(states, alphabet, transitions, new List<string>(dfa.AcceptStates), new List<string> { dfa.StartState });
        }

        public static DeterministicFiniteAutomaton DeterminizeNFA(NondeterministicFiniteAutomaton nfa)
        {
            var stateMapping = new Dictionary<string, HashSet<string>>();
            var newTransitions = new Dictionary<string, Dictionary<char, string>>();
            var newStates = new List<string>();
            var newAcceptStates = new List<string>();
            var queue = new Queue<HashSet<string>>();
            HashSet<string> startSet = new HashSet<string>(nfa.StartStates);
            string startName = GetName(startSet);
            stateMapping[startName] = startSet;
            newStates.Add(startName);
            queue.Enqueue(startSet);
            if (startSet.Any(s => nfa.AcceptStates.Contains(s)))
            {
                newAcceptStates.Add(startName);
            }

            while (queue.Count > 0)
            {
                HashSet<string> currentSet = queue.Dequeue();
                string currentName = GetName(currentSet);
                if (!newTransitions.ContainsKey(currentName))
                {
                    newTransitions[currentName] = new Dictionary<char, string>();
                }

                foreach (char symbol in nfa.Alphabet)
                {
                    var nextSet = new HashSet<string>();
                    foreach (string state in currentSet)
                    {
                        if (nfa.Transitions.ContainsKey(state) && nfa.Transitions[state].ContainsKey(symbol))
                        {
                            foreach (string t in nfa.Transitions[state][symbol])
                            {
                                nextSet.Add(t);
                            }
                        }
                    }

                    string nextName = GetName(nextSet);
                    newTransitions[currentName][symbol] = nextName;
                    if (!stateMapping.ContainsKey(nextName))
                    {
                        stateMapping[nextName] = nextSet;
                        newStates.Add(nextName);
                        queue.Enqueue(nextSet);
                        if (nextSet.Any(s => nfa.AcceptStates.Contains(s)))
                        {
                            newAcceptStates.Add(nextName);
                        }
                    }
                }
            }

            return new DeterministicFiniteAutomaton(newStates, nfa.Alphabet, newTransitions, startName, newAcceptStates);
        }

        private static string GetName(HashSet<string> stateSet)
        {
            List<string> elements = stateSet.ToList();
            elements.Sort();
            return elements.Count == 0 ? "∅" : string.Join(",", elements);
        }
    }
}