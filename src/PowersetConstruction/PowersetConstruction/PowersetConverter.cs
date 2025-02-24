using System;
using System.Collections.Generic;
using System.Linq;

namespace PowersetConstruction
{
    public static class PowersetConverter
    {
        public static DFA ConvertNfaToDfa(NFA nfa)
        {
            HashSet<char> alphabet = new HashSet<char>();
            foreach (KeyValuePair<Tuple<int, char>, HashSet<int>> entry in nfa.Transitions)
            {
                alphabet.Add(entry.Key.Item2);
            }

            Dictionary<string, HashSet<int>> dfaStatesMap = new Dictionary<string, HashSet<int>>();
            Queue<string> unprocessedStates = new Queue<string>();
            DFA dfa = new DFA();
            HashSet<int> startSet = new HashSet<int>();
            startSet.Add(nfa.StartState);
            string startName = StateSetToName(startSet);
            dfaStatesMap[startName] = startSet;
            unprocessedStates.Enqueue(startName);
            dfa.States.Add(startName);
            dfa.StartState = startName;
            while (unprocessedStates.Count > 0)
            {
                string currentName = unprocessedStates.Dequeue();
                HashSet<int> currentSet = dfaStatesMap[currentName];
                foreach (char symbol in alphabet)
                {
                    HashSet<int> newSet = new HashSet<int>();
                    foreach (int state in currentSet)
                    {
                        HashSet<int> transitions = nfa.GetTransitions(state, symbol);
                        foreach (int targetState in transitions)
                        {
                            newSet.Add(targetState);
                        }
                    }

                    if (newSet.Count > 0)
                    {
                        string newName = StateSetToName(newSet);
                        if (!dfaStatesMap.ContainsKey(newName))
                        {
                            dfaStatesMap[newName] = newSet;
                            unprocessedStates.Enqueue(newName);
                            dfa.States.Add(newName);
                        }

                        dfa.AddTransition(currentName, symbol, newName);
                    }
                }
            }

            foreach (KeyValuePair<string, HashSet<int>> entry in dfaStatesMap)
            {
                foreach (int state in entry.Value)
                {
                    if (nfa.AcceptStates.Contains(state))
                    {
                        dfa.AcceptStates.Add(entry.Key);
                        break;
                    }
                }
            }

            return dfa;
        }

        private static string StateSetToName(HashSet<int> stateSet)
        {
            List<int> statesList = stateSet.ToList();
            statesList.Sort();
            return string.Join(",", statesList);
        }
    }
}